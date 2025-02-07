%{
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define MAX_TOKEN_LENGTH 256

#ifdef _WIN32
#define YY_DECL __declspec(dllexport) int yylex(void)
#else
#define YY_DECL int yylex(void)
#endif

char token[MAX_TOKEN_LENGTH];
%}

%%

[ \t\r\n]+   ;  // Ignorar espacios y saltos de línea
"if"         { snprintf(token, MAX_TOKEN_LENGTH, "KEYWORD:if"); return 1; }
"else"       { snprintf(token, MAX_TOKEN_LENGTH, "KEYWORD:else"); return 1; }
"while"      { snprintf(token, MAX_TOKEN_LENGTH, "KEYWORD:while"); return 1; }
"int"        { snprintf(token, MAX_TOKEN_LENGTH, "TYPE:int"); return 1; }
"float"      { snprintf(token, MAX_TOKEN_LENGTH, "TYPE:float"); return 1; }
"double"     { snprintf(token, MAX_TOKEN_LENGTH, "TYPE:double"); return 1; }
"bool"       { snprintf(token, MAX_TOKEN_LENGTH, "TYPE:bool"); return 1; }
[a-zA-Z_][a-zA-Z0-9_]*  { snprintf(token, MAX_TOKEN_LENGTH, "IDENTIFIER:%s", yytext); return 1; }
[0-9]+       { snprintf(token, MAX_TOKEN_LENGTH, "NUMBER:%s", yytext); return 1; }
"=="         { snprintf(token, MAX_TOKEN_LENGTH, "OPERATOR:=="); return 1; }
"!="         { snprintf(token, MAX_TOKEN_LENGTH, "OPERATOR:!="); return 1; }
"<="         { snprintf(token, MAX_TOKEN_LENGTH, "OPERATOR:<="); return 1; }
">="         { snprintf(token, MAX_TOKEN_LENGTH, "OPERATOR:>="); return 1; }
"<"          { snprintf(token, MAX_TOKEN_LENGTH, "OPERATOR:<"); return 1; }
">"          { snprintf(token, MAX_TOKEN_LENGTH, "OPERATOR:>"); return 1; }
"&&"         { snprintf(token, MAX_TOKEN_LENGTH, "OPERATOR:&&"); return 1; }
"||"         { snprintf(token, MAX_TOKEN_LENGTH, "OPERATOR:||"); return 1; }
[+\-*/=(){}] { snprintf(token, MAX_TOKEN_LENGTH, "SYMBOL:%c", yytext[0]); return 1; }
.            { snprintf(token, MAX_TOKEN_LENGTH, "UNKNOWN:%c", yytext[0]); return 1; }

%%

int yywrap() {
    return 1;
}

YY_DECL;

const char* get_token() {
    int result = yylex();
    if (result == 0) {
        return NULL;
    }
    return token;
}

int main() {
    yylex();
    return 0;
}
