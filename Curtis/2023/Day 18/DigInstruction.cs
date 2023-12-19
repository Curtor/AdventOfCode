using AdventOfCode.Library;
using System.Drawing;

namespace csteeves.Advent2023;

public class DigInstruction {

    public readonly string rawInput;

    public readonly Grid.Direction direction;
    public readonly int distance;
    public readonly Color color;

    public readonly ConsoleColor consoleColor;

    public DigInstruction(string rawInput) {
        List<string> tokens = LineParser.Tokens(rawInput);
        this.rawInput = rawInput;

        direction = Grid.DirectionFrom(tokens[0][0]);
        distance = int.Parse(tokens[1]);

        color = ColorUtil.FromHexString(ExtractColorHex(tokens[2]));
        consoleColor = ConsoleUtil.ClosestColor(color);
    }

    public DigInstruction(
            string rawInput,
            Grid.Direction direction,
            int distance,
            Color color,
            ConsoleColor consoleColor) {
        this.rawInput = rawInput;
        this.direction = direction;
        this.distance = distance;
        this.color = color;
        this.consoleColor = consoleColor;
    }

    public static DigInstruction ParseHex(string rawInput) {
        List<string> tokens = LineParser.Tokens(rawInput);
        string extractedHexString = ExtractColorHex(tokens[2]);
        string extractedHexDigits = extractedHexString.Substring(1, extractedHexString.Length - 1);

        Grid.Direction direction = GetDirection(extractedHexDigits.Last());
        string distanceHex = extractedHexDigits.Substring(0, extractedHexDigits.Length - 1);
        int distance = Convert.ToInt32($"0x{distanceHex}", 16);

        Color color = ColorUtil.FromHexString(extractedHexString);
        ConsoleColor consoleColor = ConsoleUtil.ClosestColor(color);

        return new DigInstruction(rawInput, direction, distance, color, consoleColor);
    }

    private static string ExtractColorHex(string s) {
        return s.Substring(1, s.Length - 2);
    }

    private static Grid.Direction GetDirection(char c) {
        switch (c) {
            case '0':
                return Grid.Direction.RIGHT;
            case '1':
                return Grid.Direction.DOWN;
            case '2':
                return Grid.Direction.LEFT;
            case '3':
                return Grid.Direction.UP;
        }

        throw new ArgumentOutOfRangeException();
    }

    public Vector2Int GetNextPosition(Vector2Int current) {
        Vector2Int next = new Vector2Int(current);
        switch (direction) {
            case Grid.Direction.LEFT:
                next.x -= distance;
                break;
            case Grid.Direction.RIGHT:
                next.x += distance;
                break;
            case Grid.Direction.UP:
                next.y -= distance;
                break;
            case Grid.Direction.DOWN:
                next.y += distance;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        return next;
    }

    public override string ToString() {
        return $"{direction}{distance}";
    }
}
