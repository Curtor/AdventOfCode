namespace csteeves.Advent2023;

public class CubeConundrum : DaySolution2023 {

    public override string Dir() {
        return "Day 02";
    }

    public override void Part1(List<string> input) {
        List<CubeGame> cubeGames = GetCubeGames(input);

        int sum = 0;
        foreach (CubeGame game in cubeGames) {
            bool possible = game.IsPossible(12, 13, 14);
            if (possible) {
                sum += game.gameNumber;
            }
            Console.WriteLine($"Game {game.gameNumber}: {possible}");
        }

        Console.WriteLine($"Sum {sum}");
    }

    public override void Part2(List<string> input) {
        List<CubeGame> cubeGames = GetCubeGames(input);

        int sum = 0;
        foreach (CubeGame game in cubeGames) {
            int power = game.Power();
            sum += power;
            Console.WriteLine($"Game {game.gameNumber}: {power}");
        }

        Console.WriteLine($"Sum {sum}");
    }

    private static List<CubeGame> GetCubeGames(List<string> input) {
        List<CubeGame> cubeGames = [];

        for (int i = 0; i < input.Count; i++) {
            List<string> tokens = LineParser.Tokens(input[i], ":");
            int gameNumber = int.Parse(LineParser.Tokens(tokens[0])[1]);
            cubeGames.Add(new CubeGame(gameNumber, tokens[1]));
        }

        return cubeGames;
    }
}