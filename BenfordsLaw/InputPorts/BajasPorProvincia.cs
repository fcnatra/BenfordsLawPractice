using BenfordsLaw.Interfaces;

namespace BenfordsLaw.InputPorts
{
    public class BajasPorProvincia : IDataSourceReader
    {
        public IFileReader? FileReaderAdapter { get; set; }

        public List<double> ReadNumbers()
        {
            Console.WriteLine("INE - Bajas por provincia");

            string[] linesInFile = FileReaderAdapter?.ReadContent() ?? Array.Empty<string>();

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

            var numbers = dataInFile
                .Where(x => (x.Nacionality?.Contains("ESPAÑ") ?? false) && (x.BirthContinent?.Contains("UE") ?? false))
                .Select(x => x.Value);

            return numbers.ToList();
        }

        private static bool MustSkipLine(string line) => string.IsNullOrEmpty(line)
            || line.StartsWith("Provincia");

        private class Baja
        {
            public string? Province;
            public string? Nacionality;
            public string? BirthContinent;
            public double Value;
        };
    }
}
