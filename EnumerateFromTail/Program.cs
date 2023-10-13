

public static class Program
{
    private static void Main(string[] args)
    {
        new int[] { 1, 2, 3, 4 }.EnumerateFromTail(2).PrintResult();
    }

    /// <summary>
    /// <para> Отсчитать несколько элементов с конца </para>
    /// <example> new[] {1,2,3,4}.EnumerateFromTail(2) = (1, ), (2, ), (3, 1), (4, 0)</example>
    /// </summary> 
    /// <typeparam name="T"></typeparam>
    /// <param name="enumerable"></param>
    /// <param name="tailLength">Сколько элеметнов отсчитать с конца  (у последнего элемента tail = 0)</param>
    /// <returns></returns>


    //Метод для вывода значений на экран
    public static void PrintResult<T>(this IEnumerable<(T item, int? tail)> values)
    {
        foreach (var item in values)
        {
            Console.WriteLine(item);
        }
    }

    public static IEnumerable<(T item, int? tail)> EnumerateFromTail<T>(this IEnumerable<T> enumerable, int? tailLength)
    {
        //Создаем переменную для результата
        List<(T item, int? tail)> result = new List<(T item, int? tail)>(capacity: enumerable.Count());

        //Переменная для присваивания значений хвосту
        int i = 0;

        //Создаем временную переменную для того, чтобы не дублировать вызов result.Add()
        int? temp;

        //Переворачиваем изходнный массив
        foreach (T item in enumerable.Reverse())
        {
            if (i >= tailLength) { temp = null; }
            else { temp = i; }

            result.Add((item, temp));

            i++;
        }

        result.Reverse();
        return result;

        //Решение с Linq намного эффективнее, чем решение выше как по памяти, так и по времени выполнения, проверенно на 100 000 000 элементов в массиве
        return enumerable
        .Reverse()
        .Select((item, i) => (item, tail: (i >= tailLength) ? (int?)null : i))
        .Reverse();
    }
}