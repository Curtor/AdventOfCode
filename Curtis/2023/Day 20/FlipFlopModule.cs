namespace csteeves.Advent2023;

public class FlipFlopModule : PulseModule {

    private bool active = false;

    public FlipFlopModule(string name, List<string> downstreamModuleNames)
        : base(name, downstreamModuleNames) {
    }

    public override IEnumerable<PendingPulse> HandlePulse(PendingPulse pulse) {
        RecordPulse(pulse.high);
        if (pulse.high) {
            yield  break;
        }

        active = !active;

        foreach (PulseModule receiver in downstreamModules) {
            yield return new PendingPulse(this, receiver, active);
        }
    }
}
