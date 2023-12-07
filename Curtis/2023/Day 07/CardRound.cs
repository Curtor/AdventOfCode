namespace csteeves.Advent2023;

public class CardRound : IComparable<CardRound> {

    public CardHand Hand { get; private set; }
    public int Bid { get; private set; }

    public CardRound(string line, bool wildJokers = false) {
        List<string> tokens = LineParser.Tokens(line);
        Hand = new CardHand(tokens[0], wildJokers);
        Bid = int.Parse(tokens[1]);
    }

    public int CompareTo(CardRound? other) {
        return other == null ? -1 : Hand.CompareTo(other.Hand);
    }

    public override string ToString() {
        return $"B{Bid,-6} for {Hand}";
    }
}
