namespace csteeves;

public class LineParser {

    public static List<string> Tokens(string input, string delimiter = " ") {
        return input.Split(delimiter).ToList();
    }
}
