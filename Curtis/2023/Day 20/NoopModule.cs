namespace csteeves.Advent2023;

public class NoopModule : PulseModule {

    public NoopModule(string name, List<string> downstreamModuleNames)
        : base(name, downstreamModuleNames) {
    }

    public override IEnumerable<PendingPulse> HandlePulse(PendingPulse pulse) {
        RecordPulse(pulse.high);
        yield break;
    }
}