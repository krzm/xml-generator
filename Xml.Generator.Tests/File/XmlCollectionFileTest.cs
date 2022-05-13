using System.Collections.Generic;
using Xunit;

namespace Xml.Generator.Tests;

public class XmlCollectionFileTest
    : IClassFixture<LogFixture>
{
    private readonly LogFixture _logFixture;
    private readonly ITestUtils _utils;

    public XmlCollectionFileTest(LogFixture logFixture)
    {
        _logFixture = logFixture;
        _logFixture.FileName = nameof(XmlCollectionFileTest);
        _utils = _logFixture.Utils;
    }

    [Theory, ClassData(typeof(XmlCollectionFileData))]
    public void TestXmlCollectionFile(
        IDictionary<XmlFileParts, string> xmlFileParts
        , IDictionary<XmlCollectionParts, string> xmlCollectionParts
        , IDictionary<XmlObjectParts, string> xmlObjectParts
        , IDictionary<XmlObjectParts, string> xmlInnerObjectParts
        , string expected)
    {
        var xmlCollectionBuilder = new XmlCompositeObjectCollectionBuilder();
        var xmlCollection = xmlCollectionBuilder.CreateXml(xmlCollectionParts, xmlObjectParts, xmlInnerObjectParts);
        var actual = new XmlFileBuilder(
            new IText[] { xmlCollection }
            , (parts) =>
                new XmlFileParser(
                    (headerParts) => new XmlHeaderParser(headerParts)
                    , parts)
            , xmlFileParts).CreateXml().Text;

        var list = new List<string>();
        list.AddRange(xmlFileParts.Values);
        list.AddRange(xmlCollectionParts.Values);

        if (_utils.IsLogging)
            _utils.Log(
                _utils.CreateLog(
                    nameof(TestXmlCollectionFile)
                    , expected
                    , actual
                    , list.ToArray()));

        Assert.Equal(expected, actual);
    }
}