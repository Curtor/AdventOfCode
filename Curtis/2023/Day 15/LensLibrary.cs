namespace csteeves.Advent2023;

public class LensLibrary : DaySolution2023 {

    public override string Dir() {
        return "Day 15";
    }

    public override void Part1(List<string> input) {
        int hashSum = 0;
        foreach (string line in input) {
            List<string> tokens = LineParser.Tokens(line, ",");
            foreach (string token in tokens) {
                hashSum += GetHash(token);
            }
        }

        Console.WriteLine($"Hashs sum: {hashSum}");
    }

    public override void Part2(List<string> input) {
        int numBoxes = 256;
        LightBox[] lightBoxes = new LightBox[numBoxes];
        for (int i = 0; i < numBoxes; i++) {
            lightBoxes[i] = new LightBox();
        }

        foreach (string line in input) {
            List<string> tokens = LineParser.Tokens(line, ",");
            foreach (string token in tokens) {
                LightBoxStep step = new LightBoxStep(token);
                lightBoxes[step.boxIndex].Perform(step);
                // PrettyPrint(token, lightBoxes);
            }
        }

        int focusingPowerSum = 0;
        for (int i = 0; i < numBoxes; i++) {
            focusingPowerSum += lightBoxes[i].GetFocusingPower(i);
        }

        Console.WriteLine($"Focus power sum: {focusingPowerSum}");
    }

    private void PrettyPrint(string token, LightBox[] lightBoxes) {
        Console.WriteLine($"After \"{token}\":");
        for (int i = 0; i < lightBoxes.Length; i++) {
            LightBox lightBox = lightBoxes[i];
            if (!lightBox.AnyLenses()) {
                continue;
            }
            Console.WriteLine($"Box {i}: {lightBox}");
        }
        Console.WriteLine();
    }

    public static int GetHash(string token) {
        int result = 0;
        foreach (char c in token) {
            int cVal = c;
            result += cVal;
            result *= 17;
            result = result % 256;
        }
        return result;
    }
}