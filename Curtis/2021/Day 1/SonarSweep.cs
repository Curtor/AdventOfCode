namespace csteeves.Advent2021;

public class SonarSweep : DaySolution2021 {

    private const string dir = "Day 1";

    public override string Dir() {
        return dir;
    }

    public override void Part1(List<string> input) {
        int increaseCount = 0;
        int lastReading = int.Parse(input[0]);

        for (int i = 1; i < input.Count; i++) {
            int currentReading = int.Parse(input[i]);
            if (currentReading > lastReading) {
                increaseCount++;
            }
            lastReading = currentReading;
        }

        Console.WriteLine($"Increases: {increaseCount}");
    }


    public override void Part2(List<string> input) {
        CircularQueue<int> sweeps = new CircularQueue<int>(3);
        sweeps.Enqueue(int.Parse(input[0]));
        sweeps.Enqueue(int.Parse(input[1]));
        sweeps.Enqueue(int.Parse(input[2]));

        int increaseCount = 0;
        int lastSweep = GetSweepSum(sweeps);

        for (int i = 3; i < input.Count; i++) {
            int currentReading = int.Parse(input[i]);
            sweeps.Enqueue(currentReading, true);
            int thisSweep = GetSweepSum(sweeps);

            if (thisSweep > lastSweep) {
                increaseCount++;
            }

            lastSweep = thisSweep;
        }

        Console.WriteLine($"Sweep increases: {increaseCount}");
    }

    private int GetSweepSum(CircularQueue<int> sweeps) {
        int total = 0;
        foreach (int value in sweeps) {
            total += value;
        }
        return total;
    }
}