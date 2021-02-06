module rec Star.AST

///
/// Simple value in the Star Language
/// 
type StarValue =
    | NilValue
    | StringValue of string
    | CharValue of char
    | BoolValue of bool
    | DoubleValue of float
    | FloatValue of float32
    
///
/// Star AST Representation
/// 
type StarAST =
    | Constant of StarValue
    | List of StarAST list