using BenfordsLaw.Ports;

namespace BenfordsLaw.Adapters
{
    public class BajasPorProvincia : AdapterBase, IDataSourceReader
    {
        public List<double> ReadNumbers()
        {
            string[] linesInFile = base.LoadContentFrom("INE Bajas por Provincia.csv");

            List<Baja> dataInFile = ReadInformation(linesInFile);

            List<double> numbers = GetNumbersFromAColumn(dataInFile);

            return numbers;
        }

        private static List<double> GetNumbersFromAColumn(List<Baja> dataInFile)
        {
            return dataInFile
                .Where(x => (x.Nacionality?.Contains("ESPAÑ") ?? false) && (x.BirthContinent?.Contains("UE") ?? false))
                .Select(x => x.Value)
                .ToList();
        }

        private static List<Baja> ReadInformation(string[] linesInFile)
        {
            var dataInFile = new List<Baja>();
            foreach (string line in linesInFile)
            {
                if (MustSkipLine(line))
                    continue;

                var fields = line.Split(";");

                dataInFile.Add(new Baja
                {
                    Province = fields[0].ToUpper(),
                    Nacionality = fields[1].ToUpper(),
                    BirthContinent = fields[2].ToUpper(),
                    Value = double.Parse(fields[3])
                });
            }

            return dataInFile;
        }

        private static bool MustSkipLine(string line) => string.IsNullOrEmpty(line)
            || line.StartsWith("Provincia");

        private struct Baja
        {
            public string? Province;
            public string? Nacionality;
            public string? BirthContinent;
            public double Value;
        };
    }
}
