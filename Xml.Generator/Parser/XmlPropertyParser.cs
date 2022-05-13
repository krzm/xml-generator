namespace Xml.Generator;

public class XmlPropertyParser
    : XmlParser
{
    public XmlPropertyParser(params string[] texts) : base(texts) { }

    public override void CreateXmlTextObjects()
    {
        switch (Texts.Length)
        {
            case 4:
                Parse();
                break;
            default:
                throw new ArgumentException(nameof(XmlPropertyParser));
        }
    }

    private void Parse()
    {
        var prefix = Texts[0];
        var name = Texts[1];
        var value = Texts[2];
        var postfix = Texts[3];
        if (!string.IsNullOrEmpty(value))
            TextObjects = new IText[]
            {
                    new XmlStart(new XmlElementParser(prefix, name))
                    , new XmlValue(value)
                    , new XmlEnd(new XmlElementParser(string.Empty, name, postfix))
            };
        else
            TextObjects = new IText[]
            {
                    new XmlStartClosed(new XmlElementParser(prefix, name))
            };
    }
}