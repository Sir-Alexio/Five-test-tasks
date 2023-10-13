using System.Threading.Channels;

public class Program
{
    private static void Main(string[] args)
    {
        //Создание массива на 1 000 000 000 элементов с максимальным значением в 2000
        List<int> values = new List<int>(capacity: 1000000000);
        int j = 0;
        for (int i = 0; i < values.Capacity; i++)
        {
            values.Add(j);
            if (j == 2000) { j = 0; }
            j++;
        }

        Sort(values, 10, 2000);
        Console.WriteLine("Done");
    }

    /// <summary>
    /// Возвращает отсортированный по возрастанию поток чисел
    /// </summary>
    /// <param name="inputStream">Поток чисел от 0 до maxValue. Длина потока не превышает миллиарда чисел.</param>
    /// <param name="sortFactor">Фактор упорядоченности потока. Неотрицательное число. Если в потоке встретилось число x, то в нём больше не встретятся числа меньше, чем (x - sortFactor).</param>
    /// <param name="maxValue">Максимально возможное значение чисел в потоке. Неотрицательное число, не превышающее 2000.</param>
    /// <returns>Отсортированный по возрастанию поток чисел.</returns>
    public static IEnumerable<int> Sort(IEnumerable<int> inputStream, int sortFactor, int maxValue)
    {
        //Создаем массив для результата
        List<int> result = new List<int>(capacity: inputStream.Count());

        //Создаем временный массив для хранения количества повторений элементов в массиве. Это возможно благодаря тому, что есть диапазон значений, которые встречаются в массиве.
        int[] temp = new int[maxValue + 1];

        //Заполнене массива: как только встречается элемент в массиве, счетчит увеличивает значение на 1.
        foreach (int i in inputStream)
        {
            temp[i]++;
        }

        //Далее идем по временному массиву и собираем отсортированный массив.
        for (int i = 0; i < maxValue + 1; i++)
        {
            for (int j = 0; j < temp[i]; j++)
            {
                result.Add(i);
            }
        }

        Console.WriteLine($"Size of temp array: {sizeof(int)*temp.Length}");
        return result;
    }

    //Метод для вывода на экран
    public static void Print(IEnumerable<int> result)
    {
        string resultString = string.Join(", ", result);
        Console.WriteLine(resultString);
    }

}