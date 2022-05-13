namespace Xml.Generator;

public class XmlPropertyText
    : XmlElementText
        , IOrderedText
{
    private readonly IText? xmlLineNumber;
    private readonly IText? xmlStart;
    private readonly IText? xmlValue;
    private readonly IText? xmlEnd;

    public override string Text => $"{xmlStart?.Text}{xmlValue?.Text}{xmlEnd?.Text}";

    public string OrderedText => $"{xmlLineNumber?.Text}{Text}";

    public XmlPropertyText(IXmlParser xmlBuilder) : base(xmlBuilder)
    {
        XmlParser.CreateXmlTextObjects();
        ArgumentNullException.ThrowIfNull(XmlParser);
        ArgumentNullException.ThrowIfNull(XmlParser.TextObjects);
        foreach (var textObj in XmlParser.TextObjects)
        {
            if (textObj is XmlLineNumber)
                xmlLineNumber = textObj;
            if (textObj is XmlStart || textObj is XmlStartClosed)
                xmlStart = textObj;
            if (textObj is XmlValue)
                xmlValue = textObj;
            if (textObj is XmlEnd)
                xmlEnd = textObj;
        }
    }
}