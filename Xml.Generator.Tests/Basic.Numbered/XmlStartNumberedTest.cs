using System;
using Xunit;

namespace Xml.Generator.Tests;

public class XmlStartNumberedTest
    : IClassFixture<LogFixture>
{
    private readonly LogFixture logFixture;
    private readonly ITestUtils utils;

    public XmlStartNumberedTest(LogFixture logFixture)
    {
        this.logFixture = logFixture;
        this.logFixture.FileName = nameof(Tests.XmlStartNumberedTest);
        utils = this.logFixture.Utils;
    }

    [Theory]
    [InlineData("", "1	|<>")]
    [InlineData("test", "1	|<test>")]
    public void TestXmlStart(string name, string expected)
    {
        IText xmlElement = new XmlStart(new XmlElementNumberedParser("1", name));
        var actual = (xmlElement as IOrderedText)?.OrderedText;
        ArgumentNullException.ThrowIfNull(actual);
        utils.Log(utils.CreateLog(nameof(TestXmlStart), expected, actual, name));
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("", "", "1	|<>")]
    [InlineData("  ", "test", "1	|  <test>")]
    public void TestXmlStartPrefix(string prefix, string name, string expected)
    {
        IText xmlElement = new XmlStart(new XmlElementNumberedParser("1", prefix, name));
        var actual = (xmlElement as IOrderedText)?.OrderedText;
        ArgumentNullException.ThrowIfNull(actual);
        utils.Log(utils.CreateLog(nameof(TestXmlStartPrefix), expected, actual, prefix, name));
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("", "", "", "1	|<>")]
    [InlineData("  ", "test", "\r\n", "1	|  <test>\r\n")]
    public void TestXmlStartEnds(string prefix, string name, string postfix, string expected)
    {
        IText xmlElement = new XmlStart(new XmlElementNumberedParser("1", prefix, name, postfix));
        var actual = (xmlElement as IOrderedText)?.OrderedText;
        ArgumentNullException.ThrowIfNull(actual);
        utils.Log(utils.CreateLog(nameof(TestXmlStartEnds), expected, actual, prefix, name, postfix));
        Assert.Equal(expected, actual);
    }
}