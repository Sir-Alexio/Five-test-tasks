
# Тестовые задания

## В этом файле я дам несколько пояснений к заданиям, которые помогут понять ход моих мыслей.

### 1.  Ломай меня полностью.  
   Реализуйте метод FailProcess так, чтобы процесс завершался. Предложите побольше различных решений.

```csharp
public class Program
{
    private static void Main(string[] args)
    {
        try
        {
            FailProcess();
        }
        catch { }

        Console.WriteLine("Failed to fail the process!");
        Console.ReadKey();
    }

    static void FailProcess()
    {
        // Этот метод используется для завершения текущего процесса и выхода из программы с указанным кодом завершения.
        // Environment.Exit(1);

        // Этот метод также используется для завершения текущего процесса, но он предназначен для обработки критических ошибок.
        // Environment.FailFast("Fail");

        // Сначала получаем информацию о текущем процессе и затем принудительно его останавливаем.
        Process.GetCurrentProcess().Kill();
    }
}
```

Выше приведены 3 способа для завершения процесса и их краткое описание.

### 2.  Операция «Ы».
Что выводится на экран? Измените класс Number так, чтобы на экран выводился результат сложения для любых значений someValue1 и someValue2.

До изменения класса, на экран выводилась конкатенация двух строк: 1+2=12.

```csharp
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

        //Здесь просто переопределяем оператор
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
```

Как видно, из кода выше, для решения задачи достаточно было переопределить оператор +. При переопределении мы конвертировали string к int и далее суммировали два целых числа. Результат приводился к строке.

### 3.  Мне только спросить!

Реализуйте метод по следующей сигнатуре:

```csharp
/// <summary>
/// <para> Отсчитать несколько элементов с конца </para>
/// <example> new[] {1,2,3,4}.EnumerateFromTail(2) = (1, ), (2, ), (3, 1), (4, 0)</example>
/// </summary> 
/// <typeparam name="T"></typeparam>
/// <param name="enumerable"></param>
/// <param name="tailLength">Сколько элеметнов отсчитать с конца  (у последнего элемента tail = 0)</param>
/// <returns></returns>
public static IEnumerable<(T item, int? tail)> EnumerateFromTail<T>(this IEnumerable<T> enumerable, int? tailLength)
```

Возможно ли реализовать такой метод выполняя перебор значений перечисления только 1 раз?

Да, возможно и я хочу предложить два решения:

Первое решение будет не самым оптимальным по скорости, потому что в нем мы переворачиваем исходный массив. Зато оно интуитивно понятное: 

```csharp
 public static IEnumerable<(T item, int? tail)> EnumerateFromTail<T>(this IEnumerable<T> enumerable, int? tailLength)
 {
    if (tailLength < 0)
    {
        throw new ArgumentException("Wrong tailLength.");
    }

     //Создаем переменную для результата
     List<(T item, int? tail)> result = new List<(T item, int? tail)>(capacity: enumerable.Count());

     //Переменная для присваивания значений хвосту
     int i = 0;

     //Создаем временную переменную для того, чтобы не дублировать вызов result.Add()
     int? temp;

     //Переворачиваем иcходнный массив
     foreach (T item in enumerable.Reverse())
     {
         if (i >= tailLength) { temp = null; }
         else { temp = i; }

         result.Add((item, temp));

         i++;
     }

     result.Reverse();
     return result;
 }
```

И второй вариант:

```csharp
public static IEnumerable<(T item, int? tail)> EnumerateFromTail<T>(this IEnumerable<T> enumerable, int? tailLength)
{
    if (tailLength < 0)
    {
        throw new ArgumentException("Wrong tailLength.");
    }

    //Приводим к массиву для перебора
    T[] items = enumerable.ToArray();

    int length = items.Length;

    //Массив для результата
    List<(T item, int? tail)> result = new List<(T item, int? tail)>(length);

    for (int i = 0; i < length; i++)
    {
        //По умолчанию tail=null пока не дойдем до хвоста
        int? tail = null;
        if (i >= length - tailLength)
        {
            //высчитываем хвост
            tail = length - i - 1;
        }
        result.Add((items[i], tail));
    }

    return result;
}
```

Во втором варианте производится только один перебор и он работает быстрее. Ниже представлены затраты по памяти и времени выполнения для двух алгоритмов, первого и второго соответсвенно:

![Alt text](image.png)

![Alt text](image-1.png)

Как можно заметить, выигрыш по времени почти в 2 раза(25 и 13 секунд).

После этого, я решил оптимизировать первое решение с помощью Linq:

```csharp
 public static IEnumerable<(T item, int? tail)> EnumerateFromTail<T>(this IEnumerable<T> enumerable, int? tailLength)
 {
     //Решение с Linq намного эффективнее, чем решение выше как по памяти, так и по времени выполнения, проверенно на 100 000 000 элементов в массиве
     return enumerable
     .Reverse()
     .Select((item, i) => (item, tail: (i >= tailLength) ? (int?)null : i))
     .Reverse();
 }
```

и результаты меня впечатлили:

![Alt text](image-2.png)

Использование памяти было сокращено с 2.5 Гб до 429 Мб и время работы было сокращено до 3 секунд.

Подводя итог, могу заметить, что Linq оказался намного эффективнее в решении этой задачи.