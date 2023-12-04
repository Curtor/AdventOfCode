﻿namespace csteeves.Advent2023;

public class CardGameRound {

    public readonly int cardNumber;
    public readonly List<int> winningNumbers;
    public readonly List<int> yourNumbers;

    private List<int>? cachedOverlap = null;

    public int Score { get; set; }

    public CardGameRound(string line) {
        List<string> split = LineParser.Tokens(line, "|");
        List<string> inputTokens = LineParser.Tokens(split[0], ":");

        cardNumber = int.Parse(LineParser.Tokens(inputTokens[0])[1]);
        winningNumbers = LineParser.Tokens(inputTokens[1]).Select(int.Parse).ToList();
        yourNumbers = LineParser.Tokens(split[1]).Select(int.Parse).ToList();
    }

    public IEnumerable<int> Overlap() {
        if (cachedOverlap == null) {
            cachedOverlap = yourNumbers.Where(n => winningNumbers.Contains(n)).ToList();
        }
        return cachedOverlap;

    }
}
