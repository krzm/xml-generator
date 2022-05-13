namespace Xml.Generator;

public class XmlObjectParser : IXmlParser
{
    private readonly string[][]? properties;
    private readonly Func<string[], IXmlParser>? propertyParserFactory;
    private readonly XmlParser[]? xmlParsers;
    private XmlParser? xmlParser;
    private XmlParser? xmlStartParsers;
    private XmlParser? xmlEndParsers;

    public IText[]? TextObjects { get; protected set; }

    public XmlObjectParser(string[][] properties
        , Func<string[], IXmlParser> propertyParserFactory
        , params XmlParser[] xmlParsers)
    {
        this.properties = properties ?? throw new ArgumentNullException(nameof(properties));
        this.propertyParserFactory = propertyParserFactory ?? throw new ArgumentNullException(nameof(propertyParserFactory));
        this.xmlParsers = xmlParsers ?? throw new ArgumentNullException(nameof(xmlParsers));
    }

    public void CreateXmlTextObjects() => CreateXmlObject();

    private void CreateXmlObject()
    {
        var list = new List<IText>();
        switch (xmlParsers?.Length)
        {
            case 1:
                xmlParser = xmlParsers[0];
                list.AddRange(CreateXmlElements(xmlParser, xmlParser));
                break;
            case 2:
                xmlStartParsers = xmlParsers[0];
                xmlEndParsers = xmlParsers[1];
                list.AddRange(CreateXmlElements(xmlStartParsers, xmlEndParsers));
                break;
            default:
                throw new ArgumentException(nameof(XmlObjectParser));
        }
        TextObjects = list.ToArray();
    }

    private List<IText> CreateXmlElements(
        XmlParser xmlStartParser
        , XmlParser xmlStopParser)
    {
        var list = new List<IText>
            {
                new XmlStart(xmlStartParser)
            };
        list.AddRange(CreateXmlProperties());
        list.Add(new XmlEnd(xmlStopParser));
        return list;
    }

    private List<IText> CreateXmlProperties()
    {
        ArgumentNullException.ThrowIfNull(properties);
        ArgumentNullException.ThrowIfNull(propertyParserFactory);
        var propertiesObject = new List<IText>();
        foreach (var property in properties)
        {
            var propertyParser = propertyParserFactory(property);
            propertiesObject.Add(new XmlPropertyText(propertyParser));
        }
        return propertiesObject;
    }
}