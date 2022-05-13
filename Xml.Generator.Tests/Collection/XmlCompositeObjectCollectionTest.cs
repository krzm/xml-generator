using System.Collections.Generic;
using Xunit;

namespace Xml.Generator.Tests;

public class XmlCompositeObjectCollectionTest
    : IClassFixture<LogFixture>
{
    private readonly LogFixture _logFixture;
    private readonly ITestUtils _utils;

    public XmlCompositeObjectCollectionTest(LogFixture logFixture)
    {
        _logFixture = logFixture;
        _logFixture.FileName = nameof(XmlCompositeObjectCollectionTest);
        _utils = _logFixture.Utils;
    }

    [Theory, ClassData(typeof(XmlCompositeObjectCollectionData))]
    public void TestXmlCompositeObjectCollection(
        IDictionary<XmlCollectionParts, string> collectionParts,
        IDictionary<XmlObjectParts, string> objectParts,
        IDictionary<XmlObjectParts, string> innerObjectParts,
        string expected)
    {
        var actual = new XmlCompositeObjectCollectionBuilder().CreateXml(collectionParts, objectParts, innerObjectParts).Text;

        var list = new List<string>();
        list.AddRange(collectionParts.Values);
        list.AddRange(objectParts.Values);
        list.AddRange(innerObjectParts.Values);

        if (_utils.IsLogging)
            _utils.Log(
                _utils.CreateLog(
                    nameof(TestXmlCompositeObjectCollection)
                    , expected
                    , actual
                    , list.ToArray()));

        Assert.Equal(expected, actual);
    }
}