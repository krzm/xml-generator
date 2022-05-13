namespace Xml.Generator;

public class XmlObjectBuilder : XmlBuilder<XmlObjectParts>
{
    protected readonly Func<string[], IXmlParser> PropertyParserFactory;
    protected readonly bool IsNewLineAfter;

    public XmlObjectBuilder(
        IDictionary<XmlObjectParts, string> parts
        , Func<string[], IXmlParser> propertyParserFactory
        , bool isNewLineAfter = false) : base(parts)
    {
        PropertyParserFactory = propertyParserFactory ?? throw new ArgumentNullException(nameof(propertyParserFactory));
        IsNewLineAfter = isNewLineAfter;
    }

    public override IText CreateXml()
    {
        return new XmlObject(
            CreateXmlElementParser()); ;
    }

    public virtual XmlObjectParser CreateXmlElementParser() =>
        new XmlObjectParser(
            CreateProperties()
            , PropertyParserFactory
            , new XmlElementParser(
                BuildingBlocks[XmlObjectParts.ObjectPrefix]
                , BuildingBlocks[XmlObjectParts.ObjectName]
                , BuildingBlocks[XmlObjectParts.NewLine])
            , new XmlElementParser(
                BuildingBlocks[XmlObjectParts.ObjectPrefix]
                , BuildingBlocks[XmlObjectParts.ObjectName]
                , IsNewLineAfter ? BuildingBlocks[XmlObjectParts.NewLine] : BuildingBlocks[XmlObjectParts.Empty]));

    protected string[][] CreateProperties()
    {
        var list = new List<string[]>();
        if (!BuildingBlocks.ContainsKey(XmlObjectParts.PropPrefix) ||
            !BuildingBlocks.ContainsKey(XmlObjectParts.NewLine)) return list.ToArray();
        AddProperties(list);
        return list.ToArray();
    }

    protected virtual void AddProperties(List<string[]> list)
    {
        AddProperty(list, XmlObjectParts.Property1, XmlObjectParts.Value1);
        AddProperty(list, XmlObjectParts.Property2, XmlObjectParts.Value2);
        AddProperty(list, XmlObjectParts.Property3, XmlObjectParts.Value3);
    }

    private void AddProperty(
        List<string[]> list,
        XmlObjectParts property,
        XmlObjectParts value)
    {
        if (BuildingBlocks.ContainsKey(property) && BuildingBlocks.ContainsKey(value))
            list.Add(CreateProperty(property, value));
    }

    private string[] CreateProperty(
        XmlObjectParts property,
        XmlObjectParts value) =>
            new string[]
            {
                    BuildingBlocks[XmlObjectParts.PropPrefix],
                    BuildingBlocks[property],
                    BuildingBlocks[value],
                    BuildingBlocks[XmlObjectParts.NewLine]
            };
}