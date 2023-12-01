namespace csteeves.Advent2021;

public class Dive : DaySolution2021 {

    private const string dir = "Day 2";

    public override string Dir() {
        return dir;
    }

    public override void Run(List<string> input) {
        List<SubCommand> commands = [];
        foreach (string line in input) {
            List<string> tokens = LineParser.Tokens(line);
            SubCommand command = new SubCommand(tokens);
            commands.Add(command);
        }

        Part1(commands);
        Console.WriteLine("----");
        Part2(commands);
    }

    public void Part1(List<SubCommand> commands) {
        int depth = 0;
        int distance = 0;

        foreach (SubCommand command in commands) {

            switch (command.type) {
                case SubCommand.CommandType.FORWARD:
                    distance += command.magnitude;
                    break;
                case SubCommand.CommandType.DOWN:
                    depth += command.magnitude;
                    break;
                case SubCommand.CommandType.UP:
                    depth -= command.magnitude;
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        Console.WriteLine("Part 1");
        Console.WriteLine($"Depth: {depth}");
        Console.WriteLine($"Distance: {distance}");
        Console.WriteLine($"Product: {depth * distance}");
    }

    public void Part2(List<SubCommand> commands) {
        int depth = 0;
        int distance = 0;
        int aim = 0;

        foreach (SubCommand command in commands) {

            switch (command.type) {
                case SubCommand.CommandType.FORWARD:
                    distance += command.magnitude;
                    depth += aim * command.magnitude;
                    break;
                case SubCommand.CommandType.DOWN:
                    aim += command.magnitude;
                    break;
                case SubCommand.CommandType.UP:
                    aim -= command.magnitude;
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        Console.WriteLine("Part 2");
        Console.WriteLine($"Depth: {depth}");
        Console.WriteLine($"Distance: {distance}");
        Console.WriteLine($"Aim: {aim}");
        Console.WriteLine($"Product: {depth * distance}");
    }
}