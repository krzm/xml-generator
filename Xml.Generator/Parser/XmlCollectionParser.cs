namespace Xml.Generator;

public class XmlCollectionParser
    : IXmlParser
{
    private readonly IXmlParser? xmlParser;
    private readonly IXmlParser? startXmlParser;
    private readonly IXmlParser? endXmlParser;

    public IText[]? TextObjects { get; protected set; }

    public XmlCollectionParser(params IXmlParser[] xmlParsers)
    {
        switch (xmlParsers.Length)
        {
            case 1:
                xmlParser = xmlParsers[0];
                break;
            case 2:
                startXmlParser = xmlParsers[0];
                endXmlParser = xmlParsers[1];
                break;
            default:
                throw new ArgumentException(nameof(XmlCollectionParser));
        }
    }

    public void CreateXmlTextObjects()
    {
        ArgumentNullException.ThrowIfNull(startXmlParser);
        ArgumentNullException.ThrowIfNull(endXmlParser);
        var list = new List<IText>
            {
                new XmlStart(xmlParser ?? startXmlParser)
            };
        list.Add(new XmlEnd(xmlParser ?? endXmlParser));
        TextObjects = list.ToArray();
    }
}