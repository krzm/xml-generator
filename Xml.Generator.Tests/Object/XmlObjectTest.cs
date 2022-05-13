using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Xml.Generator.Tests;

public class XmlObjectTest
    : IClassFixture<LogFixture>
{
    private readonly LogFixture _logFixture;
    private readonly ITestUtils _utils;

    public XmlObjectTest(LogFixture logFixture)
    {
        _logFixture = logFixture;
        _logFixture.FileName = nameof(XmlObjectTest);
        _utils = _logFixture.Utils;
    }

    [Theory, ClassData(typeof(XmlObjectData))]
    public void TestXmlObject(Dictionary<XmlObjectParts, string> xmlBlocks, string expected)
    {
        var actual = new XmlObjectBuilder(xmlBlocks, (prop) => new XmlPropertyParser(prop), true).CreateXml().Text;

        _utils.Log(_utils.CreateLog(nameof(TestXmlObject), expected, actual, xmlBlocks.Values.ToArray()));

        Assert.Equal(expected, actual);
    }
}