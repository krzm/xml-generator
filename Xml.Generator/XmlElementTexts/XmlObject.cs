using System.Text;

namespace Xml.Generator;

public class XmlObject
    : XmlObjectElement
{
    protected readonly StringBuilder StringBuilder;
    protected string? ObjectText;
    protected List<string> Lines;

    public int InnerObjectPosition { get; set; }

    public override string Text
    {
        get
        {
            if (!string.IsNullOrWhiteSpace(ObjectText)) return ObjectText;
            BuildLines();
            BuildString();
            ObjectText = StringBuilder.ToString();
            return ObjectText;
        }
    }

    public XmlObject(params IXmlParser[] parsers) : base(parsers)
    {
        StringBuilder = new StringBuilder();
        Lines = new List<string>();
    }

    private void BuildLines()
    {
        Lines.Clear();
        BuildProperties();
        BuildInnerObjects();
        BuildStart();
        BuildEnd();
    }

    protected virtual void BuildStart()
    {
        ArgumentNullException.ThrowIfNull(XmlStart);
        ArgumentNullException.ThrowIfNull(XmlStart.Text);
        Lines.Insert(0, XmlStart.Text);
    }

    protected virtual void BuildProperties() =>
        Lines.AddRange(from property in XmlProperties select property.Text);

    protected virtual void BuildInnerObjects()
    {
        for (int i = 0; i < XmlObjects?.Length; i++)
        {
            Lines.Insert(InnerObjectPosition + i, XmlObjects[i].Text);
        }
    }

    protected virtual void BuildEnd()
    {
        ArgumentNullException.ThrowIfNull(XmlEnd);
        ArgumentNullException.ThrowIfNull(XmlEnd.Text);
        Lines.Add(XmlEnd.Text);
    }

    protected virtual void BuildString()
    {
        StringBuilder.Clear();
        foreach (var item in Lines)
        {
            StringBuilder.Append(item);
        }
    }
}