using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Lexer
{
    private static readonly Dictionary<string, TokenType> Keywords = new Dictionary<string, TokenType>
    {
        { "if", TokenType.KEYWORD },
        { "else", TokenType.KEYWORD },
        { "while", TokenType.KEYWORD },
        { "for", TokenType.KEYWORD },
        { "function", TokenType.KEYWORD },
        { "return", TokenType.KEYWORD },

        // Tipos de datos
        { "int", TokenType.TYPE },
        { "float", TokenType.TYPE },
        { "double", TokenType.TYPE },
        { "bool", TokenType.TYPE },
        { "void", TokenType.TYPE }
    };

    private static readonly Dictionary<string, TokenType> Operators = new Dictionary<string, TokenType>
    {
        { "==", TokenType.OPERATOR },
        { "!=", TokenType.OPERATOR },
        { "<=", TokenType.OPERATOR },
        { ">=", TokenType.OPERATOR },
        { "&&", TokenType.OPERATOR },
        { "||", TokenType.OPERATOR },
        { "+", TokenType.OPERATOR },
        { "-", TokenType.OPERATOR },
        { "*", TokenType.OPERATOR },
        { "/", TokenType.OPERATOR },
        { "=", TokenType.OPERATOR },
        { "<", TokenType.OPERATOR },
        { ">", TokenType.OPERATOR }
    };

    private static readonly HashSet<char> Symbols = new HashSet<char> { '(', ')', '{', '}', ';', ',' };

    public Lexer() { }

    public List<Token> Analyze(string input)
    {
        List<Token> tokens = new List<Token>();
        string[] words = Regex.Split(input, @"(\s+|\b)");

        foreach (string word in words)
        {
            if (string.IsNullOrWhiteSpace(word)) continue;

            if (Keywords.ContainsKey(word))
            {
                tokens.Add(new Token(Keywords[word], word));
            }
            else if (Operators.ContainsKey(word))
            {
                tokens.Add(new Token(Operators[word], word));
            }
            else if (Symbols.Contains(word[0]))
            {
                tokens.Add(new Token(TokenType.SYMBOL, word));
            }
            else if (Regex.IsMatch(word, @"^[0-9]+(\.[0-9]+)?$"))
            {
                tokens.Add(new Token(TokenType.NUMBER, word));
            }
            else if (Regex.IsMatch(word, @"^[a-zA-Z_][a-zA-Z0-9_]*$"))
            {
                tokens.Add(new Token(TokenType.IDENTIFIER, word));
            }
            else
            {
                tokens.Add(new Token(TokenType.UNKNOWN, word));
            }
        }

        return tokens;
    }
}

public enum TokenType
{
    IDENTIFIER,
    NUMBER,
    OPERATOR,
    KEYWORD,
    TYPE,
    SYMBOL,
    UNKNOWN
}

public class Token
{
    public TokenType Type { get; }
    public string Value { get; }

    public Token(TokenType type, string value)
    {
        Type = type;
        Value = value;
    }

    public override string ToString()
    {
        return $"{Type}: {Value}";
    }
}
