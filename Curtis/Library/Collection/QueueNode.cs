namespace csteeves;

public class QueueNode<C, T> where C : IComparable<C> {

    public C Priority { get; set; }
    public T Value { get; private set; }

    public QueueNode(C priority, T value) {
        Priority = priority;
        Value = value;
    }

    public override string ToString() {
        return $"{Priority}: {Value}";
    }
}