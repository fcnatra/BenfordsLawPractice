﻿namespace BenfordsLaw.DataSourceReaders
{
    public class LiveMetrics : DataSourceReaderBase, IDataSourceReader
    {
        public List<double> ReadNumbers()
        {
            Console.WriteLine("Datos.Gob.Es - LiveMetrics");
            var reader = new StreamReader($"{base.SourceFolder}LiveMetrics.produccionanualacorganizacion.datos.gob.es.csv");

            string fileContent = reader.ReadToEnd();
            string[] linesInFile = fileContent.Split("\r\n");

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

            var numbers = dataInFile.Select(x => (double)x.WebOfScienceDocs);

            return numbers.ToList();
        }

        private bool NoDataFields(string[] fields) => fields.Length < 5
            || string.IsNullOrEmpty(string.Join("",fields));

        private bool MustSkipLine(string line) => string.IsNullOrEmpty(line)
            || (line[0] < '0' || line[0] > '9');

        private class LiveMetric
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
