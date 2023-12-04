namespace csteeves.Advent2023;

public class Scratchcards : DaySolution2023 {

    public override string Dir() {
        return "Day 04";
    }

    public override void Part1(List<string> input) {
        int totalScore = 0;

        for (int i = 0; i < input.Count; i++) {
            string line = input[i];
            CardGameRound round = new CardGameRound(line);
            int overlap = round.Overlap().Count();

            int score = 0;
            if (overlap > 0) {
                score = (int)Math.Pow(2, overlap - 1);
            }

            totalScore += score;
        }

        Console.WriteLine($"Total score: {totalScore}");
    }

    public override void Part2(List<string> input) {
        Dictionary<int, CardGameRound> cardRounds = [];

        for (int i = 0; i < input.Count; i++) {
            string line = input[i];
            CardGameRound round = new CardGameRound(line);
            cardRounds[round.cardNumber] = round;
        }

        int totalCards = 0;
        Dictionary<int, int> cardRoundTotals = [];
        foreach (KeyValuePair<int, CardGameRound> kvp in cardRounds) {
            totalCards += GetCardRoundTotal(kvp.Value, cardRounds, cardRoundTotals);
        }

        Console.WriteLine($"Total cards: {totalCards}");
    }

    private int GetCardRoundTotal(
            CardGameRound cardGameRound,
            Dictionary<int, CardGameRound> cardRounds,
            Dictionary<int, int> cardRoundTotals) {

        if (cardRoundTotals.TryGetValue(cardGameRound.cardNumber, out int total)) {
            return total;
        }

        int result = 1;
        int overlap = cardGameRound.Overlap().Count();

        for (int i = 0; i < overlap; i++) {
            int earnedCardRoundNumber = cardGameRound.cardNumber + i + 1;
            CardGameRound earnedCardRound = cardRounds[earnedCardRoundNumber];
            result += GetCardRoundTotal(earnedCardRound, cardRounds, cardRoundTotals);
        }

        cardRoundTotals[cardGameRound.cardNumber] = result;
        return result;
    }
}