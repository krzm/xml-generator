using System.Collections;
using System.Collections.Generic;

namespace Xml.Generator.Tests;

public class XmlCollectionFileData
    : IEnumerable<object[]>
{
    private readonly List<object[]> _data;
    private readonly IDictionary<XmlFileParts, string> _xmlFileParts;
    private readonly IDictionary<XmlCollectionParts, string> _xmlCollectionParts;
    private readonly IDictionary<XmlObjectParts, string> _xmlObjectParts;
    private readonly IDictionary<XmlObjectParts, string> _xmlInnerObjectParts;

    public XmlCollectionFileData()
    {
        _data = new List<object[]>();
        _xmlFileParts = new Dictionary<XmlFileParts, string> {
                { XmlFileParts.Prefix, "" },
                { XmlFileParts.Header, "<?xml version=\"1.0\" encoding=\"utf-8\"?>" },
                { XmlFileParts.Postfix, "\r\n" }};
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
                { XmlObjectParts.NewLine, "\r\n" }}; ;
        _xmlInnerObjectParts = new Dictionary<XmlObjectParts, string>(_xmlObjectParts)
        {
            [XmlObjectParts.ObjectPrefix] = "    ",
            [XmlObjectParts.PropPrefix] = "      "
        };
        _data.Add(GetDataCase1());
    }

    private object[] GetDataCase1() =>
        new object[]
        {
                _xmlFileParts
                , _xmlCollectionParts
                , _xmlObjectParts
                , _xmlInnerObjectParts
                , "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" +
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