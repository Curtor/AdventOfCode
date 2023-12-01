namespace csteeves.Advent2022;

public class RockPaperScissors : DaySolution2022 {

    public override string Dir() {
        return "Day 02";
    }

    public override void Part1(List<string> input) {
        int totalScore = 0;

        foreach (string line in input) {
            List<string> tokens = LineParser.Tokens(line);
            RockPaperScissorsPlay them = RockPaperScissorsPlay.From(tokens[0]);
            RockPaperScissorsPlay us = RockPaperScissorsPlay.From(tokens[1]);

            int roundScore = us.ScoreAgainst(them);
            totalScore += roundScore;
        }

        Console.WriteLine("Part 1");
        Console.WriteLine($"Score: {totalScore}");
    }

    public override void Part2(List<string> input) {
        int totalScore = 0;

        foreach (string line in input) {
            List<string> tokens = LineParser.Tokens(line);
            RockPaperScissorsPlay them = RockPaperScissorsPlay.From(tokens[0]);
            RockPaperScissorsPlay.Result outcome = RockPaperScissorsPlay.ParseResult(tokens[1]);

            RockPaperScissorsPlay us = RockPaperScissorsPlay.GetPlay(them, outcome);

            int roundScore = us.ScoreAgainst(them);
            totalScore += roundScore;
        }

        Console.WriteLine("Part 2");
        Console.WriteLine($"Score: {totalScore}");
    }
}