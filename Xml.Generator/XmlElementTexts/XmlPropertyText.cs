namespace Xml.Generator;

public class XmlPropertyText : XmlElementText, IOrderedText
{
    private readonly IText _xmlLineNumber;
    private readonly IText _xmlStart;
    private readonly IText _xmlValue;
    private readonly IText _xmlEnd;

    public override string Text => $"{_xmlStart?.Text}{_xmlValue?.Text}{_xmlEnd?.Text}";

    public string OrderedText => $"{_xmlLineNumber?.Text}{Text}";

    public XmlPropertyText(IXmlParser xmlBuilder) : base(xmlBuilder)
    {
        XmlParser.CreateXmlTextObjects();
        foreach (var textObj in XmlParser.TextObjects)
        {
            if (textObj is XmlLineNumber)
                _xmlLineNumber = textObj;
            if (textObj is XmlStart || textObj is XmlStartClosed)
                _xmlStart = textObj;
            if (textObj is XmlValue)
                _xmlValue = textObj;
            if (textObj is XmlEnd)
                _xmlEnd = textObj;
        }
    }
}