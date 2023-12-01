namespace csteeves.Advent2022;

public class CalorieCounting : DaySolution2022 {

    private const string dir = "Day 1";

    public override string Dir() {
        return dir;
    }

    public override void Part1(List<string> input) {
        List<int> caloriePacks = GetCaloriePacks(input);
        Console.WriteLine($"Max calories: {caloriePacks.First()}");
    }

    public override void Part2(List<string> input) {
        List<int> caloriePacks = GetCaloriePacks(input);
        int firstThree = caloriePacks[0] + caloriePacks[1] + caloriePacks[2];
        Console.WriteLine($"First 3 combined: {firstThree}");
    }

    private static List<int> GetCaloriePacks(List<string> input) {
        List<int> caloriePacks = [];
        int runningCalories = 0;

        foreach (string line in input) {
            if (string.IsNullOrWhiteSpace(line)) {
                caloriePacks.Add(runningCalories);
                runningCalories = 0;
                continue;
            }

            int lineCalories = int.Parse(line);
            runningCalories += lineCalories;
        }
        caloriePacks.Add(runningCalories);

        caloriePacks.Sort();
        caloriePacks.Reverse();
        return caloriePacks;
    }
}