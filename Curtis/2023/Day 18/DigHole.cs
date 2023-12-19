namespace csteeves.Advent2023;

public class DigHole {

    public readonly DigInstruction instruction;

    public DigHole(DigInstruction instruction) {
        this.instruction = instruction;
    }

    public override string ToString() {
        return "#";
    }
}
