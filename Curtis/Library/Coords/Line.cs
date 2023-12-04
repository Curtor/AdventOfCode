namespace csteeves;

public class Line {

    public Vector2Int pointA;
    public Vector2Int pointB;

    public Line(Vector2Int pointA, Vector2Int pointB) {
        this.pointA = pointA;
        this.pointB = pointB;
    }

    public Line(int xA, int yA, int xB, int yB)
        : this(new Vector2Int(xA, yA), new Vector2Int(xB, yB)) { }

    internal bool isDiagonal() {
        return pointA.x != pointB.x && pointA.y != pointB.y;
    }

    public override string ToString() {
        return $"{pointA} -> {pointB}";
    }
}