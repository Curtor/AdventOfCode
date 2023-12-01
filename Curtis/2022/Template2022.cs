namespace csteeves.Advent2022;

public class Template2022 : DaySolution2022 {

    private const string dir = "Day X";

    public override string Dir() {
        return dir;
    }

    public override void Run(List<string> input) {
        Part1(input);
        Console.WriteLine();
        Part2(input);
    }

    public void Part1(List<string> commands) {
        Console.WriteLine("Part 1");
        Console.WriteLine("Answer: foobar");
    }

    public void Part2(List<string> commands) {
        Console.WriteLine("Part 2");
        Console.WriteLine("Answer: foobar");
    }
}