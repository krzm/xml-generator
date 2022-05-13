using System;
using Xunit;

namespace Xml.Generator.Tests;

public class UtilsTest
    : IClassFixture<LogFixture>
{
    private readonly LogFixture _logFixture;
    private readonly ITestUtils _utils;

    public UtilsTest(LogFixture logFixture)
    {
        _logFixture = logFixture;
        _utils = _logFixture.Utils;
    }

    [Theory]
    [InlineData(true, "Unicode (UTF-8)", @"C:\Tests\TestTempFiles", ".txt")]
    public void SetUpTest(bool isLogging, string encoding, string folderPath, string fileExt)
    {
        Assert.Equal(isLogging, _utils.IsLogging);
        Assert.Equal(encoding, _utils?.Encoding?.EncodingName);
        Assert.Equal(folderPath, _utils?.FolderPath);
        Assert.Equal(fileExt, _utils?.FileExtension);
        Assert.Null(_utils?.FilePath);
    }

    [Theory]
    [InlineData("1,2,3", ",", new string[] { "1", "2", "3" })]
    public void ExtractParametersTest(string separetedParameters, string separator, string[] expectedArray)
    {
        if (string.IsNullOrWhiteSpace(separator)) throw new ArgumentException("Provide text to parameter", nameof(separator));
        var actual = _utils.ExtractParameters(separetedParameters, separator.ToCharArray());
        Assert.Equal(expectedArray, actual);
    }

    [Theory]
    [InlineData(" 1, 2, 3", ",", new string[] { "1", "2", "3" })]
    public void ExtractParametersAndTrimTest(string separetedParameters, string separator, string[] expectedArray)
    {
        if (string.IsNullOrWhiteSpace(separator)) throw new ArgumentException("Provide text to parameter", nameof(separator));
        var actual = _utils.ExtractParametersAndTrim(separetedParameters, separator.ToCharArray());
        Assert.Equal(expectedArray, actual);
    }

    [Theory]
    [InlineData("testName1", "expectedText1", "acctualText1", "testParameters1;\r\n",
        "testName1:\r\n" +
        "Test Params:\r\n" +
        "[{testParameters1;\\r\\n}]:\r\n" +
        "Expected:\r\n" +
        "expectedText1\r\n" +
        "Acctual:\r\n" +
        "acctualText1\r\n\r\n")]
    public void CreateLogTest(string testName, string expectedText, string acctualText, string testParameters, string expected) =>
        Assert.Equal(expected, _utils.CreateLog(testName, expectedText, acctualText, testParameters));
}