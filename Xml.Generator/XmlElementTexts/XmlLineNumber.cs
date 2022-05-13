namespace Xml.Generator;

public class XmlLineNumber : IText
{
    private readonly string _lineNumber;

    public string Text => $"{_lineNumber}	|";

    public XmlLineNumber(string lineNumber)
    {
        _lineNumber = lineNumber;
    }
}