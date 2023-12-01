namespace csteeves.Advent2022;

public class RucksackReorganization : DaySolution2022 {

    public override string Dir() {
        return "Day 03";
    }

    public override void Part1(List<string> input) {
        int sum = 0;

        foreach (string line in input) {
            HashSet<char> firstHalf = [];

            for (int i = 0; i < line.Length; ++i) {
                char c = line[i];
                if (i < line.Length / 2) {
                    firstHalf.Add(c);
                } else if (firstHalf.Contains(c)) {
                    sum += Value(c);
                    break;
                }
            }
        }

        Console.WriteLine("Part 1");
        Console.WriteLine($"Duplicates sum: {sum}");
    }

    public override void Part2(List<string> input) {
        int sum = 0;

        HashSet<char> firstChars = [];
        HashSet<char> secondChars = [];

        for (int lineIndex = 0; lineIndex < input.Count; lineIndex++) {
            string line = input[lineIndex];

            if (lineIndex % 3 == 0) {
                firstChars.Clear();
                secondChars.Clear();
            }

            for (int charIndex = 0; charIndex < line.Length; charIndex++) {
                char c = line[charIndex];

                int remainder = lineIndex % 3;
                if (remainder == 0) {
                    firstChars.Add(c);
                } else if (remainder == 1) {
                    secondChars.Add(c);
                } else if (firstChars.Contains(c) && secondChars.Contains(c)) {
                    sum += Value(c);
                    break;
                }
            }
        }

        Console.WriteLine("Part 2");
        Console.WriteLine($"Badges sum: {sum}");
    }

    private static int Value(char c) {
        if (char.IsLower(c)) {
            return c - 'a' + 1;
        }
        return c - 'A' + 27;
    }
}