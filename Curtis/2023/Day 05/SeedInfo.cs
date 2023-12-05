namespace csteeves.Advent2023;

public class SeedInfo {

    private readonly string mapStart;
    private readonly string mapEnd;
    private readonly RangeMapper rangeMapper;

    private List<long> jumps = [];
    private Dictionary<long, Tuple<long, long>> ranges = [];

    public SeedInfo NextSeedInfo { get; set; }

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
            Console.WriteLine(
                $"Mapped {mapStart} to {mapEnd} for {input} range {range}: {redirectRange}");

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




//    public long GetLowestFinalMapping(long input, long range) {
//        long result = long.MaxValue;
//        List<long> relevantJumps = GetJumps(input, range).ToList();

//        long processingStart = input;
//        long remainingRange = range;
//        for (int i = 0; i < relevantJumps.Count; i++) {
//            long relevantJump = relevantJumps[i];

//            long rangeStart;
//            long rangeLength;
//            if (relevantJump == -1) {
//                rangeStart = processingStart;
//                rangeLength = i == relevantJumps.Count - 1
//                    ? remainingRange
//                    : relevantJumps[i + 1] - rangeStart;
//            } else {
//                Tuple<long, long> currentRange = ranges[relevantJump];
//                rangeStart = currentRange.Item1 + processingStart - relevantJump;

//                long remainingInBucket = currentRange.Item2;
//                if (processingStart > relevantJump) {
//                    remainingInBucket = remainingInBucket - processingStart + relevantJump;
//                }
//                rangeLength = Math.Min(remainingInBucket, remainingRange);
//            }

//            long currentResult = NextSeedInfo == null
//                    ? rangeStart
//                    : NextSeedInfo.GetLowestFinalMapping(rangeStart, rangeLength);
//            result = Math.Min(result, currentResult);

//            processingStart += rangeLength;
//            remainingRange -= rangeLength;
//        }

//        Console.WriteLine($"Lowest {mapStart} to {mapEnd} for {input} range {range}: {result}");
//        return result;
//    }

//    private IEnumerable<long> GetJumps(long input, long range) {
//        if (input < jumps.First()) {
//            yield return -1;
//        }

//        for (int i = 0; i < jumps.Count; i++) {
//            long jump = jumps[i];
//            if (jump > input + range) {
//                yield break;
//            }

//            if (i == jumps.Count - 1) {
//                Tuple<long, long> jumpRange = ranges[jump];
//                if (input > jump + jumpRange.Item2) {
//                    yield return -1;
//                } else {
//                    yield return jump;
//                }
//                yield break;
//            }

//            if (jumps[i + 1] > input) {
//                yield return jump;
//            }
//        }
//    }
//}



//79(14) - 93
//    soil: 81(14) - 95
//    fert: 81(14) - 95
//    watr: 81(14) - 95
//    lght: 75(14) - 89
//    temp: 
//    humt: 
//    loct: 
