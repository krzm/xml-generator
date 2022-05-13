namespace Xml.Generator;

public class XmlStartClosed : XmlElement, IOrderedText
{
    public override string Text => $"{Prefix?.Text}<{Name?.Text} />{Postfix?.Text}";

    public string OrderedText => $"{LineNumber}{Text}";

    public XmlStartClosed(IXmlParser xmlBuilder) : base(xmlBuilder) { }
}