using BenfordsLaw.Ports;

namespace BenfordsLaw.Adapters
{
    public class FileReader : IFileReader
    {
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;

        public static string[] ReadContent(string fullPathToFile)
        {
            return new FileReader { FileName = fullPathToFile }.ReadContent();
        }

        public string[] ReadContent()
        {
            var reader = new StreamReader(Path.Combine(FilePath, FileName));
            string splitter = "\n";

            string? fileContent = reader.ReadLine();
            if (fileContent?.EndsWith("\r\n") == true)
                splitter = "\r\n";

            fileContent += reader.ReadToEnd();
            return fileContent.Split(splitter);
        }
    }
}
