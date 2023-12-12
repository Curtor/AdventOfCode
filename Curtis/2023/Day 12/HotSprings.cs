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

        int comboSums = GetCombos(statuses);

        Console.WriteLine($"Sum of possible combinations: {comboSums}");
    }

    public override void Part2(List<string> input) {
        List<SpringStatus> statuses = [];
        foreach (string line in input) {
            statuses.Add(new SpringStatus(line, true));
        }

        int comboSums = GetCombos(statuses);

        Console.WriteLine($"Sum of possible unfolded combinations: {comboSums}");
    }

    private static int GetCombos(List<SpringStatus> statuses) {
        int comboSums = 0;
        foreach (SpringStatus status in statuses) {
            int possibleCombos = status.GetComboCount();
            comboSums += possibleCombos;
            Console.WriteLine($"{possibleCombos} for {status}");
        }
        return comboSums;
    }

}