namespace csteeves.Advent2023;

public class HotSprings : DaySolution2023 {

    public override string Dir() {
        return "Day 12";
    }

    public override void Part1(List<string> input) {
        List<SpringStatus> statuses = [];
        foreach (string line in input) {
            statuses.Add(new SpringStatus(line));
        }

        long comboSums = GetCombos(statuses);

        Console.WriteLine($"Sum of possible combinations: {comboSums}");
    }

    public override void Part2(List<string> input) {
        List<SpringStatus> statuses = [];
        foreach (string line in input) {
            statuses.Add(new SpringStatus(line, true));
        }

        long comboSums = GetCombos(statuses);

        Console.WriteLine($"Sum of possible unfolded combinations: {comboSums}");
    }

    private static long GetCombos(List<SpringStatus> statuses) {
        long comboSums = 0;
        foreach (SpringStatus status in statuses) {
            long possibleCombos = status.GetComboCount();
            comboSums += possibleCombos;
            Console.WriteLine($"{possibleCombos} for {status}");
        }
        return comboSums;
    }

}