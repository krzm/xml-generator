using System.Text;

namespace Xml.Generator;

public class XmlCollection
    : XmlCollectionElement
{
    private readonly StringBuilder? stringBuilder;
    private string? text;

    public override string Text
    {
        get
        {
            if (string.IsNullOrWhiteSpace(text) == false) 
                return text;
            ArgumentNullException.ThrowIfNull(stringBuilder);
            stringBuilder.Clear();
            stringBuilder.Append(XmlStart?.Text);
            foreach (var obj in XmlObjects)
            {
                stringBuilder.Append(obj.Text);
            }
            stringBuilder.Append(XmlEnd?.Text);
            text = stringBuilder.ToString();
            return text;
        }
    }

    public XmlCollection(IXmlParser xmlParser, params IText[] xmlObjects)
        : base(xmlParser, xmlObjects) =>
            stringBuilder = new StringBuilder();
}