namespace csteeves.Advent2023;

public class Workflow {

    public readonly string line;
    public readonly string name;

    private readonly List<Condition> conditions = [];

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

    public IEnumerable<string> TargetWorkflows() {
        return conditions.Select(c => c.targetName);
    }

    public override string ToString() {
        return line;
    }

    public IEnumerable<AcceptedPartConditions> AcceptedPartConditions(
            AcceptedPartConditions currentPartConditions) {

        AcceptedPartConditions acceptedPartConditions =
            new AcceptedPartConditions(name, currentPartConditions);

        for (int i = 0; i < conditions.Count; i++) {
            Condition condition = conditions[i];

            if (condition.targetName.Equals(currentPartConditions.target)) {
                if (condition.op == Condition.Operator.TRUE) {
                    yield return acceptedPartConditions;
                    yield break;
                }

                AcceptedPartConditions continuePartConditions =
                    new AcceptedPartConditions(acceptedPartConditions);
                acceptedPartConditions.Add(condition, true);
                continuePartConditions.Add(condition, false);

                yield return acceptedPartConditions;
                acceptedPartConditions = continuePartConditions;
                continue;
            }

            if (condition.op == Condition.Operator.TRUE) {
                yield break;
            }

            acceptedPartConditions.Add(condition, false);
        }
    }
}