namespace csteeves.Advent2023;

public class PulsePropagation : DaySolution2023 {

    public override string Dir() {
        return "Day 20";
    }

    public override void Part1(List<string> input) {
        Dictionary<string, PulseModule> modules = GetModules(input);
        PulseModule broadcastModule = modules["broadcaster"];

        long highPulsesSent = 0;
        long lowPulsesSent = 0;

         for (int i = 0; i < 1000; i++) {
            Queue<PendingPulse> pulses = new Queue<PendingPulse>();
            pulses.Enqueue(new PendingPulse(null, broadcastModule, false));

            while (pulses.Any()) {
                PendingPulse currentPulse = pulses.Dequeue();
                PulseModule receiver = currentPulse.receiver;

                if (currentPulse.high) {
                    highPulsesSent++;
                } else {
                    lowPulsesSent++;
                }

                // Console.WriteLine(currentPulse);
                foreach (PendingPulse nextPulse in receiver.HandlePulse(currentPulse)) {
                    pulses.Enqueue(nextPulse);
                }
            }
        }

        long product = highPulsesSent * lowPulsesSent;
        Console.WriteLine($"High pulses: {highPulsesSent}");
        Console.WriteLine($"Low pulses: {lowPulsesSent}");
        Console.WriteLine($"Product of sent pulses: {product}");
    }

    public override void Part2(List<string> input) {
        Dictionary<string, PulseModule> modules = GetModules(input);
        if (modules.ContainsKey("output")) {
            Console.WriteLine("Test case not provided");
            return;
        }

        PulseModule broadcastModule = modules["broadcaster"];
        PulseModule sandModule = modules["rx"];

        int buttonPressed = 0;
        while (true) {
            sandModule.Reset();
            buttonPressed++;

            Queue<PendingPulse> pulses = new Queue<PendingPulse>();
            pulses.Enqueue(new PendingPulse(null, broadcastModule, false));

            while (pulses.Any()) {
                PendingPulse currentPulse = pulses.Dequeue();
                PulseModule receiver = currentPulse.receiver;

                // Console.WriteLine(currentPulse);
                foreach (PendingPulse nextPulse in receiver.HandlePulse(currentPulse)) {
                    pulses.Enqueue(nextPulse);
                }
            }

            if (sandModule.Ready()) {
                break;
            }
        }

        Console.WriteLine($"Presses to prepare sand: {buttonPressed}");
    }

    private Dictionary<string, PulseModule> GetModules(List<string> input) {
        Dictionary<string, PulseModule> modules = [];
        foreach (string line in input) {
            PulseModule module = PulseModule.Create(line);
            modules[module.name] = module;
        }

        List<PulseModule> noopModules = [];
        foreach (PulseModule module in modules.Values) {
            foreach (string downstreamModuleName in module.DownstreamModuleNames) {
                if (!modules.TryGetValue(downstreamModuleName, out PulseModule? downstreamModule)) {
                    downstreamModule = new NoopModule(downstreamModuleName, new List<string>());
                    noopModules.Add(downstreamModule);
                }

                module.AddDownstreamModule(downstreamModule);
                downstreamModule.AddUpstreamModule(module);
            }
        }

        foreach (PulseModule noopModule in noopModules) {
            modules[noopModule.name] = noopModule;
        }

        return modules;
    }
}