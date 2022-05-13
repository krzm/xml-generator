namespace Xml.Generator;

public class XmlProperty 
    : IOrder
{
    public string Order { get; }

    public string StartDelimiter { get; }

    public string Name { get; }

    public string Value { get; }

    public string EndDelimiter { get; }

    public XmlProperty(
        string name
        , string value
        , string startDelimiter
        , string endDelimiter
        , string order)
    {
        Name = ValidateString(name, nameof(name));
        Value = ValidateString(value, nameof(value));
        StartDelimiter = ValidateString(startDelimiter, nameof(startDelimiter));
        EndDelimiter = ValidateString(endDelimiter, nameof(endDelimiter));
        Order = order;
    }

    private static string ValidateString(
        string text
        , string varName) => text ?? throw new ArgumentException(varName);
}