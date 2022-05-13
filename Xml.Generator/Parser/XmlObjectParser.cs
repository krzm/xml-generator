namespace Xml.Generator;

public class XmlObjectParser : IXmlParser
{
    private readonly string[][] _properties;
    private readonly Func<string[], IXmlParser> _propertyParserFactory;
    private readonly XmlParser[] _xmlParsers;
    private XmlParser _xmlParser;
    private XmlParser _xmlStartParsers;
    private XmlParser _xmlEndParsers;

    public IText[] TextObjects { get; protected set; }

    public XmlObjectParser(string[][] properties
        , Func<string[], IXmlParser> propertyParserFactory
        , params XmlParser[] xmlParsers)
    {
        _properties = properties ?? throw new ArgumentNullException(nameof(properties));
        _propertyParserFactory = propertyParserFactory ?? throw new ArgumentNullException(nameof(propertyParserFactory));
        _xmlParsers = xmlParsers ?? throw new ArgumentNullException(nameof(xmlParsers));
    }

    public void CreateXmlTextObjects() => CreateXmlObject();

    private void CreateXmlObject()
    {
        var list = new List<IText>();
        switch (_xmlParsers.Length)
        {
            case 1:
                _xmlParser = _xmlParsers[0];
                list.AddRange(CreateXmlElements(_xmlParser, _xmlParser));
                break;
            case 2:
                _xmlStartParsers = _xmlParsers[0];
                _xmlEndParsers = _xmlParsers[1];
                list.AddRange(CreateXmlElements(_xmlStartParsers, _xmlEndParsers));
                break;
            default:
                throw new ArgumentException(nameof(XmlObjectParser));
        }
        TextObjects = list.ToArray();
    }

    private List<IText> CreateXmlElements(XmlParser _xmlStartParser, XmlParser _xmlStopParser)
    {
        var list = new List<IText>
            {
                new XmlStart(_xmlStartParser)
            };
        list.AddRange(CreateXmlProperties());
        list.Add(new XmlEnd(_xmlStopParser));
        return list;
    }

    private List<IText> CreateXmlProperties()
    {
        var propertiesObject = new List<IText>();
        foreach (var property in _properties)
        {
            var propertyParser = _propertyParserFactory(property);
            propertiesObject.Add(new XmlPropertyText(propertyParser));
        }
        return propertiesObject;
    }
}