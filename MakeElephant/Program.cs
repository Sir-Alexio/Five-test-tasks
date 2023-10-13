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
        //Создаем unsafe блок для работы с указателем.
        unsafe
        {
            //Создаем строку "Слон" и сохраняем ее в переменную elephant.
            ReadOnlySpan<char> elephant = "Слон";
            //Создаем указатель, который указывает на строку "Муха" с помощью конструктии fixed.
            fixed (char* ptr = "Муха")
                //Копируем содержимое строки "Слон" в строку "Муха".
                elephant.CopyTo(new Span<char>(ptr, 4));
        }
    }
}
