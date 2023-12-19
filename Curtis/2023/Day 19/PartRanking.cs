namespace csteeves.Advent2023;

public class PartRanking {

    public enum Category { UNSPECIFIED, X, M, A, S }

    public readonly int x;
    public readonly int m;
    public readonly int a;
    public readonly int s;

    public PartRanking(string input) {
        x = GetRanking("x", input);
        m = GetRanking("m", input);
        a = GetRanking("a", input);
        s = GetRanking("s", input);
    }

    private int GetRanking(string key, string input) {
        int index = input.IndexOf(key) + key.Length;
        int endIndex = input.IndexOf(",", index);
        if (endIndex < 0) {
            endIndex = input.Length - 1;
        }

        string payload = input.Substring(index + 1, endIndex - index - 1);
        return int.Parse(payload);
    }

    public int Get(Category c) {
        switch (c) {
            case Category.X: return x;
            case Category.M: return m;
            case Category.A: return a;
            case Category.S: return s;
        }
        throw new ArgumentOutOfRangeException();
    }

    public int RanksSum() {
        return x + m + a + s;
    }

    public static Category CategoryFrom(string s) {
        switch (s) {
            case "x": return Category.X;
            case "m": return Category.M;
            case "a": return Category.A;
            case "s": return Category.S;
        }
        throw new ArgumentOutOfRangeException();
    }
}