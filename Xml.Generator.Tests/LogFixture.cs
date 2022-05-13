using System;

namespace Xml.Generator.Tests;

public class LogFixture
    : IDisposable
{
    private bool _firedOnce = false;
    private string? fileName;

    public string? FileName
    {
        get => fileName;
        set
        {
            fileName = value;
            if (_firedOnce) return;
            Utils.FileName = fileName;
            _firedOnce = true;
        }
    }

    public ITestUtils Utils { get; private set; }

    public LogFixture()
    {
        Utils = new TestUtils(new ParameterError());
    }

    public void Dispose()
    {

    }
}