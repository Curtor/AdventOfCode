namespace csteeves.Advent2023;

public class ConjunctionModule : PulseModule {

    private Dictionary<PulseModule, bool> lastReceivedHighPulse = [];

    public ConjunctionModule(string name, List<string> downstreamModuleNames)
        : base(name, downstreamModuleNames) {
    }

    public override void AddUpstreamModule(PulseModule sender) {
        base.AddUpstreamModule(sender);
        lastReceivedHighPulse[sender] = false;
    }

    public override IEnumerable<PendingPulse> HandlePulse(PendingPulse pulse) {
        RecordPulse(pulse.high);
        lastReceivedHighPulse[pulse.sender] = pulse.high;

        bool allHigh = pulse.high;
        if (pulse.high) {
            foreach (bool lastPulse in lastReceivedHighPulse.Values) {
                allHigh &= lastPulse;
            }
        }

        foreach (PulseModule receiver in downstreamModules) {
            yield return new PendingPulse(this, receiver, !allHigh);
        }
    }
}