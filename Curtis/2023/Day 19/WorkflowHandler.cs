namespace csteeves.Advent2023;

public class WorkflowHandler {

    private Dictionary<string, Workflow> workflows = [];

    public void Add(Workflow workflow) {
        workflows[workflow.name] = workflow;
    }

    public bool RunWorkflows(PartRanking ranking) {
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

    public long GetAcceptedCombos() {
        long result = 0;
        Queue<WorkflowConditions> queue = [];
        queue.Enqueue(new WorkflowConditions("in"));

        while (queue.Any()) {
            WorkflowConditions workflowConditions = queue.Dequeue();
            string workflowName = workflowConditions.workflowName;

            if (workflowName.Equals("R")) {
                continue;
            }

            if (workflowName.Equals("A")) {
                result += workflowConditions.DistinctCombos();
                continue;
            }

            Workflow workflow = workflows[workflowName];
            foreach (WorkflowConditions nextConditions
                    in workflow.NextWorkflowConditions(workflowConditions)) {
                queue.Enqueue(nextConditions);
            }
        }

        return result;
    }
}