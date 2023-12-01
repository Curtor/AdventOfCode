namespace csteeves.Advent2021;

public class SubCommand {

    public enum CommandType { FORWARD, UP, DOWN }

    public readonly CommandType type;
    public readonly int magnitude;

    public SubCommand(List<string> tokens) {
        switch (tokens[0]) {
            case "forward":
                type = CommandType.FORWARD;
                break;
            case "up":
                type = CommandType.UP;
                break;
            case "down":
                type = CommandType.DOWN;
                break;
            default:
                throw new ArgumentException();
        }

        magnitude = int.Parse(tokens[1]);
    }
}
