namespace csteeves;

public static class IDictionaryExt {

    public static TValue GetOrCreate<TKey, TValue>(
            this IDictionary<TKey, TValue> dict, TKey key) where TValue : new() {
        if (!dict.TryGetValue(key, out TValue? val)) {
            val = new();
            dict.Add(key, val);
        }

        return val;
    }
    public static TValue GetOrCreate<TKey, TValue>(
            this IDictionary<TKey, TValue> dict, TKey key, Func<TValue> factory) {
        if (!dict.TryGetValue(key, out TValue? val)) {
            val = factory.Invoke();
            dict.Add(key, val);
        }

        return val;
    }

    public static void AddOrSet<TKey>(this IDictionary<TKey, int> dict, TKey key, int num) {
        if (dict.ContainsKey(key)) {
            dict[key] += num;
        } else {
            dict.Add(key, num);
        }
    }

    public static void AddOrSet<TKey>(this IDictionary<TKey, float> dict, TKey key, float num) {
        if (dict.ContainsKey(key)) {
            dict[key] += num;
        } else {
            dict.Add(key, num);
        }
    }

    public static int GetCount<TKey>(this IDictionary<TKey, int> dict, TKey key) {
        if (dict.ContainsKey(key)) {
            return dict[key];
        }
        return 0;
    }
}