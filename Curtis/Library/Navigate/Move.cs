namespace csteeves;

public class Move<N, T> where N : Node<N, T> {

    public readonly N previousNode;
    public readonly N nextNode;

    public readonly float currentCost;
    public readonly float accumulatedCost;

    private readonly Optional<Move<N, T>> previousMove;
    public Move(N previousNode, NodeNeighbor<N, T> nextNeighbor)
        : this(previousNode, nextNeighbor, null) { }

    public Move(N previousNode, NodeNeighbor<N, T> nextNeighbor, Move<N, T>? previous)
        : this(
            previousNode,
             nextNeighbor.neighbor,
             (previous == null ? 0 : previous.accumulatedCost) + nextNeighbor.cost,
             previous) { }

    private Move(
            N previousNode,
            N nextNode,
            float accumulatedCost,
            Move<N, T>? previous) {
        this.previousNode = previousNode;
        this.nextNode = nextNode;
        this.accumulatedCost = accumulatedCost;
        previousMove = Optional.FromNullable(previous);
    }

    public static Move<N, T> Stationary(N currentNode) {
        return new Move<N, T>(currentNode, currentNode, 0, null);
    }

    public Directions<Move<N, T>, N, T> GetDirections() {
        List<Move<N, T>> orderedMoves = GetAllMovesOrdered();
        return new Directions<Move<N, T>, N, T>(orderedMoves);
    }

    private List<Move<N, T>> GetAllMovesOrdered() {
        List<Move<N, T>> result = [this];
        Optional<Move<N, T>> previousOptional = previousMove;
        while (previousOptional.Present) {
            Move<N, T> previous = previousOptional.Value;
            result.Insert(0, previous);
            previousOptional = previous.previousMove;
        }
        return result;
    }
}

