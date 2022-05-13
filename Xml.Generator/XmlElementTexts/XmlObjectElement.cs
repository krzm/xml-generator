namespace Xml.Generator;

public abstract class XmlObjectElement : XmlElementsText
{
    protected IText? XmlStart;
    protected IText[]? XmlObjects;
    protected IText[]? XmlProperties;
    protected IText? XmlEnd;

    protected XmlObjectElement(params IXmlParser[] xmlParsers) : base(xmlParsers)
    {
        if (XmlParsers.Length == 1)
        {
            XmlObjects = new IText[] { };
            CreateMainObjectTextElements();
        }
        else if (XmlParsers.Length > 1)
        {
            CreateMainObjectTextElements();
            CreateInnerObjectsTextElements(xmlParsers);
        }
    }

    private void CreateMainObjectTextElements()
    {
        var mainObjectParser = XmlParsers[0];
        mainObjectParser.CreateXmlTextObjects();
        var xmlProps = new List<IText>();
        ArgumentNullException.ThrowIfNull(mainObjectParser.TextObjects);
        foreach (var textObj in mainObjectParser.TextObjects)
        {
            if (textObj is XmlStart)
                XmlStart = textObj;
            if (textObj is XmlPropertyText)
                xmlProps.Add(textObj);
            if (textObj is XmlEnd)
                XmlEnd = textObj;
        }
        XmlProperties = xmlProps.ToArray();
    }

    private void CreateInnerObjectsTextElements(IXmlParser[] xmlParsers)
    {
        var xmlObjects = new List<IText>();
        foreach (var xmlParser in xmlParsers.Skip(1))
        {
            xmlParser.CreateXmlTextObjects();
            ArgumentNullException.ThrowIfNull(xmlParser.TextObjects);
            xmlObjects.AddRange(xmlParser.TextObjects);
        }
        XmlObjects = xmlObjects.ToArray();
    }
}