namespace BenfordsLaw.Ports
{
    public interface IFileReader
    {
        string FilePath { get; set; }

        string FileName { get; set; }

        string[] ReadContent();
    }
}