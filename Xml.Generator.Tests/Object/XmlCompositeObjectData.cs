using System.Collections;
using System.Collections.Generic;

namespace Xml.Generator.Tests;

public class XmlCompositeObjectData
    : IEnumerable<object[]>
{
    private readonly List<object[]> _data;
    private readonly IDictionary<XmlObjectParts, string> _xmlObjectParts;
    private readonly IDictionary<XmlObjectParts, string> _xmlInnerObjectParts;

    public XmlCompositeObjectData()
    {
        _data = new List<object[]>();
        _xmlObjectParts = new Dictionary<XmlObjectParts, string>() {
                { XmlObjectParts.Empty, "" }
                , { XmlObjectParts.NewLine, "\r\n" }
                , { XmlObjectParts.ObjectPrefix, "" }
                , { XmlObjectParts.ObjectName, "ObjectName" }
                , { XmlObjectParts.PropPrefix, "  " }
                , { XmlObjectParts.Property1, "Property1Name" }
                , { XmlObjectParts.Value1, "Value1" }
                , { XmlObjectParts.Property2, "Property2Name" }
                , { XmlObjectParts.Value2, "Value2" }};
        _xmlInnerObjectParts = new Dictionary<XmlObjectParts, string>(_xmlObjectParts)
        {
            [XmlObjectParts.ObjectPosition] = "0",
            [XmlObjectParts.ObjectPrefix] = "  ",
            [XmlObjectParts.PropPrefix] = "    "
        };
        _data.Add(GetDataCase1());
        _data.Add(GetDataCase2());
        _data.Add(GetDataCase3());
    }

    private object[] GetDataCase1() =>
        new object[]
        {
                _xmlObjectParts,
                _xmlInnerObjectParts,
                "<ObjectName>\r\n" +
                "  <ObjectName>\r\n" +
                "    <Property1Name>Value1</Property1Name>\r\n" +
                "    <Property2Name>Value2</Property2Name>\r\n" +
                "  </ObjectName>\r\n" +
                "  <Property1Name>Value1</Property1Name>\r\n" +
                "  <Property2Name>Value2</Property2Name>\r\n" +
                "</ObjectName>\r\n"
        };

    private object[] GetDataCase2() =>
        new object[]
        {
                new Dictionary<XmlObjectParts, string>(_xmlObjectParts)
                {
                    [XmlObjectParts.ObjectPrefix] = "  ",
                    [XmlObjectParts.PropPrefix] = "    ",
                },
                new Dictionary<XmlObjectParts, string>(_xmlInnerObjectParts)
                {
                    [XmlObjectParts.ObjectPosition] = "1",
                    [XmlObjectParts.ObjectPrefix] = "    ",
                    [XmlObjectParts.PropPrefix] = "      "
                },
                "  <ObjectName>\r\n" +
                "    <Property1Name>Value1</Property1Name>\r\n" +
                "    <ObjectName>\r\n" +
                "      <Property1Name>Value1</Property1Name>\r\n" +
                "      <Property2Name>Value2</Property2Name>\r\n" +
                "    </ObjectName>\r\n" +
                "    <Property2Name>Value2</Property2Name>\r\n" +
                "  </ObjectName>\r\n"
        };

    private object[] GetDataCase3() =>
        new object[]
        {
                new Dictionary<XmlObjectParts, string>(_xmlObjectParts)
                {
                    [XmlObjectParts.ObjectPrefix] = "	",
                    [XmlObjectParts.PropPrefix] = "		"
                },
                new Dictionary<XmlObjectParts, string>(_xmlInnerObjectParts)
                {
                    [XmlObjectParts.ObjectPosition] = "2",
                    [XmlObjectParts.ObjectPrefix] = "		",
                    [XmlObjectParts.PropPrefix] = "			"
                },
                "	<ObjectName>\r\n" +
                "		<Property1Name>Value1</Property1Name>\r\n" +
                "		<Property2Name>Value2</Property2Name>\r\n" +
                "		<ObjectName>\r\n" +
                "			<Property1Name>Value1</Property1Name>\r\n" +
                "			<Property2Name>Value2</Property2Name>\r\n" +
                "		</ObjectName>\r\n" +
                "	</ObjectName>\r\n"
        };

    public IEnumerator<object[]> GetEnumerator() { return _data.GetEnumerator(); }

    IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
}