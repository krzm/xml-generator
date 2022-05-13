namespace Xml.Generator;

public abstract class XmlText : IText
{
    public string Text { get; }

    public XmlText(string text) => Text = text;
}