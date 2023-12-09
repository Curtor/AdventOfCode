
namespace csteeves.Advent2023;

public class SensorReading {

    private readonly List<int> readings;

    public SensorReading(string line)
        : this(LineParser.Tokens(line).Select(int.Parse).ToList()) { }

    public SensorReading(List<int> readings) {
        this.readings = readings;
    }

    public int NextExtrapolatedValue() {
        List<int> diffs = GetDiffs();

        if (!diffs.Any(d => d != 0)) {
            return readings[0];
        }

        SensorReading reading = new SensorReading(diffs);
        int lastDiff = reading.NextExtrapolatedValue();
        return readings.Last() + lastDiff;
    }

    public int PreviousExtrapolatedValue() {
        List<int> diffs = GetDiffs();

        if (!diffs.Any(d => d != 0)) {
            return readings[0];
        }

        SensorReading reading = new SensorReading(diffs);
        int firstDiff = reading.PreviousExtrapolatedValue();
        return readings.First() - firstDiff;
    }

    private List<int> GetDiffs() {
        List<int> diffs = [];
        for (int i = 0; i < readings.Count - 1; i++) {
            int diff = readings[i + 1] - readings[i];
            diffs.Add(diff);
        }
        return diffs;
    }

    public override string ToString() {
        return readings.Join(",");
    }
}