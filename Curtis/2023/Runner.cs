using System.Diagnostics;

public class Runner {

    public static readonly string PATH = @"F:\Coding\AdventOfCode2022\Curtis\Input\";

    private static void Main() {
        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();

        DayOneSolution.Run();

        PrintTime(stopWatch.Elapsed);
    }

    public static void PrintTime(TimeSpan ts) {
        Console.WriteLine("RunTime: " + GetTime(ts));
    }
    public static string GetTime(TimeSpan ts) {
        return string.Format("{0:00}:{1:00}:{2:00}.{3:000}",
            ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
    }
}