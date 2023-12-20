namespace csteeves.Advent2023;

public class DigSiteV2 {

    private readonly SortedList<int, Tuple<Vector2Int, Vector2Int>> horizontalTopToBottom = [];
    private readonly SortedList<int, Tuple<Vector2Int, Vector2Int>> verticalLeftToRight = [];

    public DigSiteV2(List<DigInstruction> digInstructions) {
        Vector2Int start = new Vector2Int(0, 0);
        foreach (DigInstruction digInstruction in digInstructions) {
            Vector2Int end = digInstruction.GetNextPosition(start);
            switch (digInstruction.direction) {
                case Grid.Direction.LEFT:
                    AddHorizontal(end, start);
                    break;
                case Grid.Direction.RIGHT:
                    AddHorizontal(start, end);
                    break;
                case Grid.Direction.UP:
                    AddVertical(end, start);
                    break;
                case Grid.Direction.DOWN:
                    AddVertical(start, end);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private void AddHorizontal(Vector2Int start, Vector2Int end) {
        horizontalTopToBottom.Add(start.y, Tuple.Create(start, end));
    }

    private void AddVertical(Vector2Int start, Vector2Int end) {
        verticalLeftToRight.Add(start.x, Tuple.Create(start, end));
    }

    public long GetInteriorSize() {
        long result = 0;
        for (int i = 0; i < horizontalTopToBottom.Count - 1; i++) {
            Tuple<Vector2Int, Vector2Int> currentHorizontal
                = horizontalTopToBottom.ElementAt(i).Value;

            Tuple<Vector2Int, Vector2Int> nextHorizontal
                = horizontalTopToBottom.ElementAt(i + 1).Value;
            if (currentHorizontal.Item1.y == nextHorizontal.Item1.y) {
                continue;
            }

            List<Tuple<Vector2Int, Vector2Int>> nextHorizontals = [nextHorizontal];
            for (int j = i + 2; j < horizontalTopToBottom.Count; j++) {
                nextHorizontal = horizontalTopToBottom.ElementAt(j).Value;
                if (nextHorizontal.Item1.y != nextHorizontals[0].Item1.y) {
                    break;
                }
                nextHorizontals.Add(nextHorizontal);
            }

            bool inside = false;
            for (int k = 0; k < verticalLeftToRight.Count - 1; k++) {
                Tuple<Vector2Int, Vector2Int> vertical = verticalLeftToRight.ElementAt(k).Value;
                while (!IsRelevantVertical(
                        vertical, currentHorizontal, nextHorizontals)) {
                    continue;
                }

                inside = !inside;
                if (inside) {
                    Tuple<Vector2Int, Vector2Int> nextVertical;
                    int nextVerticalIndex = k;
                    do {
                        nextVerticalIndex++;
                        nextVertical = verticalLeftToRight.ElementAt(nextVerticalIndex).Value;
                    } while (!IsRelevantVertical(
                        nextVertical, currentHorizontal, nextHorizontals));


                    result += GetArea(vertical, nextVertical, currentHorizontal, nextHorizontals);
                }
            }
        }

        return result;
    }

    private bool IsRelevantVertical(
            Tuple<Vector2Int, Vector2Int> vertical,
            Tuple<Vector2Int, Vector2Int> currentHorizontal,
            List<Tuple<Vector2Int, Vector2Int>> nextHorizontals) {
        if (vertical.Item2.y <= currentHorizontal.Item1.y) {
            return false;
        }
        if (vertical.Item1.y > nextHorizontals[0].Item1.y) {
            return false;
        }
        return true;
    }

    private long GetArea(
            Tuple<Vector2Int, Vector2Int> vertical,
            Tuple<Vector2Int, Vector2Int> nextVertical,
            Tuple<Vector2Int, Vector2Int> currentHorizontal,
            List<Tuple<Vector2Int, Vector2Int>> nextHorizontals) {
        long width = nextVertical.Item1.x - vertical.Item1.x + 1;
        long penultimateHeight = nextHorizontals[0].Item1.y - currentHorizontal.Item1.y;
        long initialArea = width * penultimateHeight;

        long additionalLastLineArea = GetAreaBetween(
            vertical.Item1.x, nextVertical.Item1.x, nextHorizontals);

        long result = initialArea + additionalLastLineArea;
        Console.WriteLine($"Area: {result} ({initialArea} + {additionalLastLineArea})");
        return result;
    }

    private long GetAreaBetween(
            int x1, int x2, List<Tuple<Vector2Int, Vector2Int>> nextHorizontals) {
        // TODO
        return 0;
    }
}
