using BenfordsLaw.Ports;

namespace BenfordsLaw.Adapters
{
    public class MockNameAndAge : AdapterBase, IDataSourceReader
    {
        public List<double> ReadNumbers()
        {
            string[] linesInFile = base.LoadContentFrom("Mock Name and Age.mockaroo.com.csv");

            List<PersonInfo> personsInfo = ReadInformation(linesInFile);

            var numbers = personsInfo.Select(x => (double)x.Age);

            return numbers.ToList();
        }

        private static List<PersonInfo> ReadInformation(string[] linesInFile)
        {
            var dataInFile = new List<PersonInfo>();
            foreach (string line in linesInFile)
            {
                if (MustSkipLine(line))
                    continue;

                var fields = line.Split(",");

                if (NoDataFields(fields))
                    continue;

                dataInFile.Add(new PersonInfo
                {
                    Name = fields[0].ToUpper(),
                    Age = int.Parse(fields[1])
                });
            }

            return dataInFile;
        }

        private static bool NoDataFields(string[] fields) => fields.Length < 2
            || string.IsNullOrEmpty(string.Join("", fields));

        private static bool MustSkipLine(string line) => string.IsNullOrEmpty(line)
            || line.StartsWith("full");

        private class PersonInfo
        {
            public string? Name;
            public int Age;
        };
    }
}
