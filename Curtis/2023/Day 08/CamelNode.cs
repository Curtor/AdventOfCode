namespace csteeves.Advent2023;

public class CamelNode {

    public readonly string name;
    public string leftName;
    public string rightName;

    public CamelNode leftNode;
    public CamelNode rightNode;

    public CamelNode(string line) {
        List<string> tokens = LineParser.Tokens(line);
        name = tokens[0];

        leftName = tokens[2].Substring(1, 3);
        rightName = tokens[3].Substring(0, 3);
    }

    public override string ToString() {
        return $"{name}: L:{leftName} R:{rightName}";
    }
}
