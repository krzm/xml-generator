using Xunit;

namespace Xml.Generator.Tests;

public class XmlStartTest
    : IClassFixture<LogFixture>
{
    private readonly LogFixture _logFixture;
    private readonly ITestUtils _utils;

    public XmlStartTest(LogFixture logFixture)
    {
        _logFixture = logFixture;
        _logFixture.FileName = nameof(XmlStartTest);
        _utils = _logFixture.Utils;
    }

    [Theory]
    [InlineData("", "<>")]
    [InlineData("test", "<test>")]
    public void TestXmlStart(string name, string expected)
    {
        var actual = new XmlStart(new XmlElementParser(name)).Text;

        _utils.Log(_utils.CreateLog(nameof(TestXmlStart), expected, actual, name));

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("", "", "<>")]
    [InlineData("  ", "test", "  <test>")]
    public void TestXmlStartPrefix(string prefix, string name, string expected)
    {
        var actual = new XmlStart(new XmlElementParser(prefix, name)).Text;

        _utils.Log(_utils.CreateLog(nameof(TestXmlStartPrefix), expected, actual, prefix, name));

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("", "", "", "<>")]
    [InlineData("  ", "test", "\r\n", "  <test>\r\n")]
    public void TestXmlStartEnds(string prefix, string name, string postfix, string expected)
    {
        var actual = new XmlStart(new XmlElementParser(prefix, name, postfix)).Text;

        _utils.Log(_utils.CreateLog(nameof(TestXmlStartEnds), expected, actual, prefix, name, postfix));

        Assert.Equal(expected, actual);
    }
}