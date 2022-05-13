namespace Xml.Generator;

public abstract class XmlElement
    : XmlElementText
{
    protected readonly IText? LineNumber;
    protected readonly IText? Prefix;
    protected readonly IText? Name;
    protected readonly IText? Postfix;

    protected XmlElement(IXmlParser xmlParser) : base(xmlParser)
    {
        XmlParser.CreateXmlTextObjects();
        ArgumentNullException.ThrowIfNull(XmlParser.TextObjects);
        foreach (var textObj in XmlParser.TextObjects)
        {
            if (textObj is XmlLineNumber)
                LineNumber = textObj;
            if (textObj is XmlPrefix)
                Prefix = textObj;
            if (textObj is XmlName)
                Name = textObj;
            if (textObj is XmlPostfix)
                Postfix = textObj;
        }
    }
}