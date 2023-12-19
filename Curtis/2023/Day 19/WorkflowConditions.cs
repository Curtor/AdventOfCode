namespace csteeves.Advent2023;

public class WorkflowConditions {

    private readonly int PART_CATEGORY_MIN = 1;
    private readonly int PART_CATEGORY_MAX = 4000;

    public readonly string workflowName;

    // Conditions needed to reach the start of the workflow named above.
    private readonly List<Condition> conditions = [];

    public WorkflowConditions(string workflow) {
        this.workflowName = workflow;
    }

    public WorkflowConditions(string workflow, WorkflowConditions other) : this(workflow) {
        conditions.AddRange(other.conditions);
    }

    public void Add(Condition condition, bool meet = true) {
        if (meet) {
            conditions.Add(condition);
        } else {
            conditions.Add(Condition.Inverse(condition));
        }
    }

    public long DistinctCombos() {
        SortedList<int, int> xRanges = GetNewRange();
        SortedList<int, int> mRanges = GetNewRange();
        SortedList<int, int> aRanges = GetNewRange();
        SortedList<int, int> sRanges = GetNewRange();

        foreach (Condition condition in conditions) {
            switch (condition.category) {
                case PartRanking.Category.X:
                    Merge(xRanges, condition);
                    continue;
                case PartRanking.Category.M:
                    Merge(mRanges, condition);
                    continue;
                case PartRanking.Category.A:
                    Merge(aRanges, condition);
                    continue;
                case PartRanking.Category.S:
                    Merge(sRanges, condition);
                    continue;
            }
            throw new ArgumentOutOfRangeException();
        }

        long result = 1;
        result *= RangeSize(xRanges);
        result *= RangeSize(mRanges);
        result *= RangeSize(aRanges);
        result *= RangeSize(sRanges);
        return result;
    }

    private SortedList<int, int> GetNewRange() {
        return new SortedList<int, int> {
            { PART_CATEGORY_MIN, PART_CATEGORY_MAX + 1 - PART_CATEGORY_MIN }
        };
    }

    private void Merge(SortedList<int, int> ranges, Condition condition) {
        switch (condition.op) {
            case Condition.Operator.LT:
                LimitRangeToBelow(ranges, condition.threshold);
                return;
            case Condition.Operator.GT:
                LimitRangeToAbove(ranges, condition.threshold);
                return;
        }
        throw new ArgumentOutOfRangeException();
    }

    private void LimitRangeToBelow(SortedList<int, int> ranges, int threshold) {
        for (int i = ranges.Count - 1; i >= 0; i--) {
            if (ranges.ElementAt(i).Key >= threshold) {
                ranges.RemoveAt(i);
            }
            break;
        }

        KeyValuePair<int, int> kvp = ranges.Last();
        if (kvp.Key + kvp.Value - 1 >= threshold) {
            ranges[kvp.Key] = threshold - kvp.Key;
        }
    }

    private void LimitRangeToAbove(SortedList<int, int> ranges, int threshold) {
        while (ranges.First().Key + ranges.First().Value - 1 <= threshold) {
            ranges.RemoveAt(0);
        }

        KeyValuePair<int, int> kvp = ranges.First();
        if (kvp.Key <= threshold) {
            ranges.RemoveAt(0);

            ranges[threshold + 1] = kvp.Key + kvp.Value - threshold - 1;
        }
    }

    private long RangeSize(SortedList<int, int> ranges) {
        int result = 0;
        foreach (KeyValuePair<int, int> kvp in ranges) {
            result += kvp.Value;
        }
        return result;
    }
}