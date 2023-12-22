namespace csteeves.Advent2023;

public class BroadcasterModule : PulseModule {

    public BroadcasterModule(string name, List<string> downstreamModuleNames) 
        : base(name, downstreamModuleNames) {
    }

    public override IEnumerable<PendingPulse> HandlePulse(PendingPulse pulse) {
        RecordPulse(pulse.high);
        foreach (PulseModule receiver in downstreamModules) {
            yield return new PendingPulse(this, receiver, pulse.high);
        }
    }
}