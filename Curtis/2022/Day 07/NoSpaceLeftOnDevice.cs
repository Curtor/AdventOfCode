namespace csteeves.Advent2022;

public class NoSpaceLeftOnDevice : DaySolution2022 {

    public override string Dir() {
        return "Day 07";
    }

    public override void Part1(List<string> input) {
        DeviceDirectory root = BuildRootDirectory(input);

        root.PrintSizes();
        int sumUnderCap = root.SumSubsUnder(100000);
        Console.WriteLine("Sum of directories under cap: " + sumUnderCap);
    }

    public override void Part2(List<string> input) {
        DeviceDirectory root = BuildRootDirectory(input);

        int filesystem = 70000000;
        int desired = 30000000;
        int freeSpace = filesystem - root.GetSize();
        int needed = desired - freeSpace;

        List<DeviceDirectory> dirs = root.GetAllDir();
        dirs.Sort();
        DeviceDirectory toDelete = dirs.First(d => d.GetSize() >= needed);
        Console.WriteLine("Size of directories to delete: " + toDelete.GetSize());
    }

    private static DeviceDirectory BuildRootDirectory(List<string> input) {
        string pwd = "";
        DeviceDirectory root = new DeviceDirectory(pwd);
        DeviceDirectory current = root;

        for (int li = 0; li < input.Count(); ++li) {
            string line = input.ElementAt(li);
            string[] token = line.Split(' ');

            if (token[0].Equals("$")) {
                if (!token[1].Equals("cd")) {
                    continue;
                }

                string dirCommand = token[2];
                if (dirCommand.Equals("/")) {
                    pwd = "";
                    current = root;
                } else if (dirCommand.Equals("..")) {
                    int delimitIndex = pwd.LastIndexOf("/");
                    pwd = pwd.Substring(0, delimitIndex);
                    current = current.Parent;
                } else {
                    pwd += "/" + dirCommand;
                    DeviceDirectory sub = current.GetSub(dirCommand);

                    if (sub == null) {
                        sub = new DeviceDirectory(dirCommand, current);
                        current.Subs.Add(sub);
                    }

                    current = sub;
                }

                continue;
            }

            try {
                int size = int.Parse(token[0]);
                string file = token[1];

                if (!current.Files.Contains(file)) {
                    current.Files.Add(file);
                    current.LocalSize += size;
                }
            } catch (Exception) {
                string subName = token[1];
                DeviceDirectory sub = current.GetSub(subName);

                if (sub == null) {
                    sub = new DeviceDirectory(subName, current);
                    current.Subs.Add(sub);
                }
            }
        }

        return root;
    }
}