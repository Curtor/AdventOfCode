namespace csteeves.Advent2022;

public class DeviceDirectory : IComparable<DeviceDirectory> {

    public string Name;
    public DeviceDirectory? Parent = null;
    public List<DeviceDirectory> Subs = [];
    public List<string> Files = [];
    public int LocalSize = 0;

    public DeviceDirectory(string name) {
        this.Name = name;
    }

    public DeviceDirectory(string name, DeviceDirectory parent) {
        this.Name = name;
        this.Parent = parent;
    }

    public DeviceDirectory? GetSub(string subName) {
        return Subs.FirstOrDefault(s => s.Name == subName, null);
    }

    public void PrintSizes() {
        int size = GetSize();
        Console.WriteLine(Name + ": " + size);
        Subs.ForEach(s => s.PrintSizes());
    }

    public int GetSize() {
        int total = 0;
        Subs.ForEach(s => total += s.GetSize());
        total += LocalSize;
        return total;
    }

    public int SumSubsUnder(int cap) {
        int total = 0;
        int localSize = GetSize();
        if (localSize <= cap) {
            total += localSize;
        }
        Subs.ForEach(s => total += s.SumSubsUnder(cap));
        return total;
    }

    public List<DeviceDirectory> GetAllDir() {
        List<DeviceDirectory> result = [this];
        foreach (DeviceDirectory sub in Subs) {
            result.AddRange(sub.GetAllDir());
        }
        return result;
    }

    public override int GetHashCode() {
        return Name.GetHashCode();
    }

    public override bool Equals(object? obj) {
        return obj is DeviceDirectory other && Name.Equals(other.Name);
    }

    public override string ToString() {
        return "Directory: " + Name.ToString();
    }

    public int CompareTo(DeviceDirectory? other) {
        return GetSize().CompareTo(other.GetSize());
    }
}