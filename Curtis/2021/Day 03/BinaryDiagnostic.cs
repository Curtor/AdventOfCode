namespace csteeves.Advent2021;

public class BinaryDiagnostic : DaySolution2021 {

    public override string Dir() {
        return "Day 03";
    }

    public override void Part1(List<string> input) {
        Part1(input[0].Length, ToBytes(input));
    }

    public void Part1(int lineLength, List<byte[]> values) {
        int arrayLength = values[0].Length * 8;
        int[] oneCount = new int[arrayLength];
        int[] zeroCount = new int[arrayLength];

        foreach (byte[] byteArray in values) {
            for (int i = 0; i < byteArray.Length; i++) {
                byte bytes = byteArray[i];
                for (int j = 0; j < 8; j++) {
                    int evaluated = bytes & 1 << j;

                    int index = (i * 8) + j;
                    if (evaluated > 0) {
                        oneCount[index]++;
                    } else {
                        zeroCount[index]++;
                    }
                }
            }
        }

        ulong gamma = 0;
        ulong epsilon = 0;

        for (int i = 0; i < lineLength; i++) {
            ulong mask = (ulong)1 << i;
            if (oneCount[i] > zeroCount[i]) {
                gamma |= mask;
            } else {
                epsilon |= mask;
            }
        }

        Console.WriteLine("Part 1");
        Console.WriteLine($"Gamma: {gamma}");
        Console.WriteLine($"Epsilon: {epsilon}");
        Console.WriteLine($"Consumption : {gamma * epsilon}");
    }

    public override void Part2(List<string> input) {
        Console.WriteLine("Part 2");
        Console.WriteLine("Answer: <Not Implemented>");
    }

    private List<byte[]> ToBytes(List<string> input) {
        List<byte[]> bytesList = [];
        foreach (string line in input) {
            bytesList.Add(LineParser.ToBytes(line));
        }

        return bytesList;
    }
}