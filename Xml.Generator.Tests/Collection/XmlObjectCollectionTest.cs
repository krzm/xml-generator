using System.Collections.Generic;
using Xunit;

namespace Xml.Generator.Tests;

public class XmlObjectCollectionTest
    : IClassFixture<LogFixture>
{
    private readonly LogFixture _logFixture;
    private readonly ITestUtils _utils;

    public XmlObjectCollectionTest(LogFixture logFixture)
    {
        _logFixture = logFixture;
        _logFixture.FileName = nameof(XmlObjectCollectionTest);
        _utils = _logFixture.Utils;
    }

    [Theory, ClassData(typeof(XmlObjectCollectionData))]
    public void TestXmlObjectCollection(
        IDictionary<XmlCollectionParts, string> collectionParts,
        IDictionary<XmlObjectParts, string> objectParts,
        string expected)
    {
        var actual = new XmlObjectCollectionBuilder().CreateXml(collectionParts, objectParts).Text;

        var list = new List<string>();
        list.AddRange(collectionParts.Values);
        list.AddRange(objectParts.Values);

        if (_utils.IsLogging)
            _utils.Log(
                _utils.CreateLog(
                    nameof(TestXmlObjectCollection)
                    , expected
                    , actual
                    , list.ToArray()));

        Assert.Equal(expected, actual);
    }
}