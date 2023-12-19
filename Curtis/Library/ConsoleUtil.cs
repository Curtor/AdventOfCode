using System.Drawing;

namespace AdventOfCode.Library;

public class ConsoleUtil {

    public static ConsoleColor ClosestColor(Color color) {
        ConsoleColor ret = 0;
        double redChannel = color.R;
        double greenChannel = color.G;
        double blueChannel = color.B;
        double minDelta = double.MaxValue;

        foreach (ConsoleColor cc in Enum.GetValues(typeof(ConsoleColor))) {
            string? n = Enum.GetName(typeof(ConsoleColor), cc);
            Color c = Color.FromName(n == "DarkYellow" ? "Orange" : n); // bug fix
            double t =
                Math.Pow(c.R - redChannel, 2.0)
                + Math.Pow(c.G - greenChannel, 2.0)
                + Math.Pow(c.B - blueChannel, 2.0);
            if (t == 0.0) {
                return cc;
            }

            if (t < minDelta) {
                minDelta = t;
                ret = cc;
            }
        }
        return ret;
    }

}
