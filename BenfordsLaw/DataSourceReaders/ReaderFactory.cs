namespace BenfordsLaw.DataSourceReaders
{
    public enum ReaderType
    {
        BajasPorProvincia,
        BirthRate,
        LiveMetrics,
        FakePinNumbers,
        FakeNameAndAges
    }

    public class ReaderFactory
    {
        public static List<IDataSourceReader> GetAllReaders()
        {
            var readers = new List<IDataSourceReader>();

            foreach (var item in Enum.GetNames(typeof(ReaderType)))
                readers.Add(GetReader((ReaderType)Enum.Parse(typeof(ReaderType), item)));
            
            return readers;
        }

        public static IDataSourceReader GetReader(ReaderType readerType)
        {
            switch (readerType)
            {
                case ReaderType.BajasPorProvincia:
                    return new BajasPorProvincia { SourceFolder = ".\\DataSourceFiles\\" };
                case ReaderType.BirthRate:
                    return new BirthRate { SourceFolder = ".\\DataSourceFiles\\" };
                case ReaderType.LiveMetrics:
                    return new LiveMetrics { SourceFolder = ".\\DataSourceFiles\\" };
                case ReaderType.FakePinNumbers:
                    return new FakePinNumbers { SourceFolder = ".\\DataSourceFiles\\" };
                case ReaderType.FakeNameAndAges:
                    return new MockNameAndAge { SourceFolder = ".\\DataSourceFiles\\" };
                default:
                    throw new Exception("Unknown Datasource file reader");
            }
        }
    }
}
