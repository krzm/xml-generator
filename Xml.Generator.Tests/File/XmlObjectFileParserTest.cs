using System.Collections.Generic;
using Xunit;

namespace Xml.Generator.Tests;

public class XmlObjectFileParserTest
    : IClassFixture<LogFixture>
{
    private readonly LogFixture _logFixture;
    private readonly ITestUtils _utils;

    public XmlObjectFileParserTest(LogFixture logFixture)
    {
        _logFixture = logFixture;
        _logFixture.FileName = nameof(XmlObjectFileParserTest);
        _utils = _logFixture.Utils;
    }

    [Theory]
    [InlineData(
    "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" +
    "<ObjectName>\r\n" +
    "  <Property1Name>Value1</Property1Name>\r\n" +
    "  <Property2Name>Value2</Property2Name>\r\n" +
    "</ObjectName>")]
    public void TestXmlObjectFileParser(string expected)
    {
        SetupData(out Dictionary<XmlFileParts, string> xmlFileParts
            , out Dictionary<XmlObjectParts, string> objDictionary);
        var actual = SetupTest(xmlFileParts, objDictionary).Text;

        LogTest(expected, xmlFileParts, objDictionary, actual);

        Assert.Equal(expected, actual);
    }

    private static void SetupData(out Dictionary<XmlFileParts, string> xmlFileParts, out Dictionary<XmlObjectParts, string> objDictionary)
    {
        xmlFileParts = new Dictionary<XmlFileParts, string> {
                { XmlFileParts.Prefix, "" },
                { XmlFileParts.Header, "<?xml version=\"1.0\" encoding=\"utf-8\"?>" },
                { XmlFileParts.Postfix, "\r\n" }};
        objDictionary = new Dictionary<XmlObjectParts, string> {
                    { XmlObjectParts.Empty, "" },
                    { XmlObjectParts.ObjectPrefix, "" },
                    { XmlObjectParts.ObjectName, "ObjectName" },
                    { XmlObjectParts.PropPrefix, "  " },
                    { XmlObjectParts.Property1, "Property1Name" },
                    { XmlObjectParts.Value1, "Value1" },
                    { XmlObjectParts.Property2, "Property2Name" },
                    { XmlObjectParts.Value2, "Value2" },
                    { XmlObjectParts.NewLine, "\r\n" }};
    }

    private static XmlFile SetupTest(
        Dictionary<XmlFileParts, string> xmlFileParts
        , Dictionary<XmlObjectParts, string> objDictionary)
    {
        var od = objDictionary;
        return new XmlFile(
            new IXmlParser[]
            {
                new XmlFileParser((headerParts) =>
                    new XmlHeaderParser(headerParts)
                    , xmlFileParts[XmlFileParts.Prefix]
                    , xmlFileParts[XmlFileParts.Postfix]),
                new XmlObjectParser(
                    new string[][]
                    {
                        new string[]
                        {
                            od[XmlObjectParts.PropPrefix],
                            od[XmlObjectParts.Property1],
                            od[XmlObjectParts.Value1],
                            od[XmlObjectParts.NewLine]
                        },
                        new string[]
                        {
                            od[XmlObjectParts.PropPrefix],
                            od[XmlObjectParts.Property2],
                            od[XmlObjectParts.Value2],
                            od[XmlObjectParts.NewLine]
                        }
                    }
                    , (property) => new XmlPropertyParser(property)
                    , new XmlElementParser(
                        od[XmlObjectParts.ObjectPrefix],
                        od[XmlObjectParts.ObjectName],
                        od[XmlObjectParts.NewLine])
                    , new XmlElementParser(
                        od[XmlObjectParts.ObjectPrefix],
                        od[XmlObjectParts.ObjectName],
                        od[XmlObjectParts.Empty]))
            });
    }

    private void LogTest(string expected, Dictionary<XmlFileParts, string> xmlFileParts, Dictionary<XmlObjectParts, string> objDictionary, string actual)
    {
        var list = new List<string>();
        list.AddRange(xmlFileParts.Values);
        list.AddRange(objDictionary.Values);

        if (_utils.IsLogging)
            _utils.Log(
                _utils.CreateLog(
                    nameof(TestXmlObjectFileParser)
                    , expected
                    , actual
                    , list.ToArray()));
    }
}