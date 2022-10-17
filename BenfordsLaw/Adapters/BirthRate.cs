using BenfordsLaw.Ports;

namespace BenfordsLaw.Adapters
{
    public class BirthRate : AdapterBase, IDataSourceReader
    {
        public List<double> ReadNumbers()
        {
            string[] linesInFile = base.LoadContentFrom("BirthRatePerMil.Data.WorldBank.csv");

            List<BirthInfo> dataInFile = ReadInformation(linesInFile);

            var numbers = dataInFile.Select(x => x.BirthRate);

            return numbers.ToList();
        }

        private static List<BirthInfo> ReadInformation(string[] linesInFile)
        {
            Dictionary<int, int> year = MatchYearsWithIndex();

            var births = new List<BirthInfo>();
            foreach (string line in linesInFile)
            {
                if (MustSkipLine(line))
                    continue;

                var fields = line.Split("\"");
                fields = fields.Where(x => x != "\"" && x != "," && !string.IsNullOrEmpty(x)).ToArray();

                if (NoDataFields(fields))
                    continue;

                births.Add(new BirthInfo
                {
                    CountryName = fields[0].ToUpper(),
                    CountryCode = fields[1].ToUpper(),
                    IndicatorName = fields[2].ToUpper(),
                    IndicatorCode = fields[3].ToUpper(),
                    BirthRate = TryGetField_OrZero(fields, year[2000])//double.Parse(fields[year[1961]])
                });
            }

            return births;
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

        private struct BirthInfo
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
