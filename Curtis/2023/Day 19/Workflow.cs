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

    public IEnumerable<WorkflowConditions> NextWorkflowConditions(
            WorkflowConditions currentConditions) {

        foreach (Condition condition in conditions) {
            if (condition.op == Condition.Operator.TRUE) {
                yield return new WorkflowConditions(condition.targetName, currentConditions);
                yield break;
            }

            WorkflowConditions acceptedConditions =
                new WorkflowConditions(condition.targetName, currentConditions);
            acceptedConditions.Add(condition);
            yield return new WorkflowConditions(condition.targetName, acceptedConditions);

            currentConditions.Add(condition, false);
        }
    }
}