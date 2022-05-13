namespace Xml.Generator;

public class XmlHeaderNumberedParser
    : XmlParser
{
    public XmlHeaderNumberedParser(params string[] texts) : base(texts) { }

    public override void CreateXmlTextObjects()
    {
        switch (Texts.Length)
        {
            case 2:
                TextObjects = new IText[]
                {
                        new XmlLineNumber(Texts[0])
                        , new XmlPrefix(Texts[1])
                };
                break;
            case 3:
                TextObjects = new IText[]
                {
                        new XmlLineNumber(Texts[0])
                        , new XmlPrefix(Texts[1])
                        , new XmlPostfix(Texts[2])
                };
                break;
            default:
                throw new ArgumentException(nameof(XmlHeaderNumberedParser));
        }
    }
}