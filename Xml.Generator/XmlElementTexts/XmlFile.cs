using System.Text;

namespace Xml.Generator;

public class XmlFile : XmlFileElement
{
    private readonly StringBuilder _stringBuilder;
    private string _fileText;

    public override string Text
    {
        get
        {
            if (!string.IsNullOrWhiteSpace(_fileText)) return _fileText;
            _stringBuilder.Clear();
            if (XmlHeader is IOrderedText orderedXmlHeader)
                _stringBuilder.Append(orderedXmlHeader.OrderedText);
            foreach (var xmlElement in XmlElements)
            {
                _stringBuilder.Append(xmlElement.Text);
            }
            _fileText = _stringBuilder.ToString();
            return _fileText;
        }
    }

    public XmlFile(IXmlParser[] xmlParsers, params IText[] xmlElements) : base(xmlParsers, xmlElements) =>
        _stringBuilder = new StringBuilder();
}