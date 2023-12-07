namespace csteeves.Advent2023;

public class CamelCards : DaySolution2023 {

    public override string Dir() {
        return "Day 07";
    }

    public override void Part1(List<string> input) {
        List<CardRound> cardRounds = [];
        foreach (string line in input) {
            cardRounds.Add(new CardRound(line));
        }
        cardRounds.Sort();

        GetTotalScore(cardRounds);
    }

    public override void Part2(List<string> input) {
        List<CardRound> cardRounds = [];
        foreach (string line in input) {
            cardRounds.Add(new CardRound(line, true));
        }
        cardRounds.Sort();

        GetTotalScore(cardRounds);
    }

    private static void GetTotalScore(List<CardRound> cardRounds) {
        int totalScore = 0;
        Console.WriteLine($"Sorted card hands:");
        for (int i = 0; i < cardRounds.Count; i++) {
            int rank = i + 1;
            CardRound cardRound = cardRounds[i];
            int score = rank * cardRound.Bid;

            totalScore += score;
            Console.WriteLine($"R{rank,-4} S{score,-8} | {cardRound}");
        }

        Console.WriteLine($"Total score: {totalScore}");
    }
}