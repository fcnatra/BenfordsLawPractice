// See https://aka.ms/new-console-template for more information

using BenfordsLaw.Domain;
using BenfordsLaw.Ports;
using System.Collections.Generic;

internal class Program
{
    internal static BenfordsLawLogic _lawCalculator = new BenfordsLawLogic();

    private static void Main(string[] args)
    {
        PrintPercentageOfAppereance("Original Benford's Law Predictions", _lawCalculator.LawNumbers());

        AnaliseDataSourcesWithBenfordsLaw();
    }

    private static void AnaliseDataSourcesWithBenfordsLaw()
    {
        var readers = DataSourceFactory.GetAllReaders();
        //new List<IDataSourceReader> { 
        //    DataSourceFactory.GetReader(ReaderType.BajasPorProvincia) 
        //}
        Dictionary<string, List<NumberOfAppereance>> calculationResults = ApplyBenfordsLawToDataSource(readers);

        Console.WriteLine("\nDatasource analysis result =======");

        foreach (var result in calculationResults)
            PrintPercentageOfAppereance(result.Key, result.Value);
    }

    private static Dictionary<string, List<NumberOfAppereance>> ApplyBenfordsLawToDataSource(List<IDataSourceReader> readers)
    {
        List<double> numbers;
        var calculationResults = new Dictionary<string, List<NumberOfAppereance>>();

        foreach (var reader in readers)
        {
            numbers = reader.ReadNumbers();
            List<NumberOfAppereance> calculatedPercentages = _lawCalculator.CalculatePercentages(numbers);

            calculationResults.Add(reader.GetType().Name, calculatedPercentages);
        }

        return calculationResults;
    }

    private static void PrintPercentageOfAppereance(string headerText, List<NumberOfAppereance> numberOfAppereances)
    {
        Console.WriteLine($"\n{headerText}");
        Console.WriteLine($"{"Digit",5} {"appereance %",12}");

        foreach (var number in numberOfAppereances)
            Console.WriteLine($"{number.Digit,5} {number.PercentageOfAppereances,10:F1} %");
    }
}