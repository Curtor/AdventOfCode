

namespace csteeves.Advent2023;

public class CubeGame {

    public int gameNumber;
    public List<CubeGameSet> cubeGameSets = [];

    public CubeGame(int gameNumber, string setsString) {
        this.gameNumber = gameNumber;

        List<string> sets = LineParser.Tokens(setsString, ";");
        foreach (string s in sets) {
            cubeGameSets.Add(new CubeGameSet(s));
        }
    }

    public bool IsPossible(int red, int green, int blue) {
        foreach (CubeGameSet cubeGameSet in cubeGameSets) {
            if (!cubeGameSet.IsPossible(red, green, blue)) {
                return false;
            }
        }
        return true;
    }

    public int Power() {
        int minRed = 0;
        int minGreen = 0;
        int minBlue = 0;

        foreach (CubeGameSet cubeGameSet in cubeGameSets) {
            minRed = Math.Max(minRed, cubeGameSet.Red);
            minGreen = Math.Max(minGreen, cubeGameSet.Green);
            minBlue = Math.Max(minBlue, cubeGameSet.Blue);
        }

        return minRed * minGreen * minBlue;
    }
}
