namespace Xml.Generator;

public interface IXmlBuilder<TBuildingBlocksEnum>
{
    IText CreateXml();
}