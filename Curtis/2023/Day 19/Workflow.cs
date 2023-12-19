
namespace csteeves.Advent2023;

internal class Workflow {

    public readonly string line;
    public readonly string name;

    private List<Condition> conditions = [];

    public Workflow(string line) {
        this.line = line;

        int braceIndex = line.IndexOf('{');
        name = line.Substring(0, braceIndex);

        string rawConditions = line.Substring(braceIndex + 1, line.Length - braceIndex - 2);
        List<string> conditionTokens = LineParser.Tokens(rawConditions, ",");
        foreach (string conditionToken in conditionTokens) {
            conditions.Add(new Condition(conditionToken));
        }
    }

    public string Run(PartRanking ranking) {
        foreach (Condition condition in conditions) {
            if (condition.Met(ranking)) {
                return condition.targetName;
            }
        }

        throw new ApplicationException();
    }

    public override string ToString() {
        return line;
    }
}