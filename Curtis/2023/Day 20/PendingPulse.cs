namespace csteeves.Advent2023;

public class PendingPulse {

    public readonly PulseModule sender;
    public readonly PulseModule receiver;
    public readonly bool high;

    public PendingPulse(PulseModule sender, PulseModule receiver, bool high) {
        this.sender = sender;
        this.receiver = receiver;
        this.high = high;
    }

    public override string ToString() {
        string pulseString = high ? "High" : "Low";
        return $"{sender?.name} ({pulseString}) -> {receiver.name}";
    }

}
