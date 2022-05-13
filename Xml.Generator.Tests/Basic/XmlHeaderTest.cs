using Xunit;

namespace Xml.Generator.Tests;

public class XmlHeaderTest
    : IClassFixture<LogFixture>
{
    private readonly LogFixture _logFixture;
    private readonly ITestUtils _utils;

    public XmlHeaderTest(LogFixture logFixture)
    {
        _logFixture = logFixture;
        _logFixture.FileName = nameof(XmlHeaderTest);
        _utils = _logFixture.Utils;
    }

    [Theory]
    [InlineData("<?xml version=\"1.0\" encoding=\"utf-8\"?>")]
    public void TestXmlHeader(string expected)
    {
        var actual = new XmlHeader(new XmlHeaderParser(string.Empty)).Text;

        _utils.Log(_utils.CreateLog(nameof(TestXmlHeader), expected, actual));

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("", "<?xml version=\"1.0\" encoding=\"utf-8\"?>")]
    [InlineData("  ", "  <?xml version=\"1.0\" encoding=\"utf-8\"?>")]
    public void TestXmlHeaderPrefix(string prefix, string expected)
    {
        var actual = new XmlHeader(new XmlHeaderParser(prefix)).Text;

        _utils.Log(_utils.CreateLog(nameof(TestXmlHeaderPrefix), expected, actual, prefix));

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("", "", "<?xml version=\"1.0\" encoding=\"utf-8\"?>")]
    [InlineData("  ", "\r\n", "  <?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n")]
    public void TestXmlHeaderEnds(string prefix, string postfix, string expected)
    {
        var actual = new XmlHeader(new XmlHeaderParser(prefix, postfix)).Text;

        _utils.Log(_utils.CreateLog(nameof(TestXmlHeaderEnds), expected, actual, prefix, postfix));

        Assert.Equal(expected, actual);
    }
}