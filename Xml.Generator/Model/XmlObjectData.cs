namespace Xml.Generator;

public class XmlObjectData
{
    public int Order { get; }

    public string StartDelimiter { get; }

    public string EndDelimiter { get; }

    public string Name { get; }

    public IEnumerable<XmlProperty> Properties { get; }

    public XmlObjectData(string name,
        string startDelimiter, string endDelimiter,
        IEnumerable<XmlProperty> properties,
        int order)
    {
        Name = name;
        StartDelimiter = startDelimiter;
        EndDelimiter = endDelimiter;
        Properties = properties;
        Order = order;
    }
}