namespace BenfordsLaw
{
    public class BirthRate
    {
        public static List<double> ReadNumbers()
        {
            Console.WriteLine("Data WorldBank - Birth rate per mil");
            var reader = new StreamReader(".\\DataSource\\BirthRatePerMil.Data.WorldBank.csv");

            string fileContent = reader.ReadToEnd();
            string[] linesInFile = fileContent.Split("\r\n");

            Dictionary<int, int> year = MatchYearsWithIndex();

            var dataInFile = new List<Baja>();
            foreach (string line in linesInFile)
            {
                if (MustSkipLine(line))
                    continue;

                var fields = line.Split("\"");
                fields = fields.Where(x => x != "\"" && x != "," && !string.IsNullOrEmpty(x)).ToArray();

                if (NoDataFields(fields))
                    continue;

                dataInFile.Add(new Baja
                {
                    CountryName = fields[0].ToUpper(),
                    CountryCode = fields[1].ToUpper(),
                    IndicatorName = fields[2].ToUpper(),
                    IndicatorCode = fields[3].ToUpper(),
                    BirthRate = TryGetField_OrZero(fields, year[2000])//double.Parse(fields[year[1961]])
                });
            }

            var numbers = dataInFile.Select(x => x.BirthRate);

            return numbers.ToList();
        }

        private static double TryGetField_OrZero(string[] fields, int fieldIndex)
        {
            double result = 0;
            if (fields.Length > fieldIndex)
                double.TryParse(fields[fieldIndex], out result);

            return result;
        }

        private static bool NoDataFields(string[] fields) => fields.Length < 5;

        private static Dictionary<int, int> MatchYearsWithIndex()
        {
            var years = new Dictionary<int, int>();

            for (int year = 1960, index = 4; year <= 2021; year++, index++)
                years.Add(year, index);

            return years;
        }

        private static bool MustSkipLine(string line) => line.StartsWith("\"Data Source")
            || line.StartsWith("\"Last Updated")
            || line.StartsWith("\"Country Name")
            || string.IsNullOrEmpty(line);

        private class Baja
        {
            public string? CountryName;
            public string? CountryCode;
            public string? IndicatorName;
            public string? IndicatorCode;
            /// <summary>
            /// FROM 1960 TO 2021
            /// </summary>
            public double BirthRate;
        };
    }
}
