namespace csteeves.Advent2023;

public class Aplenty : DaySolution2023 {

    public override string Dir() {
        return "Day 19";
    }

    public override void Part1(List<string> input) {
        WorkflowHandler handler = new WorkflowHandler();

        int rankingSums = 0;
        bool doneAddingWorkflows = false;
        foreach (string line in input) {
            if (string.IsNullOrEmpty(line)) {
                doneAddingWorkflows = true;
                continue;
            }

            if (!doneAddingWorkflows) {
                handler.Add(new Workflow(line));
            } else {
                PartRanking ranking = new PartRanking(line);
                bool accepted = handler.RunWorkflows(ranking);
                if (accepted) {
                    rankingSums += ranking.RanksSum();
                }
            }
        }

        Console.WriteLine($"Ranking sum: {rankingSums}");
    }

    public override void Part2(List<string> input) {
        Console.WriteLine($"Answer 2: {input[0]}");
    }
}