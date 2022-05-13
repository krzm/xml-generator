namespace Xml.Generator;

public abstract class DataSourceBase
    : IDataSource
{
    public abstract IEnumerable<object[]> GetDataSource();
}