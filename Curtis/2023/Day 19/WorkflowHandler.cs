namespace csteeves.Advent2023;

public class WorkflowHandler {

    private Dictionary<string, Workflow> workflows = [];
    private Dictionary<string, List<Workflow>> workflowsWithTarget = [];

    public void Add(Workflow workflow) {
        workflows[workflow.name] = workflow;

        foreach (string target in workflow.TargetWorkflows()) {
            workflowsWithTarget.GetOrCreate(target).Add(workflow);
        }
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
        Queue<Tuple<Workflow, AcceptedPartConditions>> queue =
            new Queue<Tuple<Workflow, AcceptedPartConditions>>(
                workflowsWithTarget["A"].Select(w
                    => Tuple.Create(w, new AcceptedPartConditions("A"))));

        long result = 0;
        while (queue.Any()) {
            Tuple<Workflow, AcceptedPartConditions> current = queue.Dequeue();
            Workflow workflow = current.Item1;
            AcceptedPartConditions conditions = current.Item2;

            foreach (AcceptedPartConditions newConditions
                    in workflow.AcceptedPartConditions(conditions)) {

                if (newConditions.target.Equals("in")) {
                    result += newConditions.DistinctCombos();
                    continue;
                }

                foreach (Workflow nextWorkflow in workflowsWithTarget[newConditions.target]) {
                    AcceptedPartConditions nextConditions =
                            new AcceptedPartConditions(newConditions.target, newConditions);
                    queue.Enqueue(Tuple.Create(nextWorkflow, nextConditions));
                }
            }

        }
        return result;
    }
}