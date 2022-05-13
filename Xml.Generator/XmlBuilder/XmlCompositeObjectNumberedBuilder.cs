namespace Xml.Generator;

public class XmlCompositeObjectNumberedBuilder
    : XmlCompositeObjectBuilder
{
    public XmlCompositeObjectNumberedBuilder(
        Func<IXmlParser[], int, IText> xmlObjectFactory
        , IXmlBuilder<XmlObjectParts> xmlObjBuilder
        , IXmlBuilder<XmlObjectParts>[] xmlInnerObjBuilder
        , int innerObjectPosition) : base(xmlObjectFactory, xmlObjBuilder, xmlInnerObjBuilder, innerObjectPosition)
    { }

    protected override IEnumerable<IXmlParser> GetInnerObjParsers() =>
        XmlInnerObjBuilder.Select(
            builder => ((XmlObjectNumberedBuilder)builder)
                .CreateXmlElementParser()).ToArray();
}