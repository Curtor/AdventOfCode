namespace csteeves.Advent2023;

public class WorkflowHandler {

    private Dictionary<string, Workflow> workflows = [];

    internal void Add(Workflow workflow) {
        workflows[workflow.name] = workflow;
    }

    internal bool RunWorkflows(PartRanking ranking) {
        Workflow workflow = workflows["in"];

        string nextWorkflowName;
        while (true) {
            nextWorkflowName = workflow.Run(ranking);

            if (nextWorkflowName.Equals("A") || nextWorkflowName.Equals("R")) {
                break;
            }

            workflow = workflows[nextWorkflowName];
        }

        return nextWorkflowName.Equals("A");
    }
}