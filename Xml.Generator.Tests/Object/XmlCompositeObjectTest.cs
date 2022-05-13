using System.Collections.Generic;
using Xunit;

namespace Xml.Generator.Tests;

public class XmlCompositeObjectTest
    : IClassFixture<LogFixture>
{
    private readonly LogFixture _logFixture;
    private readonly ITestUtils _utils;

    public XmlCompositeObjectTest(LogFixture logFixture)
    {
        _logFixture = logFixture;
        _logFixture.FileName = nameof(XmlCompositeObjectTest);
        _utils = _logFixture.Utils;
    }

    [Theory, ClassData(typeof(XmlCompositeObjectData))]
    public void TestXmlCompositeObject(
        Dictionary<XmlObjectParts, string> xmlObjBlocks
        , Dictionary<XmlObjectParts, string> xmlInnerObjBlocks
        , string expected)
    {
        var xmlObjBuilder = new XmlObjectBuilder(xmlObjBlocks
            , (property) => new XmlPropertyParser(property)
            , true);
        var xmlInnerObjBuilder = new XmlObjectBuilder(xmlInnerObjBlocks
            , (property) => new XmlPropertyParser(property)
            , true);
        var xmlCompositeObjBuilder = new XmlCompositeObjectBuilder(
            (parsers, innerObjOrder) => new XmlObject(parsers) { InnerObjectPosition = innerObjOrder }
            , xmlObjBuilder
            , new IXmlBuilder<XmlObjectParts>[] { xmlInnerObjBuilder }
            , int.Parse(xmlInnerObjBlocks[XmlObjectParts.ObjectPosition]));

        var actual = xmlCompositeObjBuilder.CreateXml().Text;

        var list = new List<string>();
        list.AddRange(xmlObjBlocks.Values);
        list.AddRange(xmlInnerObjBlocks.Values);

        if (_utils.IsLogging)
            _utils.Log(
                _utils.CreateLog(
                    nameof(TestXmlCompositeObject)
                    , expected
                    , actual
                    , list.ToArray()));

        Assert.Equal(expected, actual);
    }
}