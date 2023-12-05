namespace csteeves.Advent2023;

public class Range {
    public readonly long start;
    public readonly long length;

    public Range(long start, long length) {
        this.start = start;
        this.length = length;
    }

    public override string ToString() {
        return $"[S: {start}; L: {length}]";
    }
}