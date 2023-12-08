namespace csteeves.Advent2023;

public class CamelNode {

    public readonly string name;
    public string left;
    public string right;

    public CamelNode(string line) {
        List<string> tokens = LineParser.Tokens(line);
        name = tokens[0];

        left = tokens[2].Substring(1, 3);
        right = tokens[3].Substring(0, 3);
    }

    public override string ToString() {
        return $"{name}: L:{left} R:{right}";
    }
}
