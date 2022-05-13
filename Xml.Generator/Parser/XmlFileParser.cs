namespace Xml.Generator;

public class XmlFileParser : XmlParser
{
    private readonly Func<string[], IXmlParser> _xmlHeaderParserFactory;

    public XmlFileParser(
        Func<string[], IXmlParser> xmlHeaderParserFactory
        , params string[] texts) : base(texts)
    {
        _xmlHeaderParserFactory = xmlHeaderParserFactory ?? throw new ArgumentNullException(nameof(xmlHeaderParserFactory));
    }

    public override void CreateXmlTextObjects()
    {
        var list = new List<IText>
            {
                new XmlHeader(_xmlHeaderParserFactory(Texts))
            };
        TextObjects = list.ToArray();
    }
}