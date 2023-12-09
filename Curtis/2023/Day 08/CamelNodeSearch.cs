namespace csteeves.Advent2023;

public class CamelNodeSearch {

    private string directions;
    private Dictionary<CamelNode, CamelNodeStep[]> cache = [];

    public CamelNodeSearch(string directions) {
        this.directions = directions;
    }

    public CamelNodeStep NextZNode(ulong completedSteps, CamelNode camelNode) {
        int index = (int)(completedSteps % (ulong)directions.Length);

        CamelNodeStep stepsToNextZ = SearchForZ(index, camelNode);
        return new CamelNodeStep(completedSteps + stepsToNextZ.step, stepsToNextZ.node);
    }

    public CamelNodeStep SearchForZ(int stepIndex, CamelNode startingCamelNode) {
        if (!cache.TryGetValue(startingCamelNode, out CamelNodeStep[]? camelNodesCache)) {
            camelNodesCache = new CamelNodeStep[directions.Length];
            cache[startingCamelNode] = camelNodesCache;
        } else if (camelNodesCache[stepIndex] != null) {
            return camelNodesCache[stepIndex];
        }

        List<CamelNode> visited = [startingCamelNode];
        CamelNode camelNode = startingCamelNode;
        int additionalSteps = 0;
        CamelNode? prefoundZ = null;

        do {
            int index = (stepIndex + visited.Count - 1) % directions.Length;
            char direction = directions[index];

            if (direction == 'L') {
                camelNode = camelNode.leftNode;
            } else if (direction == 'R') {
                camelNode = camelNode.rightNode;
            } else {
                throw new ArgumentOutOfRangeException();
            }

            int nextIndex = (index + 1) % directions.Length;
            if (!cache.TryGetValue(camelNode, out camelNodesCache)) {
                cache[camelNode] = new CamelNodeStep[directions.Length];
            } else if (camelNodesCache[nextIndex] != null) {
                additionalSteps = 1 + (int)camelNodesCache[nextIndex].step;
                prefoundZ = camelNodesCache[nextIndex].node;
                break;
            }

            visited.Add(camelNode);
        } while (camelNode.name.Last() != 'Z');

        CamelNode foundZ = prefoundZ ?? visited.Last();

        for (int i = 0; i < visited.Count; i++) {
            CamelNode localNode = visited[i];
            int stepsToZ = visited.Count - i - 1 + additionalSteps;

            if (stepsToZ == 0) {
                continue;
            }

            CamelNodeStep localResult = new CamelNodeStep((ulong)stepsToZ, foundZ);
            int localStepIndex = (stepIndex + i) % directions.Length;
            cache[localNode][localStepIndex] = localResult;
        }

        CamelNodeStep result = cache[startingCamelNode][stepIndex];
        return result;
    }
}
