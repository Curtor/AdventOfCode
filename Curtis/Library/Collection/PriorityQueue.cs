namespace csteeves;

using System.Collections;

public class PriorityQueue<T> : IEnumerable<QueueNode<T>> {

    private List<QueueNode<T>> queue = [];

    private int _heapSize = -1;
    private readonly bool isMinPriorityQueue;

    public int Count => queue.Count;

    public PriorityQueue(bool isMinPriorityQueue = true) {
        this.isMinPriorityQueue = isMinPriorityQueue;
    }

    public void Enqueue(float priority, T value) {
        QueueNode<T> node = new QueueNode<T>(priority, value);

        queue.Add(node);
        _heapSize++;

        // Maintaining heap
        if (isMinPriorityQueue) {
            BuildHeapMin(_heapSize);
        } else {
            BuildHeapMax(_heapSize);
        }
    }

    public T Dequeue() {
        return DequeueNode().Value;
    }

    public QueueNode<T> DequeueNode() {
        if (_heapSize < 0) {
            throw new InvalidOperationException("Queue is empty");
        }

        QueueNode<T> node = queue[0];
        queue[0] = queue[_heapSize];
        queue.RemoveAt(_heapSize);
        _heapSize--;

        if (isMinPriorityQueue) {
            MinHeapify(0);
        } else {
            MaxHeapify(0);
        }

        return node;
    }

    public T Peek() {
        return PeekNode().Value;
    }

    public QueueNode<T> PeekNode() {
        if (_heapSize < 0) {
            throw new InvalidOperationException("Queue is empty");
        }
        return queue[0];
    }

    public void UpdatePriority(T obj, int priority) {
        for (int i = 0; i <= _heapSize; i++) {
            QueueNode<T> node = queue[i];
            if (object.ReferenceEquals(node.Value, obj)) {
                node.Priority = priority;
                if (isMinPriorityQueue) {
                    BuildHeapMin(i);
                    MinHeapify(i);
                } else {
                    BuildHeapMax(i);
                    MaxHeapify(i);
                }
            }
        }
    }

    public bool Contains(T obj) {
        foreach (QueueNode<T> node in queue) {
            if (object.ReferenceEquals(node.Value, obj)) {
                return true;
            }
        }

        return false;
    }

    private void BuildHeapMax(int i) {
        while (i >= 0 && queue[(i - 1) / 2].Priority < queue[i].Priority) {
            Swap(i, (i - 1) / 2);
            i = (i - 1) / 2;
        }
    }

    private void BuildHeapMin(int i) {
        while (i >= 0 && queue[(i - 1) / 2].Priority > queue[i].Priority) {
            Swap(i, (i - 1) / 2);
            i = (i - 1) / 2;
        }
    }
    private void MaxHeapify(int i) {
        int left = ChildLeft(i);
        int right = ChildRight(i);

        int heighst = i;

        if (left <= _heapSize && queue[heighst].Priority < queue[left].Priority) {
            heighst = left;
        }

        if (right <= _heapSize && queue[heighst].Priority < queue[right].Priority) {
            heighst = right;
        }

        if (heighst != i) {
            Swap(heighst, i);
            MaxHeapify(heighst);
        }
    }
    private void MinHeapify(int i) {
        int left = ChildLeft(i);
        int right = ChildRight(i);

        int lowest = i;

        if (left <= _heapSize && queue[lowest].Priority > queue[left].Priority) {
            lowest = left;
        }

        if (right <= _heapSize && queue[lowest].Priority > queue[right].Priority) {
            lowest = right;
        }

        if (lowest != i) {
            Swap(lowest, i);
            MinHeapify(lowest);
        }
    }

    private void Swap(int i, int j) {
        QueueNode<T> temp = queue[i];
        queue[i] = queue[j];
        queue[j] = temp;
    }
    private int ChildLeft(int i) {
        return i * 2 + 1;
    }
    private int ChildRight(int i) {
        return i * 2 + 2;
    }

    public IEnumerator<QueueNode<T>> GetEnumerator() {
        return queue.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return queue.GetEnumerator();
    }
}