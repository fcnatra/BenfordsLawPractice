namespace BenfordsLaw.Domain
{
    public class NumberOfAppereance
    {
        public int Digit { get; set; }
        public int TotalAppereances { get; set; }
        public double PercentageOfAppereances { get; set; }

        public NumberOfAppereance()
        {}

        public NumberOfAppereance(int digit, int totalAppereances, double percentageOfAppereance)
        {
            Digit = digit;
            TotalAppereances = totalAppereances;
            PercentageOfAppereances = percentageOfAppereance;
        }
    }
}
