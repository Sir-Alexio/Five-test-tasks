using System;

class Program
{

    static void Main(string[] args)
    {
        TransformToElephant();

        Console.WriteLine("Муха");

        //... ваш пользовательский код

        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine(i);
        }
    }

    static void TransformToElephant()
    {
        unsafe
        {
            ReadOnlySpan<char> elephant = "Слон";
            fixed (char* ptr = "Муха")
                elephant.CopyTo(new Span<char>(ptr, 4));

        }
    }
}
