namespace csteeves.Advent2023;

public class SeedInfo {

    private readonly string mapStart;
    private readonly string mapEnd;
    private readonly RangeMapper rangeMapper;

    private List<long> jumps = [];
    private Dictionary<long, Tuple<long, long>> ranges = [];

    public SeedInfo? NextSeedInfo { get; set; }

    public SeedInfo(List<string> entry) {
        List<string> mappingTitleTokens = LineParser.Tokens(entry[0]);
        List<string> mappingHyphenedTokens = LineParser.Tokens(mappingTitleTokens[0], "-");
        mapStart = mappingHyphenedTokens[0];
        mapEnd = mappingHyphenedTokens[2];

        rangeMapper = new RangeMapper();
        for (int i = 1; i < entry.Count; i++) {
            rangeMapper.AddMapping(entry[i]);
        }

        jumps.Sort();
    }

    public long GetRedirect(long input) {
        return rangeMapper.GetRedirect(input);
    }

    public long GetLowestRedirect(long input, long range) {
        long result = long.MaxValue;
        foreach (Range redirectRange in rangeMapper.GetRedirects(input, range)) {
            // Console.WriteLine(
            //     $"Mapped {mapStart} to {mapEnd} for {input} range {range}: {redirectRange}");

            long rangeResult = NextSeedInfo == null ? redirectRange.start : NextSeedInfo.GetLowestRedirect(redirectRange.start, redirectRange.length);
            result = Math.Min(result, rangeResult);
        }

        Console.WriteLine($"Lowest {mapStart} to {mapEnd} for {input} range {range}: {result}");
        return result;
    }

    public override string ToString() {
        return $"Seed Info: {mapStart} to {mapEnd}";
    }
}