namespace csteeves.Advent2023;

public class LavaductLagoon : DaySolution2023 {

    public override string Dir() {
        return "Day 18";
    }

    public override void Part1(List<string> input) {
        List<DigInstruction> digInstructions =
            GetDigInstructions(input, s => new DigInstruction(s));
        DigSite digSite = new DigSite(digInstructions);
        long interiorSize = digSite.GetInteriorSize();
        Console.WriteLine($"Dig site size: {interiorSize}");

        DigSiteV2 digSite2 = new DigSiteV2(digInstructions);
        interiorSize = digSite2.GetInteriorSize();
        Console.WriteLine($"Dig site V2 size: {interiorSize}");
    }

    public override void Part2(List<string> input) {
        List<DigInstruction> digInstructions =
            GetDigInstructions(input, s => DigInstruction.ParseHex(s));
        DigSiteV2 digSite = new DigSiteV2(digInstructions);
        long interiorSize = digSite.GetInteriorSize();
        Console.WriteLine($"Dig site size: {interiorSize}");
    }

    private List<DigInstruction> GetDigInstructions(
            List<string> input, Func<string, DigInstruction> parser) {
        List<DigInstruction> instructions = [];
        Vector2Int position = new Vector2Int(0, 0);
        foreach (string line in input) {
            DigInstruction currentInstruction = parser(line);
            instructions.Add(currentInstruction);
            position = currentInstruction.GetNextPosition(position);
        }
        return instructions;
    }
}