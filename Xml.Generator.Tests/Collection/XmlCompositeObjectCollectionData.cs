using System.Collections;
using System.Collections.Generic;

namespace Xml.Generator.Tests;

internal class XmlCompositeObjectCollectionData
    : IEnumerable<object[]>
{
    private readonly List<object[]> _data;
    private readonly IDictionary<XmlCollectionParts, string> _xmlCollectionParts;
    private readonly IDictionary<XmlObjectParts, string> _xmlObjectParts;
    private readonly IDictionary<XmlObjectParts, string> _xmlInnerObjectParts;

    public XmlCompositeObjectCollectionData()
    {
        _data = new List<object[]>();
        _xmlCollectionParts = new Dictionary<XmlCollectionParts, string> {
                { XmlCollectionParts.Name, "Collection1" }
                , { XmlCollectionParts.Prefix1, "" }
                , { XmlCollectionParts.Postfix1, "\r\n" }
                , { XmlCollectionParts.Prefix2, "" }
                , { XmlCollectionParts.Postfix2, "" }};
        _xmlObjectParts = new Dictionary<XmlObjectParts, string> {
                { XmlObjectParts.Empty, "" },
                { XmlObjectParts.ObjectPrefix, "  " },
                { XmlObjectParts.ObjectName, "ObjectName1" },
                { XmlObjectParts.PropPrefix, "    " },
                { XmlObjectParts.Property1, "PropertyName1" },
                { XmlObjectParts.Value1, "Value1" },
                { XmlObjectParts.Property2, "PropertyName2" },
                { XmlObjectParts.Value2, "Value2" },
                { XmlObjectParts.NewLine, "\r\n" }};
        _xmlInnerObjectParts = new Dictionary<XmlObjectParts, string>(_xmlObjectParts)
            {
                { XmlObjectParts.ObjectPosition, "0" }
            };
        _xmlInnerObjectParts[XmlObjectParts.ObjectPrefix] = "    ";
        _xmlInnerObjectParts[XmlObjectParts.PropPrefix] = "      ";
        _data.Add(GetDataCase1());
    }

    private object[] GetDataCase1() =>
        new object[]
        {
                _xmlCollectionParts,
                _xmlObjectParts,
                _xmlInnerObjectParts,
                "<Collection1>\r\n" +
                "  <ObjectName1>\r\n" +
                "    <ObjectName1>\r\n" +
                "      <PropertyName1>Value1</PropertyName1>\r\n" +
                "      <PropertyName2>Value2</PropertyName2>\r\n" +
                "    </ObjectName1>\r\n" +
                "    <PropertyName1>Value1</PropertyName1>\r\n" +
                "    <PropertyName2>Value2</PropertyName2>\r\n" +
                "  </ObjectName1>\r\n" +
                "  <ObjectName1>\r\n" +
                "    <ObjectName1>\r\n" +
                "      <PropertyName1>Value1</PropertyName1>\r\n" +
                "      <PropertyName2>Value2</PropertyName2>\r\n" +
                "    </ObjectName1>\r\n" +
                "    <PropertyName1>Value1</PropertyName1>\r\n" +
                "    <PropertyName2>Value2</PropertyName2>\r\n" +
                "  </ObjectName1>\r\n" +
                "</Collection1>"
        };

    public IEnumerator<object[]> GetEnumerator() { return _data.GetEnumerator(); }

    IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
}