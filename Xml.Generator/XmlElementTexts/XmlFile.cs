using System.Text;

namespace Xml.Generator;

public class XmlFile
    : XmlFileElement
{
    private readonly StringBuilder stringBuilder;
    private string? fileText;

    public override string Text
    {
        get
        {
            if (!string.IsNullOrWhiteSpace(fileText)) return fileText;
            stringBuilder.Clear();
            if (XmlHeader is IOrderedText orderedXmlHeader)
                stringBuilder.Append(orderedXmlHeader.OrderedText);
            ArgumentNullException.ThrowIfNull(XmlElements);
            foreach (var xmlElement in XmlElements)
            {
                stringBuilder.Append(xmlElement.Text);
            }
            fileText = stringBuilder.ToString();
            return fileText;
        }
    }

    public XmlFile(IXmlParser[] xmlParsers
        , params IText[] xmlElements)
            : base(xmlParsers, xmlElements) =>
                stringBuilder = new StringBuilder();
}