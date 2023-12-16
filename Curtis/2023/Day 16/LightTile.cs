


namespace csteeves.Advent2023;

internal class LightTile {

    public enum Direction { RIGHT, DOWN, LEFT, UP }

    private char character;
    private HashSet<Direction> light = [];

    public LightTile(char c) {
        character = c;
    }

    public IEnumerable<Direction> AddAndFollowLight(Direction lightDirection) {
        if (light.Contains(lightDirection)) {
            yield break;
        }

        light.Add(lightDirection);
        foreach (Direction direction in GetNextLightDirections(lightDirection)) {
            yield return direction;
        }
    }

    private IEnumerable<Direction> GetNextLightDirections(Direction lightDirection) {
        if (character == '.') {
            yield return lightDirection;
            yield break;
        }

        switch (character) {
            case '|':
                if (lightDirection == Direction.UP || lightDirection == Direction.DOWN) {
                    yield return lightDirection;
                } else {
                    yield return Direction.UP;
                    yield return Direction.DOWN;
                }
                yield break;
            case '-':
                if (lightDirection == Direction.LEFT || lightDirection == Direction.RIGHT) {
                    yield return lightDirection;
                } else {
                    yield return Direction.LEFT;
                    yield return Direction.RIGHT;
                }
                yield break;
            case '/':
                switch (lightDirection) {
                    case Direction.RIGHT:
                        yield return Direction.UP;
                        yield break;
                    case Direction.DOWN:
                        yield return Direction.LEFT;
                        yield break;
                    case Direction.LEFT:
                        yield return Direction.DOWN;
                        yield break;
                    case Direction.UP:
                        yield return Direction.RIGHT;
                        yield break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            case '\\':
                switch (lightDirection) {
                    case Direction.RIGHT:
                        yield return Direction.DOWN;
                        yield break;
                    case Direction.DOWN:
                        yield return Direction.RIGHT;
                        yield break;
                    case Direction.LEFT:
                        yield return Direction.UP;
                        yield break;
                    case Direction.UP:
                        yield return Direction.LEFT;
                        yield break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public bool ContainsLight() {
        return light.Any();
    }

    public override string ToString() {
        if (ContainsLight()) {
            if (light.Count > 1) {
                return light.Count.ToString();
            }
            switch (light.First()) {
                case Direction.RIGHT: return ">";
                case Direction.DOWN: return "v";
                case Direction.LEFT: return "<";
                case Direction.UP: return "^";
                default: throw new ArgumentOutOfRangeException();
            }
        }
        return character.ToString();
    }

    internal void Reset() {
        light = [];
    }
}