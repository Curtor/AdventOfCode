
namespace csteeves.Advent2023;

public class BoatRace {

    private Dictionary<long, long> timeCache = [];

    public readonly long time;
    public readonly long distanceRecord;

    private long shortestHeldTime;
    private long longestHeldTime;

    public BoatRace(long time, long distance) {
        this.time = time;
        this.distanceRecord = distance;

        shortestHeldTime = long.MaxValue;
        longestHeldTime = 0;
    }

    public long GetRaceWinOptionCount() {
        long midPoint = time / 2;

        SearchWorstDistanceHoldTime(midPoint, 0, time, true);
        SearchWorstDistanceHoldTime(midPoint, 0, time, false);

        // Console.WriteLine($"Shortest for BoatRace {this}: {shortestHeldTime}");
        // Console.WriteLine($"Longest for BoatRace {this}: {longestHeldTime}");

        return longestHeldTime - shortestHeldTime + 1;
    }
    public void SearchWorstDistanceHoldTime(long timeHeld, long start, long end, bool shortest) {
        long currentDistance = GetDistance(timeHeld);

        if (currentDistance > distanceRecord) {
            if (shortest) {
                shortestHeldTime = Math.Min(shortestHeldTime, timeHeld);
            } else {
                longestHeldTime = Math.Max(longestHeldTime, timeHeld);
            }
        }

        if (start == end) {
            return;
        }

        long newStart;
        long newEnd;
        long midPoint;

        if (currentDistance > distanceRecord) {
            newStart = shortest ? start : timeHeld + 1;
            newEnd = shortest ? timeHeld - 1 : end;
            midPoint = (newStart + newEnd) / 2;
        } else {
            newStart = shortest ? timeHeld + 1 : start;
            newEnd = shortest ? end : timeHeld - 1;
            midPoint = (newStart + newEnd) / 2;
        }

        if (newStart > newEnd) {
            return;
        }

        SearchWorstDistanceHoldTime(midPoint, newStart, newEnd, shortest);
    }


    private long GetDistance(long timeHeld) {
        if (timeHeld <= 0 || timeHeld >= time) {
            return 0;
        }

        if (timeCache.TryGetValue(timeHeld, out long newDistance)) {
            return newDistance;
        }

        long speed = timeHeld;
        long timeTravelling = time - timeHeld;
        newDistance = timeTravelling * speed;

        timeCache[timeHeld] = newDistance;
        return newDistance;
    }

    public override string ToString() {
        return $"[{distanceRecord} far in {time} time]";
    }
}
