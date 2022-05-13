namespace Xml.Generator;

public class XmlObjectNumbered
    : XmlObject
{
    public XmlObjectNumbered(
        params IXmlParser[] xmlBuilders)
            : base(xmlBuilders)
    {
    }

    protected override void BuildStart()
    {
        if (XmlStart is IOrderedText orderedXmlStart)
            Lines.Insert(0, orderedXmlStart.OrderedText);
    }

    protected override void BuildProperties()
    {
        ArgumentNullException.ThrowIfNull(XmlProperties);
        foreach (var property in XmlProperties)
        {
            if (property is IOrderedText orderedProperty)
                Lines.Add(orderedProperty.OrderedText);
        }
    }

    protected override void BuildEnd()
    {
        if (XmlEnd is IOrderedText orderedXmlEnd)
            Lines.Add(orderedXmlEnd.OrderedText);
    }

    protected override void BuildInnerObjects()
    {
        ArgumentNullException.ThrowIfNull(XmlObjects);
        for (int i = 0; i < XmlObjects.Length; i++)
        {
            if (XmlObjects[i] is IOrderedText orderedXmlObjText)
                Lines.Insert(InnerObjectPosition + i, orderedXmlObjText.OrderedText);
        }
    }

    protected override void BuildString()
    {
        var ordered = new Dictionary<int, string>();
        StringBuilder.Clear();

        foreach (var line in Lines)
        {
            var number = ParseNumber(line);
            ordered.Add(number, line);
        }

        var kyes = ordered.Keys.ToArray();
        var min = kyes.Min();
        var max = kyes.Max();
        for (int i = min; i <= max; i++)
        {
            StringBuilder.Append(ordered[i]);
        }
    }

    private int ParseNumber(string line)
    {
        var lineNr = line.Split('|')[0].TrimEnd();
        var nr = int.Parse(lineNr);
        return nr;
    }
}