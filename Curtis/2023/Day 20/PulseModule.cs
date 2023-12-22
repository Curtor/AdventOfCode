using System.Data;
using System.Reflection;

namespace csteeves.Advent2023;

public abstract class PulseModule {

    public readonly string name;

    private long highPulsesReceived = 0;
    private long lowPulsesReceived = 0;

    private List<string> downstreamModuleNames;
    public IEnumerable<string> DownstreamModuleNames => downstreamModuleNames;

    protected List<PulseModule> upstreamModules = [];
    protected List<PulseModule> downstreamModules = [];

    protected PulseModule(string name, List<string> downstreamModuleNames) {
        this.name = name;
        this.downstreamModuleNames = downstreamModuleNames;
    }

    public static PulseModule Create(string line) {
        List<string> tokens = LineParser.Tokens(line, " -> ");
        List<string> downstreamModuleNames = 
            LineParser.Tokens(tokens[1], ",").Select(s => s.Trim()).ToList();

        if (tokens[0].Equals("broadcaster")) {
            return new BroadcasterModule("broadcaster", downstreamModuleNames);
        }

        string moduleName = tokens[0].Substring(1);
        switch (line[0]) {
            case '%':
                return new FlipFlopModule(moduleName, downstreamModuleNames);
            case '&':
                return new ConjunctionModule(moduleName, downstreamModuleNames);
        }

        throw new ArgumentOutOfRangeException();
    }

    public virtual void AddUpstreamModule(PulseModule sender) {
        upstreamModules.Add(sender);
    }

    public void AddDownstreamModule(PulseModule reciever) {
        downstreamModules.Add(reciever);
    }

    protected void RecordPulse(bool high) {
        if (high) {
            highPulsesReceived++;
        } else {
            lowPulsesReceived++;
        }
    }

    public void Reset() {
        highPulsesReceived = 0;
        lowPulsesReceived = 0;
    }

    public bool Ready() {
        return highPulsesReceived == 0 && lowPulsesReceived == 1;
    }

    public abstract IEnumerable<PendingPulse> HandlePulse(PendingPulse pulse);

    public override string ToString() {
        return name;
    }
}
