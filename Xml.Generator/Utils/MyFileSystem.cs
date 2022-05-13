namespace Xml.Generator;

public static class MyFileSystem
{
    public static bool EnsureFolder(string path)
    {
        try
        {
            if (Directory.Exists(path)) return true;
            else
            {
                Directory.CreateDirectory(path);
                return true;
            }
        }
        catch (Exception)
        {
            //todo: log
            return false;
        }
    }
}