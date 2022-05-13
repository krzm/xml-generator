using System.Text;

namespace Xml.Generator;

public class XmlCollection
    : XmlCollectionElement
{
    private readonly StringBuilder _stringBuilder;
    private string _text;

    public override string Text
    {
        get
        {
            if (!string.IsNullOrWhiteSpace(_text)) return _text;
            _stringBuilder.Clear();
            _stringBuilder.Append(XmlStart.Text);
            foreach (var obj in XmlObjects)
            {
                _stringBuilder.Append(obj.Text);
            }
            _stringBuilder.Append(XmlEnd.Text);
            _text = _stringBuilder.ToString();
            return _text;
        }
    }

    public XmlCollection(IXmlParser xmlParser, params IText[] xmlObjects) : base(xmlParser, xmlObjects) =>
        _stringBuilder = new StringBuilder();
}