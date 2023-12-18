namespace csteeves;

public abstract class Node<N, T> where N : Node<N, T> {

    public T? value;
    public float cost = 1;

    public Func<N, float> heuristicDistance = node => 1f;

    public Node(T? value = default(T), float cost = 1) {
        this.value = value;
        this.cost = cost;
    }

    public IEnumerable<Node<N, T>> TraverseBreadthFirst() {
        Queue<Node<N, T>> queue = new Queue<Node<N, T>>();
        HashSet<Node<N, T>> visited = [];

        queue.Enqueue(this);
        visited.Add(this);

        while (queue.Any()) {
            Node<N, T> front = queue.Dequeue();
            foreach (NodeNeighbor<N, T> nodeNeighbor in front.NextNodes()) {
                Node<N, T> node = nodeNeighbor.neighbor;
                if (!visited.Contains(node)) {
                    queue.Enqueue(node);
                    visited.Add(node);
                }
            }
            yield return front;
        }
    }

    public IEnumerable<N> TraverseDepthFirst() {
        return TraverseDepthFirst([]);
    }

    public IEnumerable<N> TraverseDepthFirst(HashSet<N> visited) {
        Stack<N> stack = new Stack<N>();

        stack.Push((N)this);
        visited.Add((N)this);

        while (stack.Any()) {
            N top = stack.Pop();
            foreach (NodeNeighbor<N, T> nodeNeighbor in top.NextNodes()) {
                N node = nodeNeighbor.neighbor;
                if (!visited.Contains(node)) {
                    stack.Push(node);
                    visited.Add(node);
                }
            }
            yield return top;
        }
    }

    public Directions<Move<N, T>, N, T> GetRoute(IEnumerable<N> targets) {
        return GetRoute(targets.ToArray());
    }

    public Directions<Move<N, T>, N, T> GetRoute(params N[] targets) {
        Move<N, T> closestMove = Move<N, T>.Stationary((N)this);
        if (targets.Contains(this)) {
            return closestMove.GetDirections();
        }

        PriorityQueue<Move<N, T>> queue = new();
        HashSet<N> visited = [(N)this];

        EnqueueNextMoves((N)this, queue, visited);

        Tuple<N, float> closeAsWeCanGet = GetClosest((N)this, targets);

        while (queue.Count > 0) {
            Move<N, T> move = queue.Dequeue();
            if (targets.Contains(move.nextNode)) {
                return move.GetDirections();
            }

            Tuple<N, float> howCloseWeCurrentlyAre = GetClosest(move.nextNode, targets);
            if (howCloseWeCurrentlyAre.Item2 < closeAsWeCanGet.Item2) {
                closestMove = move;
                closeAsWeCanGet = howCloseWeCurrentlyAre;
            }

            EnqueueNextMoves(move.nextNode, queue, visited, move);
        }
        return closestMove.GetDirections();
    }

    private void EnqueueNextMoves(
            N currentNode,
            PriorityQueue<Move<N, T>> queue,
            HashSet<N> visited,
            Move<N, T>? previousMove = null) {
        foreach (NodeNeighbor<N, T> nodeNeighbor in currentNode.NextNodes()) {
            if (visited.Contains(nodeNeighbor.neighbor)) {
                continue;
            }

            Move<N, T> move = new Move<N, T>(currentNode, nodeNeighbor, previousMove);
            queue.Enqueue(move.accumulatedCost, move);
            visited.Add(nodeNeighbor.neighbor);
        }
    }

    private static Tuple<N, float> GetClosest(N start, IEnumerable<N> targets) {
        N closestNode = targets.First();
        float closestDistance = float.MaxValue;
        foreach (N target in targets) {
            float distance = start.heuristicDistance(target);
            if (distance < closestDistance) {
                closestNode = target;
                closestDistance = distance;
            }
        }
        return Tuple.Create(closestNode, closestDistance);
    }

    public abstract IEnumerable<NodeNeighbor<N, T>> NextNodes();

    public override string ToString() {
        return $"Node: {value}";
    }
}
