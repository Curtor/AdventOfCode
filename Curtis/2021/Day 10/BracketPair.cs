namespace csteeves.Advent2021;

public class BracketPair {

    public readonly char open;
    public readonly char close;

    public BracketPair(char open, char close) {
        this.open = open;
        this.close = close;
    }

    public override string ToString() {
        return $"{open}-{close}";
    }
}
