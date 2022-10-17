using BenfordsLaw.Interfaces;

namespace BenfordsLaw.InputAdapters
{
    public class FileReader : IFileReader
    {
        public string FileName { get; set; } = string.Empty;

        public static string[] ReadContent(string fileName)
        {
            return new FileReader { FileName = fileName }.ReadContent();
        }

        public string[] ReadContent()
        {
            var reader = new StreamReader(FileName);
            string splitter = "\n";

            string? fileContent = reader.ReadLine();
            if (fileContent?.EndsWith("\r\n") == true)
                splitter = "\r\n";

            fileContent += reader.ReadToEnd();
            return fileContent.Split(splitter);
        }
    }
}
