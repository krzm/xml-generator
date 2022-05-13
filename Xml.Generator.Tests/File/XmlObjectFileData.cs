using System.Collections;
using System.Collections.Generic;

namespace Xml.Generator.Tests;

public class XmlObjectFileData
    : IEnumerable<object[]>
{
    private readonly List<object[]> _data;
    private readonly IDictionary<XmlFileParts, string> _xmlFileParts;
    private readonly IDictionary<XmlObjectParts, string> _xmlObjectParts;

    public XmlObjectFileData()
    {
        _data = new List<object[]>();
        _xmlFileParts = new Dictionary<XmlFileParts, string> {
                { XmlFileParts.Prefix, "" },
                { XmlFileParts.Header, "<?xml version=\"1.0\" encoding=\"utf-8\"?>" },
                { XmlFileParts.Postfix, "\r\n" }};
        _xmlObjectParts = new Dictionary<XmlObjectParts, string> {
                { XmlObjectParts.Empty, "" },
                { XmlObjectParts.ObjectPrefix, "" },
                { XmlObjectParts.ObjectName, "ObjectName" },
                { XmlObjectParts.PropPrefix, "  " },
                { XmlObjectParts.Property1, "Property1Name" },
                { XmlObjectParts.Value1, "Value1" },
                { XmlObjectParts.Property2, "Property2Name" },
                { XmlObjectParts.Value2, "Value2" },
                { XmlObjectParts.NewLine, "\r\n" }}; ;
        _data.Add(GetDataCase1());
    }

    private object[] GetDataCase1() =>
        new object[]
        {
                _xmlFileParts,
                _xmlObjectParts,
                "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" +
                "<ObjectName>\r\n" +
                "  <Property1Name>Value1</Property1Name>\r\n" +
                "  <Property2Name>Value2</Property2Name>\r\n" +
                "</ObjectName>\r\n"
        };

    public IEnumerator<object[]> GetEnumerator() { return _data.GetEnumerator(); }

    IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
}