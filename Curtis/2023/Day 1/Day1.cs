namespace csteeves.Advent2023;

public class Day1 : DaySolution2023 {

    private const string dir = "Day 1";

    public override string Dir() {
        return dir;
    }

    public override void Run(List<string> input) {
        foreach (string line in input) {
            Console.WriteLine(line);
        }
    }
}