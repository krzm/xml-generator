namespace Xml.Generator;

public class XmlCompositeObjectBuilder : IXmlCompositeObjectBuilder
{
    private readonly Func<IXmlParser[], int, IText> _xmlObjectFactory;
    private readonly IXmlBuilder<XmlObjectParts> _xmlObjBuilder;
    protected readonly IXmlBuilder<XmlObjectParts>[] XmlInnerObjBuilder;
    private readonly int _innerObjectPosition;

    public XmlCompositeObjectBuilder(
        Func<IXmlParser[], int, IText> xmlObjectFactory
        , IXmlBuilder<XmlObjectParts> xmlObjBuilder
        , IXmlBuilder<XmlObjectParts>[] xmlInnerObjBuilder
        , int innerObjectPosition)
    {
        _xmlObjectFactory = xmlObjectFactory ?? throw new ArgumentNullException(nameof(xmlObjectFactory));
        _xmlObjBuilder = xmlObjBuilder ?? throw new ArgumentNullException(nameof(xmlObjBuilder));
        XmlInnerObjBuilder = xmlInnerObjBuilder ?? throw new ArgumentNullException(nameof(xmlInnerObjBuilder));
        _innerObjectPosition = innerObjectPosition >= 0 ? innerObjectPosition : throw new ArgumentNullException(nameof(innerObjectPosition));
    }

    public IText CreateXml() =>
        _xmlObjectFactory(GetParsers(), _innerObjectPosition);

    private IXmlParser[] GetParsers()
    {
        var parsers = new List<IXmlParser>
            {
                GetObjParser()
            };
        parsers.AddRange(GetInnerObjParsers());
        return parsers.ToArray();
    }

    private IXmlParser GetObjParser() =>
        ((XmlObjectBuilder)_xmlObjBuilder).CreateXmlElementParser();

    protected virtual IEnumerable<IXmlParser> GetInnerObjParsers() =>
        XmlInnerObjBuilder.Select(
            builder => ((XmlObjectBuilder)builder)
                .CreateXmlElementParser()).ToArray();
}