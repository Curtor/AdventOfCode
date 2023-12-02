namespace csteeves;

using System.Diagnostics;

public class Runner {

    public const string PATH = @"F:\Coding\AdventOfCode\Curtis\";

    public const string DEBUG_INPUT = "Debug.txt";
    public const string DEBUG_ALT_INPUT = "DebugPart2.txt";

    public const string REAL_INPUT = "Input.txt";
    public const string REAL_ALT_INPUT = "InputPart2.txt";

    private static int year = 2021;
    private static int day = 7;
    private static bool debugOnly = false;

    private static void Main() {
        Stopwatch stopWatch = new Stopwatch();
        DaySolution? solution = Solutions.Get(year, day);

        if (solution == null) {
            Console.WriteLine($"No solution yet for {year} day {day}");
            return;
        }


        Console.WriteLine("---- ---- ---- ----");
        Console.WriteLine();
        Console.WriteLine($" ~~ {solution.GetType().Name} ~~");
        Console.WriteLine();
        Console.WriteLine("---- ---- ---- ----");
        Console.WriteLine();
        Console.WriteLine("TESTING:");
        Console.WriteLine();

        stopWatch.Start();
        solution.RunDebug();
        stopWatch.Stop();

        Console.WriteLine();
        PrintTime(stopWatch.Elapsed);
        Console.WriteLine();
        Console.WriteLine("---- ---- ---- ----");
        Console.WriteLine();

        if (debugOnly) {
            return;
        }

        Console.WriteLine("ANSWER:");
        Console.WriteLine();

        stopWatch.Restart();
        solution.Run();
        stopWatch.Stop();

        Console.WriteLine();
        PrintTime(stopWatch.Elapsed);
        Console.WriteLine();
        Console.WriteLine("---- ---- ---- ----");
        Console.WriteLine();
    }

    public static void PrintTime(TimeSpan ts) {
        Console.WriteLine("RunTime: " + GetTime(ts));
    }
    public static string GetTime(TimeSpan ts) {
        return string.Format("{0:00}:{1:00}:{2:00}.{3:000}",
            ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
    }
}