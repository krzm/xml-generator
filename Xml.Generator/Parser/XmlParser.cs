namespace Xml.Generator;

public abstract class XmlParser
    : IXmlParser
{
    protected readonly string[] Texts;

    public IText[]? TextObjects { get; protected set; }

    public XmlParser(params string[] texts) => Texts = texts;

    public abstract void CreateXmlTextObjects();
}