namespace Xml.Generator;

public abstract class XmlElementsText : IText
{
    protected readonly IXmlParser[] XmlParsers;

    public abstract string Text { get; }

    public XmlElementsText(IXmlParser[] xmlParsers) => XmlParsers = xmlParsers;
}