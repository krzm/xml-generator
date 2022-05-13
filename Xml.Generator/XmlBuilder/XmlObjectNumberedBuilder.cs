namespace Xml.Generator;

public class XmlObjectNumberedBuilder : XmlObjectWithPropsBuilder
{
    public XmlObjectNumberedBuilder(
        IDictionary<XmlObjectParts, string> parts
        , IEnumerable<XmlProperty> xmlPropertyData
        , Func<string[], IXmlParser> propertyParserFactory
        , bool isNewLineAfter = false) :
            base(parts, xmlPropertyData, propertyParserFactory, isNewLineAfter)
    {
    }

    public override XmlObjectParser CreateXmlElementParser() =>
        new XmlObjectParser(
            CreateProperties()
            , PropertyParserFactory
            , new XmlElementNumberedParser(
                BuildingBlocks[XmlObjectParts.ObjectStartLineNr]
                , BuildingBlocks[XmlObjectParts.ObjectPrefix]
                , BuildingBlocks[XmlObjectParts.ObjectName]
                , BuildingBlocks[XmlObjectParts.NewLine])
            , new XmlElementNumberedParser(
                BuildingBlocks[XmlObjectParts.ObjectStopLineNr]
                , BuildingBlocks[XmlObjectParts.ObjectPrefix]
                , BuildingBlocks[XmlObjectParts.ObjectName]
                , IsNewLineAfter ? BuildingBlocks[XmlObjectParts.NewLine] : BuildingBlocks[XmlObjectParts.Empty]));
}