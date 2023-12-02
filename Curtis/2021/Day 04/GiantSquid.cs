namespace csteeves.Advent2021;

public class GiantSquid : DaySolution2021 {

    public override string Dir() {
        return "Day 04";
    }

    public override void Part1(List<string> input) {
        List<int> calledNumbers = LineParser.Tokens(input[0], ",").Select(int.Parse).ToList();
        List<BingoCard> bingoCards = CreateBoards(input);

        Tuple<int, BingoCard> results = GetResults(calledNumbers, bingoCards).First();
        PrettyPrintResults(results);
    }

    public override void Part2(List<string> input) {
        List<int> calledNumbers = LineParser.Tokens(input[0], ",").Select(int.Parse).ToList();
        List<BingoCard> bingoCards = CreateBoards(input);

        Tuple<int, BingoCard> results = GetResults(calledNumbers, bingoCards).Last();
        PrettyPrintResults(results);
    }

    private static List<BingoCard> CreateBoards(List<string> input) {
        List<BingoCard> bingoCards = [];

        for (int i = 1; i < input.Count; i++) {
            if (string.IsNullOrEmpty(input[i])) {
                continue;
            }

            BingoCard card = new BingoCard(
                input[i], input[i + 1], input[i + 2], input[i + 3], input[i + 4]);
            bingoCards.Add(card);
            i += 4;
        }

        return bingoCards;
    }

    private static IEnumerable<Tuple<int, BingoCard>> GetResults(
            List<int> calledNumbers, List<BingoCard> bingoCards) {

        foreach (int number in calledNumbers) {
            foreach (BingoCard card in bingoCards) {
                if (card.StillPlaying() && card.Call(number)) {

                    yield return Tuple.Create(number, card);
                }
            }
        }
    }

    private void PrettyPrintResults(Tuple<int, BingoCard> winResults) {
        int winningNumber = winResults.Item1;
        BingoCard card = winResults.Item2;

        int unmarkedSum = card.GetUnmarkedSum();
        int score = winningNumber * unmarkedSum;

        Console.WriteLine($"Winning number: {winningNumber}");
        Console.WriteLine();
        Console.WriteLine("Winning card:");
        card.PrettyPrint();
        Console.WriteLine();
        Console.WriteLine($"Unmarked sum: {unmarkedSum}");
        Console.WriteLine($"Score: {score}");
    }
}