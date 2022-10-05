namespace BenfordsLaw.DataSourceReaders
{
    public class FakePinNumbers
    {
        public static List<double> ReadNumbers()
        {
            Console.WriteLine("FAKE PIN Numbers");
            var reader = new StreamReader(".\\DataSourceFiles\\FAKE PinNumbers.csv");

            string fileContent = reader.ReadToEnd();
            string[] linesInFile = fileContent.Split("\r\n");

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
