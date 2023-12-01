namespace csteeves.Advent2022;

public class RockPaperScissorsPlay {

    public enum Type { ROCK, PAPER, SCISSORS }
    public enum Result { WIN, LOSE, DRAW }

    public Type Move { get; private set; }
    public int Value { get; private set; }

    public static RockPaperScissorsPlay ROCK => new RockPaperScissorsPlay(Type.ROCK, 1);
    public static RockPaperScissorsPlay PAPER => new RockPaperScissorsPlay(Type.PAPER, 2);
    public static RockPaperScissorsPlay SCISSORS => new RockPaperScissorsPlay(Type.SCISSORS, 3);

    private RockPaperScissorsPlay(Type move, int value) {
        Move = move;
        Value = value;
    }

    public static RockPaperScissorsPlay From(string token) {
        switch (token) {
            case "A":
            case "X":
                return ROCK;
            case "B":
            case "Y":
                return PAPER;
            case "C":
            case "Z":
                return SCISSORS;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public static Result ParseResult(string token) {
        switch (token) {
            case "X":
                return Result.LOSE;
            case "Y":
                return Result.DRAW;
            case "Z":
                return Result.WIN;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public static RockPaperScissorsPlay GetPlay(RockPaperScissorsPlay them, Result outcome) {
        switch (outcome) {
            case Result.WIN:
                return them.Move switch {
                    Type.ROCK => PAPER,
                    Type.PAPER => SCISSORS,
                    Type.SCISSORS => ROCK,
                    _ => throw new ArgumentException(
                        string.Format("Unexpected play: {0}", them.Move)),
                };
            case Result.DRAW:
                return them.Move switch {
                    Type.ROCK => ROCK,
                    Type.PAPER => PAPER,
                    Type.SCISSORS => SCISSORS,
                    _ => throw new ArgumentException(
                        string.Format("Unexpected play: {0}", them.Move)),
                };
            case Result.LOSE:
                return them.Move switch {
                    Type.ROCK => SCISSORS,
                    Type.PAPER => ROCK,
                    Type.SCISSORS => PAPER,
                    _ => throw new ArgumentException(
                        string.Format("Unexpected play: {0}", them.Move)),
                };
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public int ScoreAgainst(RockPaperScissorsPlay other) {
        Result result = ResultAgainst(other);
        return Value + Score(result);
    }

    private int Score(Result result) {
        switch (result) {
            case Result.WIN:
                return 6;
            case Result.DRAW:
                return 3;
            case Result.LOSE:
                return 0;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public Result ResultAgainst(RockPaperScissorsPlay other) {
        if (Equals(other)) {
            return Result.DRAW;
        }

        return WinAgainst(other) ? Result.WIN : Result.LOSE;
    }

    /** Returns true if you (second arg) won, false if lose or draw. */
    private bool WinAgainst(RockPaperScissorsPlay other) {
        return other.Move switch {
            Type.ROCK => Move == Type.PAPER,
            Type.PAPER => Move == Type.SCISSORS,
            Type.SCISSORS => Move == Type.ROCK,
            _ => throw new ArgumentException(string.Format("Unexpected play: {0}", other.Move)),
        };
    }

    public static implicit operator string(RockPaperScissorsPlay play) {
        return play.ToString();
    }

    public override string ToString() {
        return $"{Move.ToString()}: {Value}";
    }

    public override int GetHashCode() {
        return Value;
    }

    public override bool Equals(object? obj) {
        return obj is RockPaperScissorsPlay play && Move == play.Move;
    }
}