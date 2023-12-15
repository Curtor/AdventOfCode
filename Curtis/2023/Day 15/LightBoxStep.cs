namespace csteeves.Advent2023;

public class LightBoxStep {

    public enum Step { SET, REMOVE }

    public readonly int boxIndex;
    public readonly Step step;
    public readonly string name;
    public readonly int focalLength;

    public LightBoxStep(string token) {
        if (token.Last() == '-') {
            step = Step.REMOVE;
            name = token.Substring(0, token.Length - 1);
            focalLength = -1;
        } else {
            step = Step.SET;

            int equalsIndex = token.IndexOf('=');
            name = token.Substring(0, equalsIndex);
            focalLength =
                int.Parse(token.Substring(equalsIndex + 1, token.Length - equalsIndex - 1));
        }

        boxIndex = LensLibrary.GetHash(name);
    }

    public override string ToString() {
        return $"{step} {name} of {focalLength} in {boxIndex}";
    }
}