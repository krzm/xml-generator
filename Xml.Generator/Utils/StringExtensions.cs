using System.Text;
using System.Text.RegularExpressions;

namespace Xml.Generator;

public static class StringExtensions
{
    public static string GetAsOneLine(this string text)
    {
        text = text.Replace(Environment.NewLine, " ");
        return text;
    }

    public static int CountWords(this string text) =>
        text.Split(new char[] { ' ', '.', '?' },
            StringSplitOptions.RemoveEmptyEntries).Length;

    public static string AddNewLine(this string text) =>
        text + Environment.NewLine;

    //todo: unitTest
    public static string JoinWithComma(this string[] testParameters) => string.Join(',', testParameters);

    //todo: unitTest
    public static string JoinTestParams(this string[] testParameters)
    {
        var array = (from testParameter in testParameters
                     select $"{{{testParameter.EscapeNewLine()}}}").ToArray();
        return string.Join(',', array);
    }

    //todo: unitTest
    public static string EscapeNewLine(this string text)
    {
        if (!text.Contains("\r\n")) return text;
        return text.Replace("\r", "\\r").Replace("\n", "\\n");
    }

    public static string RemoveSpecialCharacters(this string str)
    {
        var sb = new StringBuilder();
        foreach (char c in str)
        {
            if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
            {
                sb.Append(c);
            }
        }
        return sb.ToString();
    }

    public static string RemoveSubstring(this string source, string substring)
    {
        if (string.IsNullOrWhiteSpace(source)
            || string.IsNullOrWhiteSpace(substring))
            return string.Empty;
        var index = source.IndexOf(substring);
        return index < 0 ? source : source.Remove(index, substring.Length);
    }

    public static string ReplaceWhitespace(this string input, string replacement)
    {
        if (input == "" || input == string.Empty)
            return "";
        if (input == null || replacement == null) 
            return string.Empty;
        return new Regex(@"\s+").Replace(input, replacement);
    }

    public static string RemoveWhiteSpace(this string input) => ReplaceWhitespace(input, "");

    public static bool IsValue(this string input) => !string.IsNullOrEmpty(input) && !string.IsNullOrWhiteSpace(input);

    public static bool IsNoValue(this string input) => string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input);
}