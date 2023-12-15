
namespace csteeves.Advent2023;

public class LightBox {

    private List<Lens> lenses = [];
    private Dictionary<string, int> lensIndexes = [];

    public void Perform(LightBoxStep step) {
        switch (step.step) {
            case LightBoxStep.Step.SET:
                SetLens(step);
                break;
            case LightBoxStep.Step.REMOVE:
                RemoveLens(step);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void SetLens(LightBoxStep step) {
        if (lensIndexes.TryGetValue(step.name, out int index)) {
            lenses[index].focalLength = step.focalLength;
        } else {
            Lens lens = new Lens(step.name, step.focalLength);
            lenses.Add(lens);
            lensIndexes[step.name] = lenses.Count - 1;
        }
    }

    private void RemoveLens(LightBoxStep step) {
        if (!lensIndexes.TryGetValue(step.name, out int index)) {
            return;
        }

        lenses.RemoveAt(index);
        lensIndexes.Remove(step.name);

        for (int i = index; i < lenses.Count; i++) {
            lensIndexes[lenses[i].name] = i;
        }
    }

    public bool AnyLenses() {
        return lenses.Any();
    }

    public int GetFocusingPower(int boxNumber) {
        int result = 0;
        for (int i = 0; i < lenses.Count; i++) {
            Lens lens = lenses[i];
            int power = (1 + boxNumber) * (1 + i) * lens.focalLength;
            result += power;
        }
        return result;
    }

    public override string ToString() {
        return lenses.Join(" ");
    }
}