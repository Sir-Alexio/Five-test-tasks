using System.Globalization;

public class Program
{
    static readonly IFormatProvider _ifp = CultureInfo.InvariantCulture;
    class Number
    {
        readonly int _number;
        public Number(int number)
        {
            _number = number;
        }
        public override string ToString()
        {
            return _number.ToString(_ifp);
        }

        public static string operator +(Number first, string second)
        {
            return (first._number + Convert.ToInt32(second)).ToString();
        }
    }
    static void Main(string[] args)
    {
        int someValue1 = 1000000;
        int someValue2 = -500;
        string result = new Number(someValue1) + someValue2.ToString(_ifp);
        Console.WriteLine(result);
        Console.ReadKey();
    }
}