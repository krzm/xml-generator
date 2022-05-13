using System.Text;

namespace Xml.Generator;

public class TestUtils
    : ITestUtils
{
    private readonly object _lock = new object();
    private readonly IParameterError _parameterError;
    private string? fileName;
    private const string NewLine = "\r\n";

    public bool IsLogging { get; set; }

    public Encoding? Encoding { get; set; }

    public string? FolderPath { get; set; }

    public string? FileName
    {
        get => fileName;

        set
        {
            ArgumentNullException.ThrowIfNull(FolderPath);
            fileName = value;
            FilePath = Path.Combine(FolderPath, $"{FileName}{FileExtension}");
            LogToNewFile(string.Empty);
        }
    }

    public string? FileExtension { get; set; }

    public string? FilePath { get; private set; }

    public TestUtils(IParameterError standardErrors)
    {
        _parameterError = standardErrors;
        SetUp();
    }

    private void SetUp()
    {
        IsLogging = true;
        Encoding = Encoding.UTF8;
        FolderPath = @"C:\Tests\TestTempFiles";
        FileExtension = ".txt";
        MyFileSystem.EnsureFolder(FolderPath);
    }

    public string[] ExtractParameters(string separetedParameters, params char[] separator)
    {
        _parameterError.NullText(separetedParameters);
        _parameterError.NoParam(separator);
        return separetedParameters.Split(separator);
    }

    public string[] ExtractParametersAndTrim(string separetedParameters, params char[] separator) =>
        TrimParameters(ExtractParameters(separetedParameters, separator));

    public string[] TrimParameters(string[] parameters, params char[] trimChars)
    {
        _parameterError.NoParam(parameters);
        for (int i = 0; i < parameters.Length; i++)
        {
            parameters[i] = parameters[i].Trim(trimChars);
        }
        return parameters;
    }

    public void Log(string logText)
    {
        lock (_lock)
        {
            if (IsLogging == false) return;
            ArgumentNullException.ThrowIfNull(FilePath);
            ArgumentNullException.ThrowIfNull(Encoding);
            File.AppendAllText(FilePath, logText, Encoding);
        }
    }

    public void LogToNewFile(string logText)
    {
        lock (_lock)
        {
            if (IsLogging == false) return;
            ArgumentNullException.ThrowIfNull(FilePath);
            ArgumentNullException.ThrowIfNull(Encoding);
            File.WriteAllText(FilePath, logText, Encoding);
        }
    }

    public string CreateLog(
        string testName
        , string expectedText
        , string acctualText
        , params string[] testParameters) =>
            $"{testName}:{NewLine}" +
            $"Test Params:{NewLine}" +
            $"[{testParameters.JoinTestParams()}]:{NewLine}" +
            $"Expected:{NewLine}" +
            $"{expectedText}{NewLine}" +
            $"Acctual:{NewLine}" +
            $"{acctualText}{NewLine}{NewLine}";
}