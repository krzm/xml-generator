namespace Xml.Generator;

public class XmlElementNumberedParser : XmlParser
{
    public XmlElementNumberedParser(params string[] texts) : base(texts) { }

    public override void CreateXmlTextObjects()
    {
        switch (Texts.Length)
        {
            case 2:
                TextObjects = new IText[]
                {
                        new XmlLineNumber(Texts[0])
                        , new XmlName(Texts[1])
                };
                break;
            case 3:
                TextObjects = new IText[]
                {
                        new XmlLineNumber(Texts[0])
                        , new XmlPrefix(Texts[1])
                        , new XmlName(Texts[2])
                };
                break;
            case 4:
                TextObjects = new IText[]
                {
                        new XmlLineNumber(Texts[0])
                        , new XmlPrefix(Texts[1])
                        , new XmlName(Texts[2])
                        , new XmlPostfix(Texts[3])
                };
                break;
            default:
                throw new ArgumentException(nameof(XmlElementParser));
        }
    }
}