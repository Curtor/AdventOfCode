namespace csteeves.Advent2021;

public class Dive : DaySolution2021 {

    public override string Dir() {
        return "Day 02";
    }

    public override void Part1(List<string> input) {
        List<SubCommand> commands = GetSubCommands(input);

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

        Console.WriteLine($"Depth: {depth}");
        Console.WriteLine($"Distance: {distance}");
        Console.WriteLine($"Product: {depth * distance}");
    }

    public override void Part2(List<string> input) {
        List<SubCommand> commands = GetSubCommands(input);

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

        Console.WriteLine($"Depth: {depth}");
        Console.WriteLine($"Distance: {distance}");
        Console.WriteLine($"Aim: {aim}");
        Console.WriteLine($"Product: {depth * distance}");
    }

    private static List<SubCommand> GetSubCommands(List<string> input) {
        List<SubCommand> commands = [];
        foreach (string line in input) {
            List<string> tokens = LineParser.Tokens(line);
            SubCommand command = new SubCommand(tokens);
            commands.Add(command);
        }
        return commands;
    }
}