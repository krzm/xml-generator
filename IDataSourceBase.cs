namespace Xml.Generator;

public interface IDataSource
{
    IEnumerable<object[]> GetDataSource();
}