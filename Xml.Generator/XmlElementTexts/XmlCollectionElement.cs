namespace Xml.Generator;

public abstract class XmlCollectionElement : XmlElementText
{
    protected IText? XmlStart;
    protected IText[] XmlObjects;
    protected IText? XmlEnd;

    protected XmlCollectionElement(IXmlParser xmlParser, params IText[] xmlObjects) : base(xmlParser)
    {
        xmlParser.CreateXmlTextObjects();
        ArgumentNullException.ThrowIfNull(xmlParser.TextObjects);
        foreach (var textObj in xmlParser.TextObjects)
        {
            if (textObj is XmlStart)
                XmlStart = textObj;
            if (textObj is XmlEnd)
                XmlEnd = textObj;
        }
        XmlObjects = xmlObjects;
    }
}