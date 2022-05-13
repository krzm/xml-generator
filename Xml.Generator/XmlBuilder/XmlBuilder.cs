namespace Xml.Generator;

public abstract class XmlBuilder<TBuildingBlocksEnum>
    : IXmlBuilder<TBuildingBlocksEnum>
{
    protected IDictionary<TBuildingBlocksEnum, string> BuildingBlocks;

    public XmlBuilder(IDictionary<TBuildingBlocksEnum, string> buildingBlocks)
    {
        BuildingBlocks = buildingBlocks ?? throw new ArgumentNullException(nameof(buildingBlocks));
    }

    public abstract IText CreateXml();
}