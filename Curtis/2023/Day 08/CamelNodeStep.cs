namespace csteeves.Advent2023;

public class CamelNodeStep {

    public ulong step;
    public CamelNode node;

    public CamelNodeStep(ulong step, CamelNode node) {
        this.step = step;
        this.node = node;
    }

    public override string ToString() {
        return $"S:{step} {node.name}";
    }
}
