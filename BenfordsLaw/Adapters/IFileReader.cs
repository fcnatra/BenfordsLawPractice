namespace BenfordsLaw.Adapters
{
    public interface IFileReader
    {
        string FileName { get; set; }

        string[] ReadContent();
    }
}