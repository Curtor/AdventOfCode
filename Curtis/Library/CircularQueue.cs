using System.Collections;

namespace csteeves;

public class CircularQueue<T> : IEnumerable<T> {

    private T[] elements;
    private int startIndex = 0;

    public int Count { get; private set; } = 0;

    public CircularQueue(int size) {
        elements = new T[size];
    }

    public void Enqueue(T value, bool overwrite = false) {
        if (Count == elements.Length) {
            if (!overwrite) {
                throw new ArgumentOutOfRangeException("Circular queue is full");
            }

            elements[startIndex] = value;
            startIndex = (startIndex + 1) % elements.Length;
        }

        int insertIndex = (startIndex + Count) % elements.Length;
        elements[insertIndex] = value;
        Count++;
    }

    public T Dequeue() {
        if (Count == 0) {
            throw new ArgumentOutOfRangeException("Circular queue is empty");
        }

        T result = elements[startIndex];
        startIndex = (startIndex + 1) % elements.Length;
        Count--;
        return result;
    }

    public T Peek() {
        if (Count == 0) {
            throw new ArgumentOutOfRangeException("Circular queue is empty");
        }
        return elements[startIndex];
    }

    public bool Any() {
        return Count > 0;
    }

    public IEnumerator<T> GetEnumerator() {
        return ((IEnumerable<T>)elements).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return elements.GetEnumerator();
    }
}
