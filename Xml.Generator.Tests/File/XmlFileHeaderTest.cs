using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Xml.Generator.Tests;

public class XmlFileHeaderTest
    : IClassFixture<LogFixture>
{
    private readonly LogFixture _logFixture;
    private readonly ITestUtils _utils;

    public XmlFileHeaderTest(LogFixture logFixture)
    {
        _logFixture = logFixture;
        _logFixture.FileName = nameof(XmlFileHeaderTest);
        _utils = _logFixture.Utils;
    }

    [Theory, ClassData(typeof(XmlFileHeaderData))]
    public void TestXmlFileHeader(
        IDictionary<XmlFileParts, string> xmlBlocks
        , string expected)
    {
        var actual = SetupTest(xmlBlocks).Text;

        LogTest(xmlBlocks, expected, actual);

        Assert.Equal(expected, actual);
    }

    private static IText SetupTest(IDictionary<XmlFileParts, string> xmlBlocks)
    {
        return new XmlFileBuilder(
                        Array.Empty<IText>()
                        , (parts) => new XmlFileParser(
                            (headerParts) => new XmlHeaderParser(headerParts)
                            , parts)
                        , xmlBlocks).CreateXml();
    }

    private void LogTest(IDictionary<XmlFileParts, string> xmlBlocks, string expected, string actual)
    {
        if (_utils.IsLogging)
            _utils.Log(
                _utils.CreateLog(
                    nameof(TestXmlFileHeader)
                    , expected
                    , actual
                    , xmlBlocks.Values.ToArray()));
    }
}