namespace csteeves;

public abstract class DaySolution {

    public void RunDebug() {
        Run(File.ReadLines(GetFullFilename(Runner.DEBUG_INPUT)).ToList());
    }

    public void Run() {
        Run(File.ReadLines(GetFullFilename(Runner.REAL_INPUT)).ToList());
    }

    private string GetFullFilename(string filename) {
        return Path.Combine(Runner.PATH, GetYear(), Dir(), "Input", filename);
    }

    public abstract string GetYear();

    public abstract string Dir();

    public abstract void Run(List<string> input);
}
