using System;
using Xunit;

namespace Xml.Generator.Tests;

public class XmlHeaderNumberedTest
    : IClassFixture<LogFixture>
{
    private readonly LogFixture _logFixture;
    private readonly ITestUtils _utils;

    public XmlHeaderNumberedTest(LogFixture logFixture)
    {
        _logFixture = logFixture;
        _logFixture.FileName = nameof(XmlHeaderNumberedTest);
        _utils = _logFixture.Utils;
    }

    [Theory]
    [InlineData("1	|<?xml version=\"1.0\" encoding=\"utf-8\"?>")]
    public void TestXmlHeader(string expected)
    {
        IText xmlElement = new XmlHeader(new XmlHeaderNumberedParser("1", string.Empty));
        var actual = (xmlElement as IOrderedText)?.OrderedText;
        ArgumentNullException.ThrowIfNull(actual);
        _utils.Log(_utils.CreateLog(nameof(TestXmlHeader), expected, actual));
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("", "1	|<?xml version=\"1.0\" encoding=\"utf-8\"?>")]
    [InlineData("  ", "1	|  <?xml version=\"1.0\" encoding=\"utf-8\"?>")]
    public void TestXmlHeaderPrefix(string prefix, string expected)
    {
        IText xmlElement = new XmlHeader(new XmlHeaderNumberedParser("1", prefix));
        var actual = (xmlElement as IOrderedText)?.OrderedText;
        ArgumentNullException.ThrowIfNull(actual);
        _utils.Log(_utils.CreateLog(nameof(TestXmlHeaderPrefix), expected, actual, prefix));
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("", "", "1	|<?xml version=\"1.0\" encoding=\"utf-8\"?>")]
    [InlineData("  ", "\r\n", "1	|  <?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n")]
    public void TestXmlHeaderEnds(string prefix, string postfix, string expected)
    {
        IText xmlElement = new XmlHeader(new XmlHeaderNumberedParser("1", prefix, postfix));
        var actual = (xmlElement as IOrderedText)?.OrderedText;
        ArgumentNullException.ThrowIfNull(actual);
        _utils.Log(_utils.CreateLog(nameof(TestXmlHeaderEnds), expected, actual, prefix, postfix));
        Assert.Equal(expected, actual);
    }
}