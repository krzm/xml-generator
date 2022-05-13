namespace Xml.Generator;

public class XmlEnd : XmlElement, IOrderedText
{
    public override string Text => $"{Prefix?.Text}</{Name?.Text}>{Postfix?.Text}";

    public string OrderedText => $"{LineNumber?.Text}{Text}";

    public XmlEnd(IXmlParser xmlBuilder) : base(xmlBuilder) { }
}