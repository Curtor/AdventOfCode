namespace csteeves.Advent2023;

public class PipeNode {

    public char value;
    public bool onPath = false;

    public PipeNode(char value) {
        this.value = value;
    }

    public override string ToString() {
        return value.ToString();
    }
}
