

public static class Program
{
    private static void Main(string[] args)
    {
        var list = new List<int>(capacity:100000000);
        for (int i = 0; i < list.Capacity; i++)
        {
            list.Add(i);
        }
        //new int[] { 1, 2, 3, 4 }.EnumerateFromTail(2).PrintResult();
        list.EnumerateFromTail(50);
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

    //public static IEnumerable<(T item, int? tail)> EnumerateFromTail<T>(this IEnumerable<T> enumerable, int? tailLength)
    //{
    //    if (tailLength < 0)
    //    {
    //        throw new ArgumentException("Wrong tailLength.");
    //    }

    //    //Приводим к массиву для перебора
    //    T[] items = enumerable.ToArray();

    //    int length = items.Length;

    //    //Массив для результата
    //    List<(T item, int? tail)> result = new List<(T item, int? tail)>(length);

    //    for (int i = 0; i < length; i++)
    //    {
    //        //По умолчанию tail=null пока не дойдем до хвоста
    //        int? tail = null;
    //        if (i >= length - tailLength)
    //        {
    //            //высчитываем хвост
    //            tail = length - i - 1;
    //        }
    //        result.Add((items[i], tail));
    //    }

    //    return result;
    //}

    public static IEnumerable<(T item, int? tail)> EnumerateFromTail<T>(this IEnumerable<T> enumerable, int? tailLength)
    {
        //if (tailLength < 0)
        //{
        //    throw new ArgumentException("Wrong tailLength.");
        //}
        ////Создаем переменную для результата
        //List<(T item, int? tail)> result = new List<(T item, int? tail)>(capacity: enumerable.Count());

        ////Переменная для присваивания значений хвосту
        //int i = 0;

        ////Создаем временную переменную для того, чтобы не дублировать вызов result.Add()
        //int? temp;

        ////Переворачиваем иcходнный массив
        //foreach (T item in enumerable.Reverse())
        //{
        //    if (i >= tailLength) { temp = null; }
        //    else { temp = i; }

        //    result.Add((item, temp));

        //    i++;
        //}

        //result.Reverse();
        //return result;

        //Решение с Linq намного эффективнее, чем решение выше как по памяти, так и по времени выполнения, проверенно на 100 000 000 элементов в массиве
        return enumerable
        .Reverse()
        .Select((item, i) => (item, tail: (i >= tailLength) ? (int?)null : i))
        .Reverse();
    }
}