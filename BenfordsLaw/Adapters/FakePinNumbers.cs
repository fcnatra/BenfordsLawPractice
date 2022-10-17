using BenfordsLaw.Ports;

namespace BenfordsLaw.Adapters
{
    public class FakePinNumbers : AdapterBase, IDataSourceReader
    {
        public List<double> ReadNumbers()
        {
            string[] linesInFile = base.LoadContentFrom("FAKE PinNumbers.csv");

            List<double> numbers = ReadInformation(linesInFile);

            return numbers;
        }

        private static List<double> ReadInformation(string[] linesInFile)
        {
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
