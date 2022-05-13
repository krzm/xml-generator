using System.Collections;
using System.Collections.Generic;

namespace Xml.Generator.Tests;

public class XmlObjectData
    : IEnumerable<object[]>
{
    private readonly List<object[]> _data;
    private readonly IDictionary<XmlObjectParts, string> _xmlObjectParts;

    public XmlObjectData()
    {
        _data = new List<object[]>();
        _xmlObjectParts = new Dictionary<XmlObjectParts, string> {
                { XmlObjectParts.Empty, "" },
                { XmlObjectParts.ObjectPrefix, "" },
                { XmlObjectParts.ObjectName, "ObjectName" },
                { XmlObjectParts.PropPrefix, "  " },
                { XmlObjectParts.Property1, "Property1Name" },
                { XmlObjectParts.Value1, "Value1" },
                { XmlObjectParts.Property2, "Property2Name" },
                { XmlObjectParts.Value2, "Value2" },
                { XmlObjectParts.NewLine, "\r\n" }};
        _data.Add(GetDataCase1());
        _data.Add(GetDataCase2());
    }

    private object[] GetDataCase1() =>
        new object[]
        {
                _xmlObjectParts,
                "<ObjectName>\r\n" +
                "  <Property1Name>Value1</Property1Name>\r\n" +
                "  <Property2Name>Value2</Property2Name>\r\n" +
                "</ObjectName>\r\n"
        };

    private object[] GetDataCase2() =>
        new object[]
        {
                new Dictionary<XmlObjectParts, string>(_xmlObjectParts)
                {
                    [XmlObjectParts.ObjectName] = "Object2Name",
                    [XmlObjectParts.ObjectPrefix] = "  ",
                    [XmlObjectParts.PropPrefix] = "    "
                },
                "  <Object2Name>\r\n" +
                "    <Property1Name>Value1</Property1Name>\r\n" +
                "    <Property2Name>Value2</Property2Name>\r\n" +
                "  </Object2Name>\r\n"
        };

    public IEnumerator<object[]> GetEnumerator() { return _data.GetEnumerator(); }

    IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
}