namespace csteeves.Advent2023;

public class SpringStatus {

    private static Dictionary<Tuple<string, string>, long> cache = [];

    private string statusLine;
    private List<int> damageCounts;

    public SpringStatus(string line, bool unfold = false) {
        List<string> tokens = LineParser.Tokens(line);

        statusLine = tokens[0];
        damageCounts = LineParser.Tokens(tokens[1], ",").Select(int.Parse).ToList();

        if (unfold) {
            string originalStatusLine = statusLine;
            int originalDamageCount = damageCounts.Count;
            for (int i = 0; i < 4; i++) {
                statusLine += '?' + originalStatusLine;
                for (int j = 0; j < originalDamageCount; j++) {
                    damageCounts.Add(damageCounts[j]);
                }
            }
        }
    }

    public long GetComboCount() {
        return GetComboCount(statusLine, damageCounts);
    }

    private static long GetComboCount(
            string statusLine, List<int> damageCounts, int damageIndex = 0) {
        string damageCountsSerialized =
            damageCounts.GetRange(damageIndex, damageCounts.Count - damageIndex).Join(",");
        Tuple<string, string> key = Tuple.Create(statusLine, damageCountsSerialized);
        if (cache.TryGetValue(key, out long cachedResult)) {
            return cachedResult;
        }

        if (!IsComboPossible(statusLine, damageCounts, damageIndex)) {
            cache[key] = 0;
            return 0;
        }

        int seenDamage = 0;
        int damageListCount = damageCounts.Count;

        for (int i = 0; i < statusLine.Length; i++) {
            char c = statusLine[i];

            if (seenDamage > 0 && damageCounts[damageIndex] == seenDamage) {
                if (c == '#') {
                    cache[key] = 0;
                    return 0;
                }

                damageIndex++;
                seenDamage = 0;
                continue;
            }

            if (c == '?') {
                if (seenDamage == 0) {
                    string statusLineSubstring = statusLine.Substring(i + 1);
                    long combos =
                        GetComboCount("#" + statusLineSubstring, damageCounts, damageIndex)
                        + GetComboCount(statusLineSubstring, damageCounts, damageIndex);
                    cache[key] = combos;
                    return combos;
                }

                seenDamage++;
                continue;
            }

            if (c == '#') {
                if (damageIndex >= damageListCount) {
                    cache[key] = 0;
                    return 0;
                }

                seenDamage++;
                continue;
            }

            if (seenDamage > 0) {
                if (damageCounts[damageIndex] != seenDamage) {
                    cache[key] = 0;
                    return 0;
                }

                damageIndex++;
                seenDamage = 0;
                continue;
            }
        }

        if (damageIndex < damageListCount - 1) {
            cache[key] = 0;
            return 0;
        }

        if (damageIndex == damageListCount - 1) {
            int result = seenDamage == damageCounts[damageIndex] ? 1 : 0;
            cache[key] = result;
            return result;
        }

        if (damageIndex == damageListCount) {
            int result = seenDamage == 0 ? 1 : 0;
            cache[key] = result;
            return result;
        }

        throw new ApplicationException();
    }

    private static bool IsComboPossible(
            string statusLine, List<int> damageCounts, int damageIndex) {
        int totalDamage = damageCounts
            .GetRange(damageIndex, damageCounts.Count - damageIndex).Sum();
        int lengthNeededForDamage = totalDamage + damageCounts.Count() - 1 - damageIndex;

        if (statusLine.Length < lengthNeededForDamage) {
            return false;
        }

        if (statusLine.Where(c => c == '?' || c == '#').Count() < totalDamage) {
            return false;
        }

        if (statusLine.Where(c => c == '#').Count() > totalDamage) {
            return false;
        }

        return true;
    }

    public override string ToString() {
        return $"{statusLine} : {damageCounts.Join(",")}";
    }
}
