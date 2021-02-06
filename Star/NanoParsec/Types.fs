module rec Nano.Types

///
/// Result type
///
type Result<'a> =
    | Success of 'a
    | Failure of ParserLabel * ParserError

///
/// Print a Result type
/// 
let printResult (result: Result<'a>): unit =
    match result with
    | Success value ->
        printfn "%A" value
    | Failure (label, error) ->
        printfn "Error parsing %s\n%s" label error

///
/// Map a function over a Result
/// 
let mapR (f: 'a -> 'b) (result: Result<'a>): Result<'b> =
    match result with
    | Success value             -> Success (f value)
    | Failure (label, error)    -> Failure (label, error)

// ----------------------------------------------------------------------------
// Parser type

///
/// Parser Label, Error and Input aliases
/// 
type ParserLabel  = string
type ParserError  = string
type InputRemains = string

///
/// Parser type
///
type Parser<'a> =
    {
        ParseFn: (string -> Result<'a * InputRemains>)
        Label: ParserLabel
    }
    
let run (parser: Parser<'a>) (input: string): Result<'a * InputRemains> =
    let { ParseFn = parserFunction; Label = label } = parser
    parserFunction input
    
// ----------------------------------------------------------------------------
// Functor, Applicative and Monad implementation for Parser

///
/// Map a function over Parser
/// 
let mapP (f: 'a -> 'b) (parser: Parser<'a>): Parser<'b> =
    let innerFn (input: string) =
        let result = run parser input
        mapR f result
    in innerFn