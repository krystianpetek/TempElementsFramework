using System.Text;

namespace TempElementsFramework_;

public class TempTextFile : TempFile
{
    private TextReader txtReader;
    private TextWriter txtWriter;

    public TempTextFile(string fileName) : base(fileName)
    {
        txtReader = new StreamReader(fileStream, Encoding.UTF8);
        txtWriter = new StreamWriter(fileStream);
    }

    public TempTextFile() : this(Path.GetTempFileName())
    { }

    ~TempTextFile()
    {
        Dispose(false);
    }

    public string ReadAllText()
    {
        fileStream.Position = 0;
        return txtReader.ReadToEnd();
    }
    private long positionRead { get; set; } = 0;
    public string ReadLine()
    {
        fileStream.Position = positionRead;
        var temp = txtReader.ReadLine();
        positionRead = fileStream.Position;
        return temp;
    }

    public void Write(string text)
    {
        ReadAllText();
        txtWriter.Write(text);
        txtWriter.Flush();
    }

    public void WriteLine(string text)
    {
        ReadAllText();
        txtWriter.WriteLine(text);
        txtWriter.Flush();
    }
}