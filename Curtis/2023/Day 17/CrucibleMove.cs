namespace csteeves.Advent2023;

public class CrucibleMove {

    public static int minLineMoveLength = 1;
    public static int maxLineMoveLength = int.MaxValue - 1;

    public readonly int accumulatedHeat;
    public readonly GridNode<int> current;
    public readonly CrucibleMove? previous;

    private List<CrucibleMove>? cachedLine = null;
    private List<CrucibleMove> MovesLine {
        get {
            if (cachedLine == null) {
                cachedLine = [this];
                cachedLine.AddRange(PriorMovesInThisLine());
                cachedLine.Reverse();
            }
            return cachedLine;
        }
    }

    public int LegLength => MovesLine.Count - 1;

    public CrucibleMove(
            int accumulatedHeat,
            GridNode<int> current,
            CrucibleMove? previous) {
        this.accumulatedHeat = accumulatedHeat;
        this.current = current;
        this.previous = previous;
    }

    public bool ValidNextMove(GridNode<int> next) {
        if (previous != null && previous.current == next) {
            return false;
        }

        if (MovesLine.Count - 1 < minLineMoveLength) {
            return ContinuesLine(next);
        }

        if (MovesLine.Count < maxLineMoveLength + 1) {
            return true;
        }

        if (next.coord.x == current.coord.x
            && next.coord.x == previous.current.coord.x) {
            return false;
        }

        if (next.coord.y == current.coord.y
            && next.coord.y == previous.current.coord.y) {
            return false;
        }

        return true;
    }

    private bool ContinuesLine(GridNode<int> next) {
        if (previous == null) {
            return true;
        }

        bool movingAlongCol = previous.current.coord.x == current.coord.x;
        if (movingAlongCol) {
            return current.coord.x == next.coord.x;
        }
        return current.coord.y == next.coord.y;
    }

    public override int GetHashCode() {
        return current.GetHashCode();
    }

    public override bool Equals(object? obj) {
        return obj is CrucibleMove move
            && move.current == current
            && SameMoveLines(move.MovesLine);
    }

    private bool SameMoveLines(List<CrucibleMove> movesLine) {
        if (this.MovesLine.Count != movesLine.Count) {
            return false;
        }

        for (int i = 0; i < movesLine.Count; i++) {
            if (this.MovesLine[i].current != movesLine[i].current) {
                return false;
            }
        }

        return true;
    }

    private IEnumerable<CrucibleMove> PriorMovesInThisLine() {
        if (previous == null) {
            yield break;
        }

        bool movingAlongCol = previous.current.coord.x == current.coord.x;

        CrucibleMove? currentPrevious = previous;
        while (currentPrevious != null) {
            if (movingAlongCol) {
                if (currentPrevious.current.coord.x != current.coord.x) {
                    yield break;
                }
                yield return currentPrevious;
                currentPrevious = currentPrevious.previous;
            } else {
                if (currentPrevious.current.coord.y != current.coord.y) {
                    yield break;
                }
                yield return currentPrevious;
                currentPrevious = currentPrevious.previous;
            }
        }
    }

    public override string ToString() {
        return $"H:{accumulatedHeat} at {current} from {previous?.current}";
    }
}