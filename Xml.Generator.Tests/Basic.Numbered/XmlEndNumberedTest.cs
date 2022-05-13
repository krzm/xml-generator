using System;
using Xunit;

namespace Xml.Generator.Tests;

public class XmlEndNumberedTest
    : IClassFixture<LogFixture>
{
    private readonly LogFixture logFixture;
    private readonly ITestUtils utils;

    public XmlEndNumberedTest(LogFixture logFixture)
    {
        this.logFixture = logFixture;
        this.logFixture.FileName = nameof(Tests.XmlEndNumberedTest);
        utils = this.logFixture.Utils;
    }

    [Theory]
    [InlineData("", "1	|</>")]
    [InlineData("test", "1	|</test>")]
    public void TestXmlEnd(string name, string expected)
    {
        IText xmlElement = new XmlEnd(new XmlElementNumberedParser("1", name));
        var actual = (xmlElement as IOrderedText)?.OrderedText;
        ArgumentNullException.ThrowIfNull(actual);
        utils.Log(utils.CreateLog(nameof(TestXmlEnd), expected, actual, name));
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("", "", "1	|</>")]
    [InlineData("  ", "test", "1	|  </test>")]
    public void TestXmlEndPrefix(string prefix, string name, string expected)
    {
        IText xmlElement = new XmlEnd(new XmlElementNumberedParser("1", prefix, name));
        var actual = (xmlElement as IOrderedText)?.OrderedText;
        ArgumentNullException.ThrowIfNull(actual);
        utils.Log(utils.CreateLog(nameof(TestXmlEndPrefix), expected, actual, prefix, name));
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("", "", "", "1	|</>")]
    [InlineData("  ", "test", "\r\n", "1	|  </test>\r\n")]
    public void TestXmlEndEnds(string prefix, string name, string postfix, string expected)
    {
        IText xmlElement = new XmlEnd(new XmlElementNumberedParser("1", prefix, name, postfix));
        var actual = (xmlElement as IOrderedText)?.OrderedText;
        ArgumentNullException.ThrowIfNull(actual);
        utils.Log(utils.CreateLog(nameof(TestXmlEndEnds), expected, actual, prefix, name, postfix));
        Assert.Equal(expected, actual);
    }
}