namespace Xml.Generator;

public class XmlHeader
    : XmlElement
    , IOrderedText
{
    const string PlEncoding = "\"utf-8\"?";

    public override string Text => $"{Prefix?.Text}"
        + $"<?xml version=\"1.0\" encoding={PlEncoding}>"
        + $"{Postfix?.Text}";

    public string OrderedText => $"{LineNumber?.Text}{Text}";

    public XmlHeader(IXmlParser xmlParser) : base(xmlParser)
    {
    }
}