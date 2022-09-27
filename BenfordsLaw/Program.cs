// See https://aka.ms/new-console-template for more information


Console.WriteLine("Benford's Law");
Console.WriteLine(" 1   30.1 %");
Console.WriteLine(" 2   17.6 %");
Console.WriteLine(" 3   12.5 %");
Console.WriteLine(" 4   9.7 % ");
Console.WriteLine(" 5   7.9 % ");
Console.WriteLine(" 6   6.7 % ");
Console.WriteLine(" 7   5.8 % ");
Console.WriteLine(" 8   5.1 % ");
Console.WriteLine(" 9   4.6 % ");


Console.WriteLine("\nDatasource analysis result");
List<double> numbers = ReadNumbers();
List<char>? firstDigitInNumbers = numbers.Select(x => x.ToString()[0]).ToList();

var totalNumbers = numbers.Count();
Console.WriteLine($"Total numbers: {totalNumbers}\n");

var percentages = new List<double>();
for(char i = '1'; i <= '9'; i++)
{
    var appereances = firstDigitInNumbers.Count(n => n == i);
    var percentage = (appereances / (double)totalNumbers) * 100F;
    Console.WriteLine($"digit {i} appears {appereances,4} times, which makes a % of {(int)percentage}");
    percentages.Add(percentage);
}

// draw here a horizontal bar chart with Benford's law %s and another below with data analysis %s

List<double> ReadNumbers()
{
    var reader = new StreamReader(".\\DataSource\\conjunto datos.csv");

    string fileContent = reader.ReadToEnd();
    string[] linesInFile = fileContent.Split("\r\n");
    
    var dataInFile = new List<DataStruct>();
    foreach (string line in linesInFile)
    {
        if (IsTheFirstLine(line) || string.IsNullOrEmpty(line))
            continue;

        var fields = line.Split(";");
        
        dataInFile.Add(new DataStruct
        {
            Province = fields[0].ToUpper(),
            Nacionality = fields[1].ToUpper(),
            Continent = fields[2].ToUpper(),
            Value = double.Parse(fields[3])
        });
    }

    var numbers = dataInFile
        .Where(x => x.Nacionality.Contains("ESPAÑ") && x.Continent.Contains("ESPAÑ"))
        .Select(x => x.Value);

    return numbers.ToList();
}

bool IsTheFirstLine(string line) => line.EndsWith("Total");

class DataStruct
{
    public string? Province;
    public string? Nacionality;
    public string? Continent;
    public double Value;
};

