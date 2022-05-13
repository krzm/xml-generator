namespace Xml.Generator;

public class XmlHeaderParser : XmlParser
{
    public XmlHeaderParser(params string[] texts) : base(texts) { }

    public override void CreateXmlTextObjects()
    {
        switch (Texts.Length)
        {
            case 1:
                TextObjects = new IText[]
                {
                        new XmlPrefix(Texts[0])
                };
                break;
            case 2:
                TextObjects = new IText[]
                {
                        new XmlPrefix(Texts[0])
                        , new XmlPostfix(Texts[1])
                };
                break;
            default:
                throw new ArgumentException(nameof(XmlHeaderParser));
        }
    }
}