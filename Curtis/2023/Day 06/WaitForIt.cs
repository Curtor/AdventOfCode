namespace csteeves.Advent2023;

public class WaitForIt : DaySolution2023 {

    public override string Dir() {
        return "Day 06";
    }

    public override void Part1(List<string> input) {
        List<string> timeTokens = LineParser.Tokens(input[0]);
        List<string> distanceTokens = LineParser.Tokens(input[1]);

        List<long> times =
            timeTokens.GetRange(1, timeTokens.Count - 1).Select(long.Parse).ToList();
        List<long> distances =
            distanceTokens.GetRange(1, distanceTokens.Count - 1).Select(long.Parse).ToList();

        List<BoatRace> boatRaces = GetBoatRaces(times, distances);
        GetRaceScore(boatRaces);
    }

    public override void Part2(List<string> input) {
        List<string> timeTokens = LineParser.Tokens(input[0]);
        List<string> distanceTokens = LineParser.Tokens(input[1]);

        List<long> times =
            [long.Parse(timeTokens.GetRange(1, timeTokens.Count - 1).Join(""))];
        List<long> distances =
            [long.Parse(distanceTokens.GetRange(1, distanceTokens.Count - 1).Join(""))];

        List<BoatRace> boatRaces = GetBoatRaces(times, distances);
        GetRaceScore(boatRaces);
    }

    private List<BoatRace> GetBoatRaces(List<long> times, List<long> distances) {
        List<BoatRace> boatRaces = [];
        for (int i = 0; i < times.Count; i++) {
            BoatRace BoatRace = new BoatRace(times[i], distances[i]);
            boatRaces.Add(BoatRace);
        }
        return boatRaces;
    }

    private static void GetRaceScore(List<BoatRace> boatRaces) {
        long result = 1;
        foreach (BoatRace boatRace in boatRaces) {
            long combos = boatRace.GetRaceWinOptionCount();
            Console.WriteLine($"Options for BoatRace {boatRace}: {combos}");

            result *= combos;
        }

        Console.WriteLine($"Options product: {result}");
    }
}