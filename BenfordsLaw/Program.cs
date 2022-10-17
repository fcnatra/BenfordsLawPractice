// See https://aka.ms/new-console-template for more information

using BenfordsLaw;

internal class Program
{
    private static void Main(string[] args)
    {
        PrintBenfordsLawPercentages();

        Console.WriteLine("\nDatasource analysis result\n\n");

        List<double> numbers;
        //numbers = ReaderFactory.GetReader(ReaderType.BajasPorProvincia).ReadNumbers();
        //numbers = ReaderFactory.GetReader(ReaderType.FakePinNumbers).ReadNumbers();
        //numbers = ReaderFactory.GetReader(ReaderType.BirthRate).ReadNumbers();
        //numbers = ReaderFactory.GetReader(ReaderType.LiveMetrics).ReadNumbers();
        //numbers = ReaderFactory.GetReader(ReaderType.FakeNameAndAges).ReadNumbers();
        //PrintPercentages(numbers);

        foreach (var reader in ReaderFactory.GetAllReaders())
        {
            numbers = reader.ReadNumbers();
            Console.WriteLine(reader.GetType().Name);
            PrintPercentages(numbers);
        }
    }

    private static void PrintBenfordsLawPercentages()
    {
        Console.WriteLine("Benford's Law");
        Console.WriteLine(" 1  30,1 %");
        Console.WriteLine(" 2  17,6 %");
        Console.WriteLine(" 3  12,5 %");
        Console.WriteLine(" 4   9,7 % ");
        Console.WriteLine(" 5   7,9 % ");
        Console.WriteLine(" 6   6,7 % ");
        Console.WriteLine(" 7   5,8 % ");
        Console.WriteLine(" 8   5,1 % ");
        Console.WriteLine(" 9   4,6 % ");
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