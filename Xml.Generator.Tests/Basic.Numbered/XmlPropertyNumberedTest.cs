using System;
using Xunit;

namespace Xml.Generator.Tests;

public class XmlPropertyNumberedTest
    : IClassFixture<LogFixture>
{
    private readonly LogFixture _logFixture;
    private readonly ITestUtils _utils;

    public XmlPropertyNumberedTest(LogFixture logFixture)
    {
        _logFixture = logFixture;
        _logFixture.FileName = nameof(XmlPropertyNumberedTest);
        _utils = _logFixture.Utils;
    }

    [Theory]
    [InlineData("", "name", "value", "", "1	|<name>value</name>")]
    [InlineData("  ", "Weight", "60", "\r\n", "1	|  <Weight>60</Weight>\r\n")]
    public void XmlPropertyTest(string prefix, string name, string value, string postfix, string expected)
    {
        IText xmlElement = new XmlPropertyText(new XmlPropertyNumberedParser("1", prefix, name, value, postfix));
        var actual = (xmlElement as IOrderedText)?.OrderedText;
        ArgumentNullException.ThrowIfNull(actual);
        _utils.Log(_utils.CreateLog(nameof(XmlPropertyTest), expected, actual, prefix, name, value, postfix));
        Assert.Equal(expected, actual);
    }
}