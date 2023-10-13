using System.Diagnostics;

public class Program
{
    private static void Main(string[] args)
    {
        try
        {
            FailProcess();
        }
        catch { }

        Console.WriteLine("Failed to fail process!");
        Console.ReadKey();
    }

    static void FailProcess()
    {
        //Этот метод используется для завершения текущего процесса и выхода из программы с указанным кодом завершения.
        //Environment.Exit(1);

        //Этот метод также используется для завершения текущего процесса, но он предназначен для обработки критических ошибок.
        //Environment.FailFast("Fail");

        //Сначала получаем информацию о текущем процессе и потом его принудительно останавливаем.
        Process.GetCurrentProcess().Kill();
    }
}