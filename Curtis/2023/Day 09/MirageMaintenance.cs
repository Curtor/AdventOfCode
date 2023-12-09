namespace csteeves.Advent2023;

public class MirageMaintenance : DaySolution2023 {

    public override string Dir() {
        return "Day 09";
    }

    public override void Part1(List<string> input) {
        List<SensorReading> readings = GetReadings(input);

        int sum = 0;
        foreach (SensorReading reading in readings) {
            sum += reading.NextExtrapolatedValue();
        }

        Console.WriteLine($"Sum of end extrapolations: {sum}");
    }

    public override void Part2(List<string> input) {
        List<SensorReading> readings = GetReadings(input);

        int sum = 0;
        foreach (SensorReading reading in readings) {
            sum += reading.PreviousExtrapolatedValue();
        }

        Console.WriteLine($"Sum of start extrapolations: {sum}");
    }

    private static List<SensorReading> GetReadings(List<string> input) {
        List<SensorReading> readings = [];
        foreach (string line in input) {
            readings.Add(new SensorReading(line));
        }

        return readings;
    }
}