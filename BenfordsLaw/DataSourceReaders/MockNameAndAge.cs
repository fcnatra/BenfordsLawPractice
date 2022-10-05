namespace BenfordsLaw.DataSourceReaders
{
    public class MockNameAndAge : DataSourceReaderBase, IDataSourceReader
    {
        public List<double> ReadNumbers()
        {
            Console.WriteLine("Mock Name and Age.mockaroo.com");
            var reader = new StreamReader($"{base.SourceFolder}Mock Name and Age.mockaroo.com.csv");

            string fileContent = reader.ReadToEnd();
            string[] linesInFile = fileContent.Split("\n");

            var dataInFile = new List<PersonData>();
            foreach (string line in linesInFile)
            {
                if (MustSkipLine(line))
                    continue;

                var fields = line.Split(",");

                if (NoDataFields(fields))
                    continue;

                dataInFile.Add(new PersonData
                {
                    Name = fields[0].ToUpper(),
                    Age = int.Parse(fields[1])
                });
            }

            var numbers = dataInFile.Select(x => (double)x.Age);

            return numbers.ToList();
        }

        private bool NoDataFields(string[] fields) => fields.Length < 2
            || string.IsNullOrEmpty(string.Join("",fields));

        private bool MustSkipLine(string line) => string.IsNullOrEmpty(line)
            || line.StartsWith("full");

        private class PersonData
        {
            public string? Name;
            public int Age;
        };
    }
}
