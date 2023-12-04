namespace csteeves.AdventLibrary;

public class QueueNode<T> {

    public float Priority { get; set; }
    public T Value { get; private set; }

    public QueueNode(float priority, T value) {
        Priority = priority;
        Value = value;
    }

    public override string ToString() {
        return $"{Priority}: {Value}";
    }
}