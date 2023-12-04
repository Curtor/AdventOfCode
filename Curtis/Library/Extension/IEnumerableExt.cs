namespace csteeves.AdventLibrary;

public static class IEnumerableExt {

    public static string Join<T>(this IEnumerable<T> enumerable, string seperator) {
        return string.Join(seperator, enumerable);
    }
}