namespace Xml.Generator;

public class XmlFileBuilder
    : XmlBuilder<XmlFileParts>
{
    private readonly IText[] _texts;
    private readonly Func<string[], IXmlParser> _xmlFileParserFactory;

    public XmlFileBuilder(
        IText[] texts
        , Func<string[], IXmlParser> xmlFileParserFactory
        , IDictionary<XmlFileParts, string> buildingBlocks) : base(buildingBlocks)
    {
        _texts = texts ?? throw new ArgumentNullException(nameof(texts));
        _xmlFileParserFactory = xmlFileParserFactory ?? throw new ArgumentNullException(nameof(xmlFileParserFactory));
    }

    public override IText CreateXml()
    {
        return CreateXmlElement();
    }

    private XmlFile CreateXmlElement() =>
        new XmlFile(
            new IXmlParser[]
            {
                    _xmlFileParserFactory(SelectParts())
            },
            _texts);

    protected virtual string[] SelectParts()
    {
        return new string[]
        {
                BuildingBlocks[XmlFileParts.Prefix]
                , BuildingBlocks[XmlFileParts.Postfix]
        };
    }
}