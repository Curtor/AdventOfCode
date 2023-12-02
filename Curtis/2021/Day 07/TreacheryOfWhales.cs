namespace csteeves.Advent2021;

public class TreacheryOfWhales : DaySolution2021 {

    private Dictionary<int, long> reproduction = [];

    public override string Dir() {
        return "Day 07";
    }

    public override void Part1(List<string> input) {
        List<int> numbers = GetSortedNumbers(input);
        int median = numbers[numbers.Count / 2];

        int fuel = 0;
        foreach (int i in numbers) {
            fuel += Math.Abs(median - i);
        }

        Console.WriteLine($"Median: {median}");
        Console.WriteLine($"Fuel: {fuel}");
    }

    public override void Part2(List<string> input) {
        List<int> numbers = GetSortedNumbers(input);

        double average = numbers.Sum() / (double)numbers.Count;
        int averageFloored = (int)Math.Floor(average);
        int averageCeiled = (int)Math.Ceiling(average);

        int fuelFloored = 0;
        int fuelCeiled = 0;
        foreach (int i in numbers) {
            int distanceFloored = Math.Abs(averageFloored - i);
            int distanceCeiled = Math.Abs(averageCeiled - i);

            fuelFloored += (distanceFloored * (distanceFloored + 1)) / 2;
            fuelCeiled += (distanceCeiled * (distanceCeiled + 1)) / 2;
        }

        int bestFuel = Math.Min(fuelFloored, fuelCeiled);

        Console.WriteLine($"Average: {average}");
        Console.WriteLine($"Expensive Fuel: {bestFuel}");
    }

    private static List<int> GetSortedNumbers(List<string> input) {
        List<string> tokens = LineParser.Tokens(input[0], ",");
        List<int> numbers = tokens.Select(t => int.Parse(t)).ToList();
        numbers.Sort();
        return numbers;
    }
}