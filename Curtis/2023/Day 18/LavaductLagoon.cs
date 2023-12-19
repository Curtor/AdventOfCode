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
        Console.WriteLine($"Hole size: {interiorSize}");
    }

    public override void Part2(List<string> input) {
        List<DigInstruction> digInstructions =
            GetDigInstructions(input, s => DigInstruction.ParseHex(s));
        DigSite digSite = new DigSite(digInstructions);
        long interiorSize = digSite.GetInteriorSize();
        Console.WriteLine($"Hole size: {interiorSize}");
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