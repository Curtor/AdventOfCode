namespace csteeves.Advent2022;

public class TuningTrouble : DaySolution2022 {

    public override string Dir() {
        return "Day 06";
    }

    public override void Part1(List<string> input) {
        foreach (string line in input) {
            int answer = FindEndIndexOfUniqueSequence(line, 4);
            Console.WriteLine($"Packet start: {answer}");
        }
    }

    public override void Part2(List<string> input) {
        foreach (string line in input) {
            int answer = FindEndIndexOfUniqueSequence(line, 14);
            Console.WriteLine($"Message start: {answer}");
        }
    }

    private static int FindEndIndexOfUniqueSequence(string line, int sequenceLength) {
        List<char> buffer = [];
        for (int ch = 0; ch < line.Length; ++ch) {
            char c = line.ElementAt(ch);
            buffer.Add(c);
            if (ch >= sequenceLength) {
                buffer.RemoveAt(0);
            } else {
                continue;
            }

            HashSet<char> unique = new(buffer);
            if (unique.Count == sequenceLength) {
                return ch + 1;
            }
        }
        return -1;
    }
}

