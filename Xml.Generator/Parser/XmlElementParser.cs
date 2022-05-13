namespace Xml.Generator;

public class XmlElementParser : XmlParser
{
    public XmlElementParser(params string[] texts) : base(texts) { }

    public override void CreateXmlTextObjects()
    {
        switch (Texts.Length)
        {
            case 1:
                TextObjects = new IText[] { new XmlName(Texts[0]) };
                break;
            case 2:
                TextObjects = new IText[] { new XmlPrefix(Texts[0]), new XmlName(Texts[1]) };
                break;
            case 3:
                TextObjects = new IText[] { new XmlPrefix(Texts[0]), new XmlName(Texts[1]), new XmlPostfix(Texts[2]) };
                break;
            default:
                throw new ArgumentException(nameof(XmlElementParser));
        }
    }
}