namespace csteeves.Advent2023;

public class Lens {

    public readonly string name;
    public int focalLength;

    public Lens(string name, int focalLength) {
        this.name = name;
        this.focalLength = focalLength;
    }

    public override string ToString() {
        return $"[{name} {focalLength}]";
    }
}