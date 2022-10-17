namespace BenfordsLaw.Interfaces
{
    public interface IDataSourceReader
    {
        IFileReader? FileReaderAdapter { get; set; }
        List<double> ReadNumbers();
    }
}