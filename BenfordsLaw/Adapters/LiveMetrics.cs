using BenfordsLaw.Ports;

namespace BenfordsLaw.Adapters
{
    public class LiveMetrics : AdapterBase, IDataSourceReader
    {
        public List<double> ReadNumbers()
        {
            string[] linesInFile = base.LoadContentFrom("LiveMetrics.produccionanualacorganizacion.datos.gob.es.csv");

            List<LiveMetric> liveMetrics = ReadInformation(linesInFile);

            var numbers = liveMetrics.Select(x => (double)x.WebOfScienceDocs);

            return numbers.ToList();
        }

        private static List<LiveMetric> ReadInformation(string[] linesInFile)
        {
            var dataInFile = new List<LiveMetric>();
            foreach (string line in linesInFile)
            {
                if (MustSkipLine(line))
                    continue;

                var fields = line.Split(";");

                if (NoDataFields(fields))
                    continue;

                dataInFile.Add(new LiveMetric
                {
                    Name = fields[0].ToUpper(),
                    WebOfScienceDocs = int.Parse(fields[1]), //*
                    CategoryImpact = double.Parse(fields[2]),
                    TimesCited = int.Parse(fields[3]), //*
                    HighlyCitedPapers = int.Parse(fields[4]), //*
                    PublicationYear = int.Parse(fields[5])
                });
            }

            return dataInFile;
        }

        private static bool NoDataFields(string[] fields) => fields.Length < 5
            || string.IsNullOrEmpty(string.Join("", fields));

        private static bool MustSkipLine(string line) => string.IsNullOrEmpty(line)
            || line[0] < '0' || line[0] > '9';

        private struct LiveMetric
        {
            public string? Name;
            public int WebOfScienceDocs;
            public double CategoryImpact;
            public int TimesCited;
            public int HighlyCitedPapers;
            public int PublicationYear;
        };
    }
}
