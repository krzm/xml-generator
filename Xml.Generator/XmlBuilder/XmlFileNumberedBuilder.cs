namespace Xml.Generator;

public class XmlFileNumberedBuilder : XmlFileBuilder
{
    public XmlFileNumberedBuilder(
        IText[] texts
        , Func<string[], IXmlParser> xmlFileParserFactory
        , IDictionary<XmlFileParts, string> buildingBlocks) : base(texts, xmlFileParserFactory, buildingBlocks)
    {
    }

    protected override string[] SelectParts()
    {
        var list = base.SelectParts().ToList();
        list.Insert(0, "01");
        return list.ToArray();
    }
}