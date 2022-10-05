// See https://aka.ms/new-console-template for more information

using BenfordsLaw.DataSourceReaders;

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


Console.WriteLine("\nDatasource analysis result");

List<double> numbers;
numbers = ReaderFactory.GetReader(ReaderType.BajasPorProvincia).ReadNumbers();
numbers = ReaderFactory.GetReader(ReaderType.FakePinNumbers).ReadNumbers();
numbers = ReaderFactory.GetReader(ReaderType.BirthRate).ReadNumbers();
numbers = ReaderFactory.GetReader(ReaderType.LiveMetrics).ReadNumbers();
numbers = ReaderFactory.GetReader(ReaderType.FakeNameAndAges).ReadNumbers();

List<char>? firstDigitInNumbers = numbers.Select(x => x.ToString()[0]).ToList();

var totalNumbers = numbers.Count();
Console.WriteLine($"Total numbers: {totalNumbers}\n");

var percentages = new List<double>();
for(char i = '1'; i <= '9'; i++)
{
    var appereances = firstDigitInNumbers.Count(n => n == i);
    var percentage = (appereances / (double)totalNumbers) * 100F;
    Console.WriteLine($"digit {i} appears {appereances,4} times, which makes a % of {percentage:F1}");
    percentages.Add(percentage);
}
