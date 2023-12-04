namespace csteeves.AdventLibrary;

public class LineParser {

    public static List<string> Tokens(string input, string delimiter = " ") {
        return input.Split(delimiter, StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    public static byte[] ToBytes(string input) {
        ulong value = Convert.ToUInt64(input, 2);
        return BitConverter.GetBytes(value);
    }
}
