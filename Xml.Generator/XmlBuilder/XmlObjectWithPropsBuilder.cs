namespace Xml.Generator;

public class XmlObjectWithPropsBuilder : XmlObjectBuilder
{
    private readonly IEnumerable<XmlProperty> _xmlPropertyData;

    public XmlObjectWithPropsBuilder(
        IDictionary<XmlObjectParts, string> parts
        , IEnumerable<XmlProperty> xmlPropertyData
        , Func<string[], IXmlParser> propertyParserFactory
        , bool isNewLineAfter) : base(parts, propertyParserFactory, isNewLineAfter) =>
        _xmlPropertyData = xmlPropertyData ?? throw new ArgumentNullException(nameof(xmlPropertyData));

    protected override void AddProperties(List<string[]> list)
    {
        foreach (var xmlProp in _xmlPropertyData)
        {
            list.Add(
                new string[]
                {
                        xmlProp.Order.ToString(),
                        xmlProp.StartDelimiter,
                        xmlProp.Name,
                        xmlProp.Value,
                        xmlProp.EndDelimiter
                });
        }
    }
}