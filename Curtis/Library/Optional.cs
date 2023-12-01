namespace csteeves;

public static class Optional {
    public static Optional<T> Of<T>(T value) {
        return new Optional<T>(value);
    }

    public static Optional<T> Absent<T>() {
        return Optional<T>.Absent();
    }

    public static Optional<T> FromNullable<T>(T? value) {
        return value == null ? Absent<T>() : Of(value);
    }
}

public struct Optional<T> {

    public bool Present { get; private set; }

    private T value;

    public T Value {
        get {
            if (Present) {
                return value;
            } else {
                throw new InvalidOperationException("Attempted to get absent optional.");
            }
        }
    }

    public Optional(T value) {
        if (value == null) {
            throw new ArgumentNullException("value");
        }

        this.value = value;
        Present = true;
    }

    public static Optional<T> Absent() {
        return new() { Present = false };
    }

    public static explicit operator T(Optional<T> optional) {
        return optional.Value;
    }

    public static implicit operator Optional<T>(T value) {
        return new(value);
    }

    public override bool Equals(object? obj) {
        return obj is Optional<T> optional && this.Equals(optional);
    }

    public bool Equals(Optional<T> other) {
        if (Present && other.Present) {
            return object.Equals(value, other.value);
        }
        return Present == other.Present;
    }

    public override int GetHashCode() {
        return value != null ? value.GetHashCode() : 0;
    }

    public override string ToString() {
        return Present ? $"Optional[{value}]" : "Optional.Absent";
    }
}