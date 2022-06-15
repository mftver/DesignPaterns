namespace Factory.models;

public class File
{
    public File(string path)
    {
        Path = path;
    }

    public string Path { get; }

    public string[] Contents() {
        var lines = System.IO.File.ReadAllLines(Path);
        return lines;
    }

    public string Extension => System.IO.Path.GetExtension(Path);
}