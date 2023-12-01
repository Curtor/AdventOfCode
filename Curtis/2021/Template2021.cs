namespace csteeves.Advent2021;

public class Template2021 : DaySolution2021 {

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