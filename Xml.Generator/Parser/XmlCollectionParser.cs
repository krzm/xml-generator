namespace Xml.Generator;

public class XmlCollectionParser
    : IXmlParser
{
    private readonly IXmlParser _xmlParser;
    private readonly IXmlParser _startXmlParser;
    private readonly IXmlParser _endXmlParser;

    public IText[] TextObjects { get; protected set; }

    public XmlCollectionParser(params IXmlParser[] xmlParsers)
    {
        switch (xmlParsers.Length)
        {
            case 1:
                _xmlParser = xmlParsers[0];
                break;
            case 2:
                _startXmlParser = xmlParsers[0];
                _endXmlParser = xmlParsers[1];
                break;
            default:
                throw new ArgumentException(nameof(XmlCollectionParser));
        }
    }

    public void CreateXmlTextObjects()
    {
        var list = new List<IText>
            {
                new XmlStart(_xmlParser ?? _startXmlParser)
            };
        list.Add(new XmlEnd(_xmlParser ?? _endXmlParser));
        TextObjects = list.ToArray();
    }
}