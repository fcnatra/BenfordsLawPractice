using BenfordsLaw.Adapters;
using BenfordsLaw.Ports;

namespace BenfordsLaw.Domain
{
    public enum ReaderType
    {
        BajasPorProvincia,
        BirthRate,
        LiveMetrics,
        FakePinNumbers,
        FakeNameAndAges
    }

    public class DataSourceFactory
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
            var dataSourceFolder = ".\\DataSourceFiles\\";
            var fileReaderAdapter = new FileReader { FilePath = dataSourceFolder };

            switch (readerType)
            {
                case ReaderType.BajasPorProvincia:
                    return new BajasPorProvincia { FileReaderAdapter = fileReaderAdapter };

                case ReaderType.BirthRate:
                    return new BirthRate { FileReaderAdapter = fileReaderAdapter };

                case ReaderType.LiveMetrics:
                    return new LiveMetrics { FileReaderAdapter = fileReaderAdapter };

                case ReaderType.FakePinNumbers:
                    return new FakePinNumbers { FileReaderAdapter = fileReaderAdapter };

                case ReaderType.FakeNameAndAges:
                    return new MockNameAndAge { FileReaderAdapter = fileReaderAdapter };

                default:
                    throw new Exception("Unknown Datasource file reader");
            }
        }
    }
}
