namespace Xml.Generator;

public class XmlPropertyNumberedParser : XmlParser
{
    public XmlPropertyNumberedParser(params string[] texts) : base(texts) { }

    public override void CreateXmlTextObjects()
    {
        switch (Texts.Length)
        {
            case 5:
                Parse5Case();
                break;
            default:
                throw new ArgumentException(nameof(XmlPropertyParser));
        }
    }

    private void Parse5Case()
    {
        var lineNr = Texts[0];
        var prefix = Texts[1];
        var name = Texts[2];
        var value = Texts[3];
        var postfix = Texts[4];
        if (!string.IsNullOrEmpty(value))
            TextObjects = new IText[]
            {
                    new XmlLineNumber(lineNr)
                    , new XmlStart(new XmlElementParser(prefix, name))
                    , new XmlValue(value)
                    , new XmlEnd(new XmlElementParser(string.Empty, name, postfix))
            };
        else
            TextObjects = new IText[]
            {
                    new XmlLineNumber(lineNr)
                    , new XmlStartClosed(new XmlElementParser(prefix, name, postfix))
            };
    }
}