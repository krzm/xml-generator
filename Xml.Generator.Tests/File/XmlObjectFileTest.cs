using System.Collections.Generic;
using Xunit;

namespace Xml.Generator.Tests;

public class XmlObjectFileTest
    : IClassFixture<LogFixture>
{
    private readonly LogFixture _logFixture;
    private readonly ITestUtils _utils;

    public XmlObjectFileTest(LogFixture logFixture)
    {
        _logFixture = logFixture;
        _logFixture.FileName = nameof(XmlObjectFileTest);
        _utils = _logFixture.Utils;
    }

    [Theory, ClassData(typeof(XmlObjectFileData))]
    public void TestXmlObjectFile(
        Dictionary<XmlFileParts, string> xmlFileParts,
        Dictionary<XmlObjectParts, string> xmlObjectParts,
        string expected)
    {
        var actual = SetupTest(xmlFileParts, xmlObjectParts).CreateXml().Text;

        LogTest(xmlFileParts, xmlObjectParts, expected, actual);

        Assert.Equal(expected, actual);
    }

    private static XmlFileBuilder SetupTest(Dictionary<XmlFileParts, string> xmlFileParts, Dictionary<XmlObjectParts, string> xmlObjectParts)
    {
        var xmlObjectBuilder = new XmlObjectBuilder(xmlObjectParts
            , (property) => new XmlPropertyParser(property)
            , true);
        var xmlObject = xmlObjectBuilder.CreateXml();
        var xmlFileBuilder = new XmlFileBuilder(
            new IText[] { xmlObject }
            , (parts) => new XmlFileParser(
                (headerParts) => new XmlHeaderParser(headerParts)
                , parts)
            , xmlFileParts);
        return xmlFileBuilder;
    }

    private void LogTest(Dictionary<XmlFileParts, string> xmlFileParts, Dictionary<XmlObjectParts, string> xmlObjectParts, string expected, string actual)
    {
        var list = new List<string>();
        list.AddRange(xmlFileParts.Values);
        list.AddRange(xmlObjectParts.Values);

        if (_utils.IsLogging)
            _utils.Log(
                _utils.CreateLog(
                    nameof(TestXmlObjectFile)
                    , expected
                    , actual
                    , list.ToArray()));
    }
}