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
        //Environment.Exit(1);

        //Environment.FailFast("Fail");

        Process.GetCurrentProcess().Kill();
    }
}