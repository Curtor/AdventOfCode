namespace csteeves.Advent2023;

public class HauntedWasteland : DaySolution2023 {

    public override string Dir() {
        return "Day 08";
    }

    public override void Part1(List<string> input) {
        char[] directions = input[0].ToCharArray();
        Dictionary<string, CamelNode> camelNodes = CreateCamelNodes(input);

        CamelNode currentNode = camelNodes["AAA"];
        int steps = 0;
        while (!currentNode.name.Equals("ZZZ")) {
            char direction = directions[steps % directions.Length];

            if (direction == 'L') {
                currentNode = currentNode.leftNode;
            } else if (direction == 'R') {
                currentNode = currentNode.rightNode;
            } else {
                throw new ArgumentOutOfRangeException();
            }

            steps++;
        }

        Console.WriteLine($"Number of camel steps: {steps}");
    }

    public override void Part2(List<string> input) {
        string directions = input[0];
        Dictionary<string, CamelNode> camelNodes = CreateCamelNodes(input);

        PriorityQueue<ulong, CamelNode> currentNodes = new PriorityQueue<ulong, CamelNode>();
        foreach (CamelNode camelNode in camelNodes.Values) {
            if (camelNode.name.Last() == 'A') {
                currentNodes.Enqueue(0, camelNode);
            }
        }

        CamelNodeSearch camelNodeCache = new CamelNodeSearch(directions);
        ulong thresholdIncrement = 10_000_000_000;
        ulong threshold = thresholdIncrement;
        while (!AllCurrentNodesMeetEndCondition(currentNodes)) {
            QueueNode<ulong, CamelNode> nextNode = currentNodes.DequeueNode();
            CamelNodeStep nextZNode = camelNodeCache.NextZNode(nextNode.Priority, nextNode.Value);

            if (nextNode.Priority > threshold) {
                Console.WriteLine(threshold.ToString("N0"));
                threshold += thresholdIncrement;
            }

            currentNodes.Enqueue(nextZNode.step, nextZNode.node);
        }

        // 16,784,396: Too low
        Console.WriteLine($"Number of ghost steps: {currentNodes.PeekNode().Priority}");
    }

    private static Dictionary<string, CamelNode> CreateCamelNodes(List<string> input) {
        Dictionary<string, CamelNode> camelNodes = [];

        for (int i = 2; i < input.Count; i++) {
            CamelNode node = new CamelNode(input[i]);
            camelNodes[node.name] = node;
        }

        foreach (KeyValuePair<string, CamelNode> node in camelNodes) {
            node.Value.leftNode = camelNodes[node.Value.leftName];
            node.Value.rightNode = camelNodes[node.Value.rightName];
        }

        return camelNodes;
    }

    private bool AllCurrentNodesMeetEndCondition(PriorityQueue<ulong, CamelNode> currentNodes) {
        ulong step = currentNodes.PeekNode().Priority;
        foreach (QueueNode<ulong, CamelNode> currentNode in currentNodes) {
            if (currentNode.Priority != step) {
                return false;
            }
            if (currentNode.Value.name.Last() != 'Z') {
                return false;
            }
        }
        return true;
    }
}