// See https://aka.ms/new-console-template for more information

using BenfordsLaw;
using BenfordsLaw.Domain;

internal class Program
{
    internal static BenfordsLawLogic _lawCalculator = new BenfordsLawLogic();

    private static void Main(string[] args)
    {
        PrintBenfordsLawPercentages();

        Console.WriteLine("\nDatasource analysis result\n\n");

        List<double> numbers;
        //numbers = ReaderFactory.GetReader(ReaderType.BajasPorProvincia).ReadNumbers();
        //PrintPercentages(numbers);

        foreach (var reader in ReaderFactory.GetAllReaders())
        {
            Console.WriteLine(reader.GetType().Name);
            numbers = reader.ReadNumbers();

            List<NumberOfAppereance> calculatedPercentages = _lawCalculator.CalculatePercentages(numbers);

            PrintPercentages(numbers);
        }
    }

    private static void PrintBenfordsLawPercentages()
    {
        Console.WriteLine("Benford's Law");
        Console.WriteLine($"{"Digit",5} {"appereance %",12}");
        var lawNumbers = _lawCalculator.LawNumbers();
        foreach (var number in lawNumbers)
            Console.WriteLine($"{number.Digit,5} {number.PercentageOfAppereances,10} %");
    }

    private static void PrintPercentages(List<double> numbers)
    {
        var totalNumbers = numbers.Count();
        Console.WriteLine($"Total numbers: {totalNumbers}\n");

        List<char>? firstDigits = numbers.Select(x => x.ToString()[0]).ToList();

        for (char i = '1'; i <= '9'; i++)
            PrintPercentageOfAppereance(totalNumbers, firstDigits, i);

        Console.WriteLine($"\n\n\n");
    }

    private static void PrintPercentageOfAppereance(int totalNumbers, List<char> firstDigits, char i)
    {
        var appereances = firstDigits.Count(n => n == i);
        var percentage = appereances / (double)totalNumbers * 100;
        Console.WriteLine($"digit {i} appears {appereances,4} times, which makes a % of {percentage:F1}");
    }
}