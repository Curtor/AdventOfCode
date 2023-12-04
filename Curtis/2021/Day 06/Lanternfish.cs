namespace csteeves.Advent2021;

using csteeves.AdventLibrary;

public class Lanternfish : DaySolution2021 {

    private Dictionary<int, long> reproduction = [];

    public override string Dir() {
        return "Day 06";
    }

    public override void Part1(List<string> input) {
        List<string> tokens = LineParser.Tokens(input[0], ",");

        PriorityQueue<int> queue = new PriorityQueue<int>();
        foreach (string token in tokens) {
            int daysRemaining = int.Parse(token);
            queue.Enqueue(daysRemaining, 0);
        }

        for (int i = 0; i <= 80; i++) {
            while (queue.PeekNode().Item1 < i) {
                queue.Dequeue();
                queue.Enqueue(i + 6, 0);
                queue.Enqueue(i + 8, 0);
            }

            // PrettyPrint(i, queue);
        }

        int count = queue.Count;
        Console.WriteLine($"Lanternfish count: {count}");
    }

    public override void Part2(List<string> input) {
        // This solution also works for Part 1
        // However, the solution used for Part 1 will take too long to run on this large a number.
        RunForDays(input, 256);
    }

    private void RunForDays(List<string> input, int days) {
        long count = 0;
        List<string> tokens = LineParser.Tokens(input[0], ",");
        foreach (string token in tokens) {
            int daysRemainingBeforeReproduction = int.Parse(token);
            int daysToRun = days - daysRemainingBeforeReproduction - 1;
            count += daysToRun < 0 ? 1 : GetCountAfterDays(daysToRun);
        }

        Console.WriteLine($"Lanternfish count: {count}");
    }

    /** Returns the total number of lanternfish after n days if it reproduces today */
    private long GetCountAfterDays(int days) {
        // Reuse previously calculated values.
        if (reproduction.TryGetValue(days, out long count)) {
            return count;
        }

        long result;
        if (days < 7) {
            // For the first 6 days, the only the first fish reproduced.
            result = 2;
        } else if (days < 9) {
            // On the 6th and 7th days, the first fish reproduced again, but not the second one.
            result = 3;
        } else {
            // We are at least 9 days out.
            result = GetCountAfterDays(days - 7) + GetCountAfterDays(days - 9);
        }

        reproduction[days] = result;
        return result;
    }

    private void PrettyPrint(int day, PriorityQueue<int> queue) {
        string attendance = queue.Select(node => node.Priority - day).Join(",");
        Console.WriteLine($"Day {day}: {queue.Count} {attendance}");
    }
}