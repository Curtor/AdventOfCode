namespace csteeves.Advent2023;

public class CubeGameSet {

    public int Blue { get; private set; }
    public int Red { get; private set; }
    public int Green { get; private set; }

    public CubeGameSet(string set) {
        List<string> colors = LineParser.Tokens(set, ",");
        foreach (string color in colors) {
            List<string> tokens = LineParser.Tokens(color);
            string colorName = tokens[1];
            int count = int.Parse(tokens[0]);

            switch (colorName) {
                case "blue":
                    Blue = count;
                    break;
                case "red":
                    Red = count;
                    break;
                case "green":
                    Green = count;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public bool IsPossible(int r, int g, int b) {
        return r >= Red && g >= Green && b >= Blue;
    }
}
