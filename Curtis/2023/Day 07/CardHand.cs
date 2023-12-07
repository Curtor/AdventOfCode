namespace csteeves.Advent2023;

public class CardHand : IComparable<CardHand> {

    public enum Rank {
        FIVE_OF_A_KIND = 7,
        FOUR_OF_A_KIND = 6,
        FULL_HOUSE = 5,
        THREE_OF_A_KIND = 4,
        TWO_PAIR = 3,
        ONE_PAIR = 2,
        HIGH_CARD = 1,
    }

    private List<char> cards;
    private Rank rank;
    private bool wildJokers;

    public CardHand(string v, bool wildJokers = false) {
        cards = v.ToCharArray().ToList();
        rank = GetRank(cards, wildJokers);

        this.wildJokers = wildJokers;
    }

    public static Rank GetRank(List<char> cards, bool wildJokers) {
        Dictionary<char, int> cardCountMap = [];
        foreach (char card in cards) {
            if (cardCountMap.ContainsKey(card)) {
                cardCountMap[card]++;
            } else {
                cardCountMap[card] = 1;
            }
        }

        int numJokers = 0;
        if (wildJokers && cardCountMap.TryGetValue('J', out numJokers) && numJokers == 5) {
            return Rank.FIVE_OF_A_KIND;
        }

        List<int> countCardValues = [];
        Dictionary<int, List<char>> countCards = [];
        foreach (KeyValuePair<char, int> kvp in cardCountMap) {
            countCardValues.Add(kvp.Value);
            if (countCards.TryGetValue(kvp.Value, out List<char>? chars)) {
                chars.Add(kvp.Key);
            } else {
                countCards[kvp.Value] = [kvp.Key];
            }
        }

        countCardValues = countCardValues.Distinct().ToList();
        countCardValues.Sort();
        countCardValues.Reverse();

        bool threeOfAKind = false;
        int numPairs = 0;
        foreach (int cardCount in countCardValues) {
            foreach (char card in countCards[cardCount]) {
                if (wildJokers && card == 'J') {
                    continue;
                }

                if (cardCount + numJokers == 5) {
                    return Rank.FIVE_OF_A_KIND;
                }

                if (cardCount + numJokers == 4) {
                    return Rank.FOUR_OF_A_KIND;
                }

                if (cardCount + numJokers == 3) {
                    numJokers = numJokers - 3 + cardCount;
                    threeOfAKind = true;
                } else if (cardCount + numJokers == 2) {
                    numJokers = numJokers - 2 + cardCount;
                    numPairs++;
                }
            }
        }

        if (threeOfAKind) {
            return numPairs > 0 ? Rank.FULL_HOUSE : Rank.THREE_OF_A_KIND;
        }

        if (numPairs == 2) {
            return Rank.TWO_PAIR;
        }
        if (numPairs == 1) {
            return Rank.ONE_PAIR;
        }
        return Rank.HIGH_CARD;
    }

    public int CompareTo(CardHand? other) {
        if (other == null) {
            return -1;
        }

        int compareResult = ((int)rank).CompareTo((int)other.rank);
        if (compareResult != 0) {
            return compareResult;
        }

        return CompareCards(cards, other.cards);
    }

    private int CompareCards(List<char> cards1, List<char> cards2) {
        for (int i = 0; i < cards1.Count; i++) {
            int card1Value = GetCardValue(cards1[i]);
            int card2Value = GetCardValue(cards2[i]);

            int compareResult = card1Value - card2Value;
            if (compareResult != 0) {
                return compareResult;
            }
        }

        return 0;
    }

    private int GetCardValue(char card) {
        if (!IsFaceCard(card)) {
            return card - '0';
        }

        switch (card) {
            case 'T': return 10;
            case 'J': return wildJokers ? 1 : 11;
            case 'Q': return 12;
            case 'K': return 13;
            case 'A': return 14;
        }

        throw new ArgumentOutOfRangeException();
    }

    private bool IsFaceCard(char card) {
        return card == 'T' || card == 'J' || card == 'Q' || card == 'K' || card == 'A';
    }

    public override string ToString() {
        return $"{rank}({(int)rank}) with {cards.Join("")}";
    }
}
