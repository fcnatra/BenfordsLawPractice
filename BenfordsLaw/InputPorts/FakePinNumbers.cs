using BenfordsLaw.Interfaces;

namespace BenfordsLaw.InputPorts
{
    public class FakePinNumbers : IDataSourceReader
    {
        public IFileReader? FileReaderAdapter { get; set; }

        public List<double> ReadNumbers()
        {
            Console.WriteLine("FAKE PIN Numbers");

            string[] linesInFile = FileReaderAdapter?.ReadContent() ?? Array.Empty<string>();

            var numbers = new List<double>();
            foreach (string line in linesInFile)
            {
                if (MustSkipLine(line))
                    continue;

                numbers.Add(double.Parse(line));
            }

            return numbers;
        }

        private static bool MustSkipLine(string line) => line.StartsWith("PIN") || string.IsNullOrEmpty(line);
    }
}
