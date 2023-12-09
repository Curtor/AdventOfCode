namespace csteeves;

public class BinaryNode<T> {

    public T value;

    public BinaryNode<T>? left;
    public BinaryNode<T>? right;

    public BinaryNode(T value) {
        this.value = value;
    }
    public BinaryNode(T value, BinaryNode<T>? left, BinaryNode<T>? right) {
        this.value = value;

        this.left = left;
        this.right = right;
    }
}
