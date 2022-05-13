namespace Xml.Generator;

public interface IXmlParser
{
    IText[]? TextObjects { get; }

    void CreateXmlTextObjects();
}