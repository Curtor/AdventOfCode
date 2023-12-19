using System.Drawing;

namespace AdventOfCode.Library;

public class ColorUtil {

    public static Color FromHexString(string hexString) {
        return ColorTranslator.FromHtml(hexString);
    }

}
