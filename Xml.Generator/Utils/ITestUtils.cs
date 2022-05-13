using System.Text;

namespace Xml.Generator;

public interface ITestUtils
{
    bool IsLogging { get; set; }

    Encoding? Encoding { get; set; }

    string? FolderPath { get; set; }

    string? FileName { get; set; }

    string? FileExtension { get; set; }

    string? FilePath { get; }

    string[] ExtractParameters(string separatedParameters, params char[] separator);

    string[] ExtractParametersAndTrim(string separatedParameters, params char[] separator);

    string[] TrimParameters(string[] parameters, params char[] trimChars);

    void Log(string logText);

    void LogToNewFile(string logText);

    string CreateLog(string testName, string expectedText, string acctualText, params string[] testParameters);
}