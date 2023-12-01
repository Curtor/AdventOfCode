namespace csteeves;

public class Vector2Int {

    public static readonly Vector2Int ORIGIN = new Vector2Int(0, 0);

    public int x;
    public int y;

    public Vector2Int(int x, int y) {
        this.x = x;
        this.y = y;
    }

    public float Magnitude(Vector2Int other) {
        return Distance(ORIGIN);
    }

    public float Distance(Vector2Int other) {
        int deltaX = other.x - x;
        int deltaY = other.y - y;
        return MathF.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
    }

    public override int GetHashCode() {
        return x.GetHashCode() + y.GetHashCode();
    }

    public override bool Equals(object? obj) {
        return obj is Vector2Int vector
            && vector.x == x
            && vector.y == y;
    }

    public override string ToString() {
        return $"[{x}, {y}]";
    }

}