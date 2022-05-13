namespace Xml.Generator;

public class XmlStart : XmlElement, IOrderedText
{
    public override string Text => $"{Prefix?.Text}<{Name?.Text}>{Postfix?.Text}";

    public string OrderedText => $"{LineNumber?.Text}{Text}";

    public XmlStart(IXmlParser xmlBuilder) : base(xmlBuilder) { }
}