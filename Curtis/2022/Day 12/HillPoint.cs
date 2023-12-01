namespace csteeves.Advent2022;

public class HillPoint {

    public readonly bool isStart;
    public readonly bool isEnd;

    public readonly char raw;
    public readonly int height;

    public HillPoint(char raw) {
        this.raw = raw;

        isStart = raw == 'S';
        isEnd = raw == 'E';

        if (isStart) {
            this.height = 0;
        } else if (isEnd) {
            this.height = 'z' - 'a';
        } else {
            height = raw - 'a';
        }
    }

    public override string ToString() {
        return $"{raw}: {height} [S:{isStart}] [E:{isEnd}]";
    }
}