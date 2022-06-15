namespace Factory.models;

public class File
{
    public File(string path)
    {
        Path = path;
    }

    public string Path { get; }

    private string[] Contents(string filePath) {
        var lines = System.IO.File.ReadAllLines(filePath);
        return lines;
    }

    public string Extension => System.IO.Path.GetExtension(Path);
}