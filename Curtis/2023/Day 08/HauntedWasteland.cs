namespace csteeves.Advent2023;

public class HauntedWasteland : DaySolution2023 {

    public override string Dir() {
        return "Day 08";
    }

    public override void Part1(List<string> input) {
        char[] directions = input[0].ToCharArray();
        Dictionary<string, CamelNode> nodes = [];

        for (int i = 2; i < input.Count; i++) {
            CamelNode node = new CamelNode(input[i]);
            nodes[node.name] = node;
        }

        CamelNode currentNode = nodes["AAA"];
        int steps = 0;
        while (!currentNode.name.Equals("ZZZ")) {
            char direction = directions[steps % directions.Length];

            if (direction == 'L') {
                currentNode = nodes[currentNode.left];
            } else if (direction == 'R') {
                currentNode = nodes[currentNode.right];
            } else {
                throw new ArgumentOutOfRangeException();
            }

            steps++;
        }

        Console.WriteLine($"Number of camel steps: {steps}");
    }

    private bool AllCurrentNodesEndInZ(List<CamelNode> currentNodes) {
        foreach (CamelNode node in currentNodes) {
            if (node.name.Last() != 'Z') {
                return false;
            }
        }

        return true;
    }

    public override void Part2(List<string> input) {
        char[] directions = input[0].ToCharArray();
        Dictionary<string, CamelNode> nodes = [];
        PriorityQueue<CamelNode> currentNodes = new PriorityQueue<CamelNode>();

        for (int i = 2; i < input.Count; i++) {
            CamelNode node = new CamelNode(input[i]);
            nodes[node.name] = node;

            if (node.name.Last() == 'A') {
                currentNodes.Enqueue(0, node);
            }
        }

        Dictionary<Tuple<int, CamelNode>, Tuple<int, CamelNode>> nextEndNode = [];

        while (!AllCurrentNodesMeetEndCondition(currentNodes)) {
            QueueNode<CamelNode> currentNode = currentNodes.DequeueNode();

            int currentStep = (int)currentNode.Priority;
            int stepIndex = currentStep % directions.Length;
            Tuple<int, CamelNode> currentPointer = Tuple.Create(stepIndex, currentNode.Value);

            if (nextEndNode.TryGetValue(currentPointer, out Tuple<int, CamelNode>? nextZ)) {
                currentNodes.Enqueue(currentStep + nextZ.Item1, nextZ.Item2);
                continue;
            }

            char direction = directions[stepIndex];
            CamelNode nextNode;
            if (direction == 'L') {
                nextNode = nodes[currentNode.Value.left];
            } else if (direction == 'R') {
                nextNode = nodes[currentNode.Value.right];
            } else {
                throw new ArgumentOutOfRangeException();
            }

            if (nextNode.name.Last() == 'Z') {
                currentNodes.Enqueue(currentStep + 1, nextNode);
                nextEndNode.Add(currentPointer, Tuple.Create(1, nextNode));
                continue;
            }

            Tuple<int, CamelNode> nextPointer =
                Tuple.Create((currentStep + 1) % directions.Length, nextNode);
            if (nextEndNode.TryGetValue(nextPointer, out nextZ)) {
                nextEndNode.Add(
                    currentPointer,
                    Tuple.Create(nextZ.Item1 + 1, nextZ.Item2));
                currentNodes.Enqueue(currentStep + 1 + nextZ.Item1, nextZ.Item2);
            } else {
                currentNodes.Enqueue(currentStep + 1, nextNode);
            }
        }

        // 16,784,396: Too low
        Console.WriteLine($"Number of ghost steps: {currentNodes.PeekNode().Priority}");
    }

    private bool AllCurrentNodesMeetEndCondition(PriorityQueue<CamelNode> currentNodes) {
        int step = (int)currentNodes.PeekNode().Priority;
        foreach (QueueNode<CamelNode> currentNode in currentNodes) {
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