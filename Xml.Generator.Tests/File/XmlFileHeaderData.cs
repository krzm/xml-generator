using System.Collections;
using System.Collections.Generic;

namespace Xml.Generator.Tests;

public class XmlFileHeaderData
    : IEnumerable<object[]>
{
    private readonly List<object[]> _data;
    private readonly IDictionary<XmlFileParts, string> _xmlFileParts;

    public XmlFileHeaderData()
    {
        _data = new List<object[]>();
        _xmlFileParts = new Dictionary<XmlFileParts, string> {
                { XmlFileParts.Prefix, "" },
                { XmlFileParts.Header, "<?xml version=\"1.0\" encoding=\"utf-8\"?>" },
                { XmlFileParts.Postfix, "\r\n" }};
        _data.Add(GetDataCase1());
        _data.Add(GetDataCase2());
        _data.Add(GetDataCase3());
    }

    private object[] GetDataCase1() =>
        new object[]
        {
                _xmlFileParts,
                "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n"
        };

    private object[] GetDataCase2() =>
        new object[]
        {
                new Dictionary<XmlFileParts, string>(_xmlFileParts)
                {
                    [XmlFileParts.Prefix] = "  ",
                    [XmlFileParts.Postfix] = ""
                },
                "  <?xml version=\"1.0\" encoding=\"utf-8\"?>"
        };

    private object[] GetDataCase3() =>
        new object[]
        {
                new Dictionary<XmlFileParts, string>(_xmlFileParts)
                {
                    [XmlFileParts.Prefix] = "    "
                },
                "    <?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n"
        };

    public IEnumerator<object[]> GetEnumerator() { return _data.GetEnumerator(); }

    IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
}