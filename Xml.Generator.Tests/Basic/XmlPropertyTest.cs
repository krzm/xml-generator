using Xunit;

namespace Xml.Generator.Tests;

public class XmlPropertyTest
    : IClassFixture<LogFixture>
{
    private readonly LogFixture _logFixture;
    private readonly ITestUtils _utils;

    public XmlPropertyTest(LogFixture logFixture)
    {
        _logFixture = logFixture;
        _logFixture.FileName = nameof(XmlPropertyTest);
        _utils = _logFixture.Utils;
    }

    [Theory]
    [InlineData("", "name", "value", "", "<name>value</name>")]
    [InlineData("  ", "Weight", "60", "\r\n", "  <Weight>60</Weight>\r\n")]
    public void TestXmlProperty(string prefix, string name, string value, string postfix, string expected)
    {
        var actual = new XmlPropertyText(new XmlPropertyParser(prefix, name, value, postfix)).Text;

        _utils.Log(_utils.CreateLog(nameof(TestXmlProperty), expected, actual, prefix, name, value, postfix, expected));

        Assert.Equal(expected, actual);
    }
}