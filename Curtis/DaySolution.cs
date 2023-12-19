
namespace csteeves;

public abstract class DaySolution {

    private const bool RUN_PART_1 = true;
    private const bool RUN_PART_2 = true;

    public void RunDebug() {
        Run(Runner.DEBUG_INPUT, Runner.DEBUG_ALT_INPUT);
    }

    public void Run() {
        Run(Runner.REAL_INPUT, Runner.REAL_ALT_INPUT);
    }

    public abstract string GetYear();

    public abstract string Dir();

    private void Run(string filename, string partTwoFilename) {
        List<string>? input = ReadLines(filename);
        List<string>? partTwoInput = ReadLines(partTwoFilename);

        if (input == null) {
            throw new FileNotFoundException(filename);
        }

        if (RUN_PART_1) {
            Console.WriteLine("Part 1");
            Part1(input);
        }

        if (RUN_PART_2) {
            Console.WriteLine();
            Console.WriteLine("Part 2");
            Part2(partTwoInput ?? input);
        }
    }

    private List<string>? ReadLines(string filename) {
        string fullFilename = GetFullFilename(filename);
        if (!File.Exists(fullFilename)) {
            return null;
        }
        return File.ReadLines(fullFilename).ToList();
    }

    private string GetFullFilename(string filename) {
        return Path.Combine(Runner.PATH, GetYear(), Dir(), "Input", filename);
    }

    public abstract void Part1(List<string> input);

    public abstract void Part2(List<string> input);
}
