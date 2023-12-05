namespace csteeves.Advent2023;

public class IfYouGiveASeedAFertilizer : DaySolution2023 {

    public override string Dir() {
        return "Day 05";
    }

    public override void Part1(List<string> input) {
        List<string> seedsString = LineParser.Tokens(input[0]);
        seedsString.RemoveAt(0);
        List<long> seeds = seedsString.Select(long.Parse).ToList();

        List<SeedInfo> seedInfoList = GetSeedInfoList(input);

        long result = long.MaxValue;
        foreach (long seed in seeds) {
            long mapping = seed;
            foreach (SeedInfo seedInfo in seedInfoList) {
                mapping = seedInfo.GetRedirect(mapping);
                // Console.WriteLine($"{seedInfo} for {seed}: {mapping}");
            }

            result = Math.Min(result, mapping);
            Console.WriteLine($"Final mapping for {seed}: {mapping}");
        }

        Console.WriteLine($"Lowest location: {result}");
    }

    public override void Part2(List<string> input) {
        List<string> seedsString = LineParser.Tokens(input[0]);
        seedsString.RemoveAt(0);

        List<Tuple<long, long>> seeds = [];
        for (int i = 0; i < seedsString.Count; i += 2) {
            seeds.Add(Tuple.Create(long.Parse(seedsString[i]), long.Parse(seedsString[i + 1])));
        }

        List<SeedInfo> seedInfoList = GetSeedInfoList(input);
        for (int i = 0; i < seedInfoList.Count - 1; i++) {
            seedInfoList[i].NextSeedInfo = seedInfoList[i + 1];
        }

        long result = long.MaxValue;
        foreach (Tuple<long, long> seedRange in seeds) {
            long mapping =
                seedInfoList[0].GetLowestRedirect(seedRange.Item1, seedRange.Item2);
            result = Math.Min(result, mapping);
            Console.WriteLine($"---");
        }

        Console.WriteLine($"Lowest location from ranges: {result}");
    }

    private static List<SeedInfo> GetSeedInfoList(List<string> input) {
        List<SeedInfo> result = [];
        for (int i = 2; i < input.Count; i++) {
            List<string> entry = [input[i]];
            for (i++; i < input.Count && !string.IsNullOrEmpty(input[i]); i++) {
                entry.Add(input[i]);
            }
            result.Add(new SeedInfo(entry));
        }
        return result;
    }
}