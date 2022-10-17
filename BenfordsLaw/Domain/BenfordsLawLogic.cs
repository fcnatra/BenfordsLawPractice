namespace BenfordsLaw.Domain
{
    public class BenfordsLawLogic
    {
        public List<NumberOfAppereance> LawNumbers()
        {
            var lawNumbers = new List<NumberOfAppereance>
            {
                new NumberOfAppereance(1, 0, 30.1),
                new NumberOfAppereance(2, 0, 17.6),
                new NumberOfAppereance(3, 0, 12.5),
                new NumberOfAppereance(4, 0,  9.7),
                new NumberOfAppereance(5, 0,  7.9),
                new NumberOfAppereance(6, 0,  6.7),
                new NumberOfAppereance(7, 0,  5.8),
                new NumberOfAppereance(8, 0,  5.1),
                new NumberOfAppereance(9, 0,  4.6)
            };

            return lawNumbers;
        }

        public List<NumberOfAppereance> CalculatePercentages(List<double> numbers) => Calculate(numbers);

        private List<NumberOfAppereance> CalculatePercentages(List<int> numbers) => Calculate(numbers);

        private List<NumberOfAppereance> Calculate<T>(List<T> numbers) where T : IComparable
        {
            var totalNumbers = numbers.Count();
            List<char>? firstDigits = numbers.Select(x => (x?.ToString() ?? "\0")[0]).ToList();

            var lawCalculation = new List<NumberOfAppereance>();

            for (char i = '1'; i <= '9'; i++)
            {
                int appereances = firstDigits.Count(n => n == i);
                double percentage = appereances / (double)totalNumbers * 100;
                lawCalculation.Add(new NumberOfAppereance(int.Parse(i.ToString()), appereances, percentage));
            }

            return lawCalculation;
        }
    }
}
