namespace Xml.Generator;

public abstract class XmlElementText
    : IText
{
    protected readonly IXmlParser XmlParser;

    public abstract string Text { get; }

    protected XmlElementText(IXmlParser xmlParser) => XmlParser = xmlParser;
}