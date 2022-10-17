using BenfordsLaw.Ports;

namespace BenfordsLaw.Adapters
{
    public class AdapterBase
    {
        public IFileReader? FileReaderAdapter { get; set; }

        protected string[] LoadContentFrom(string fileName)
        {
            if (FileReaderAdapter is null)
                throw new Exception($"{nameof(IFileReader)} instance must be provided.");

            FileReaderAdapter.FileName = fileName;

            return FileReaderAdapter.ReadContent();
        }
    }
}
