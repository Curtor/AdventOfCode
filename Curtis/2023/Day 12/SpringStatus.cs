

namespace csteeves.Advent2023;

public class SpringStatus {

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

    public int GetComboCount() {
        int totalDamage = damageCounts.Sum();
        int lengthNeededForDamage = totalDamage + damageCounts.Count() - 1;
        return GetComboCount(statusLine, damageCounts, totalDamage, lengthNeededForDamage);
    }

    private static int GetComboCount(
        string statusLine, List<int> damageCounts, int totalDamage, int lengthNeededForDamage) {
        if (!IsComboPossible(statusLine, damageCounts, totalDamage, lengthNeededForDamage)) {
            return 0;
        }

        bool seenDamageLast = false;
        List<int> recordedDamage = [];

        for (int i = 0; i < statusLine.Length; i++) {
            char c = statusLine[i];
            if (c == '.') {
                if (recordedDamage.Any()
                    && recordedDamage.Last() != damageCounts[recordedDamage.Count - 1]) {
                    return 0;
                }

                seenDamageLast = false;
                continue;
            }

            if (c == '#') {
                if (!seenDamageLast) {
                    recordedDamage.Add(1);
                } else {
                    recordedDamage[recordedDamage.Count - 1]++;
                }

                if (recordedDamage.Count > damageCounts.Count
                    || recordedDamage.Last() > damageCounts[recordedDamage.Count - 1]) {
                    return 0;
                }

                seenDamageLast = true;
                continue;
            }

            string assumeDamageLine = GetNewStatusLine(statusLine, i, '#');
            string assumeOperatingLine = GetNewStatusLine(statusLine, i, '.');

            if (!seenDamageLast) {
                return GetComboCount(
                    assumeDamageLine, damageCounts, totalDamage, lengthNeededForDamage)
                    + GetComboCount(
                        assumeOperatingLine, damageCounts, totalDamage, lengthNeededForDamage);
            }

            int runningDamage = recordedDamage.Last();
            int expectedDamage = damageCounts[recordedDamage.Count - 1];
            if (runningDamage > expectedDamage) {
                return 0;
            } else if (runningDamage == expectedDamage) {
                return GetComboCount(
                    assumeOperatingLine, damageCounts, totalDamage, lengthNeededForDamage);
            } else {
                return GetComboCount(
                    assumeDamageLine, damageCounts, totalDamage, lengthNeededForDamage);
            }

        }

        if (recordedDamage.Count != damageCounts.Count) {
            return 0;
        }

        for (int i = 0; i < recordedDamage.Count; i++) {
            if (recordedDamage[i] != damageCounts[i]) {
                return 0;
            }
        }

        return 1;
    }

    private static bool IsComboPossible(
            string statusLine,
            List<int> damageCounts,
            int totalDamage,
            int lengthNeededForDamage) {

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

    private static string GetNewStatusLine(string statusLine, int index, char newValue) {
        char[] chars = statusLine.ToCharArray();
        chars[index] = newValue;
        return new string(chars);
    }

    public override string ToString() {
        return $"{statusLine} : {damageCounts.Join(",")}";
    }
}
