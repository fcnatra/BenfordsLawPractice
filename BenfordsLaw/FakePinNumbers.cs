namespace BenfordsLaw
{
    public class FakePinNumbers
    {
        public static List<double> ReadNumbers()
        {
            Console.WriteLine("FAKE PIN Numbers");
            var reader = new StreamReader(".\\DataSource\\FAKE PinNumbers.csv");

            string fileContent = reader.ReadToEnd();
            string[] linesInFile = fileContent.Split("\r\n");

            var numbers = new List<double>();
            foreach (string line in linesInFile)
            {
                if (IsTheFirstLine(line) || string.IsNullOrEmpty(line))
                    continue;

                numbers.Add(double.Parse(line));
            }

            return numbers;
        }

        private static bool IsTheFirstLine(string line) => line.StartsWith("PIN");
    }
}
