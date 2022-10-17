using BenfordsLaw.InputAdapters;
using BenfordsLaw.InputPorts;
using BenfordsLaw.Interfaces;

namespace BenfordsLaw
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
            var dataSourceFolder = ".\\DataSourceFiles\\";
            var fileReaderAdapter = new FileReader();

            switch (readerType)
            {
                case ReaderType.BajasPorProvincia:
                    fileReaderAdapter.FileName = $"{dataSourceFolder}INE Bajas por Provincia.csv";
                    return new BajasPorProvincia { FileReaderAdapter = fileReaderAdapter };

                case ReaderType.BirthRate:
                    fileReaderAdapter.FileName = $"{dataSourceFolder}BirthRatePerMil.Data.WorldBank.csv";
                    return new BirthRate { FileReaderAdapter = fileReaderAdapter };

                case ReaderType.LiveMetrics:
                    fileReaderAdapter.FileName = $"{dataSourceFolder}LiveMetrics.produccionanualacorganizacion.datos.gob.es.csv";
                    return new LiveMetrics { FileReaderAdapter = fileReaderAdapter };

                case ReaderType.FakePinNumbers:
                    fileReaderAdapter.FileName = $"{dataSourceFolder}FAKE PinNumbers.csv";
                    return new FakePinNumbers { FileReaderAdapter = fileReaderAdapter };

                case ReaderType.FakeNameAndAges:
                    fileReaderAdapter.FileName = $"{dataSourceFolder}Mock Name and Age.mockaroo.com.csv";
                    return new MockNameAndAge { FileReaderAdapter = fileReaderAdapter };

                default:
                    throw new Exception("Unknown Datasource file reader");
            }
        }
    }
}
