namespace BenfordsLaw.Interfaces
{
    public interface IFileReader
    {
        string FileName { get; set; }

        string[] ReadContent();
    }
}