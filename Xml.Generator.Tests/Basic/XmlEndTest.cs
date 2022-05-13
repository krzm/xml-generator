using Xunit;

namespace Xml.Generator.Tests;

public class XmlEndTest
    : IClassFixture<LogFixture>
{
    private readonly LogFixture _logFixture;
    private readonly ITestUtils _utils;

    public XmlEndTest(LogFixture logFixture)
    {
        _logFixture = logFixture;
        _logFixture.FileName = nameof(XmlEndTest);
        _utils = _logFixture.Utils;
    }

    [Theory]
    [InlineData("", "</>")]
    [InlineData("test", "</test>")]
    public void TestXmlEnd(string name, string expected)
    {
        var actual = new XmlEnd(new XmlElementParser(name)).Text;

        _utils.Log(_utils.CreateLog(nameof(TestXmlEnd), expected, actual, name));

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("", "", "</>")]
    [InlineData("  ", "test", "  </test>")]
    public void TestXmlEndPrefix(string prefix, string name, string expected)
    {
        var actual = new XmlEnd(new XmlElementParser(prefix, name)).Text;

        _utils.Log(_utils.CreateLog(nameof(TestXmlEndPrefix), expected, actual, prefix, name));

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("", "", "", "</>")]
    [InlineData("  ", "test", "\r\n", "  </test>\r\n")]
    public void TesXmlEnd(string prefix, string name, string postfix, string expected)
    {
        var actual = new XmlEnd(new XmlElementParser(prefix, name, postfix)).Text;

        _utils.Log(_utils.CreateLog(nameof(TesXmlEnd), expected, actual, prefix, name, postfix));

        Assert.Equal(expected, actual);
    }
}