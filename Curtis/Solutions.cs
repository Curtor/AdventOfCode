namespace csteeves;

using csteeves.Advent2021;
using csteeves.Advent2022;
using csteeves.Advent2023;

public static class Solutions {

    private static Dictionary<Vector2Int, DaySolution?> solutions =
        new Dictionary<Vector2Int, DaySolution?> {

            // 2022
            {new Vector2Int(2021, 1),  new SonarSweep() },
            {new Vector2Int(2021, 2),  new Dive() },
            {new Vector2Int(2021, 3),  new BinaryDiagnostic() },

            // 2022
            {new Vector2Int(2022, 1),  new CalorieCounting() },
            {new Vector2Int(2022, 2),  null },
            {new Vector2Int(2022, 3),  null },
            {new Vector2Int(2022, 4),  null },
            {new Vector2Int(2022, 5),  null },
            {new Vector2Int(2022, 6),  null },
            {new Vector2Int(2022, 7),  null },
            {new Vector2Int(2022, 8),  null },
            {new Vector2Int(2022, 9),  null },
            {new Vector2Int(2022, 10),  null },
            {new Vector2Int(2022, 11),  null },
            {new Vector2Int(2022, 12),  new HillClimbing() },
            {new Vector2Int(2022, 13),  null },
            {new Vector2Int(2022, 14),  null },
            {new Vector2Int(2022, 15),  null },
            {new Vector2Int(2022, 16),  null },
            {new Vector2Int(2022, 17),  null },
            {new Vector2Int(2022, 18),  null },
            {new Vector2Int(2022, 19),  null },
            {new Vector2Int(2022, 20),  null },
            {new Vector2Int(2022, 21),  null },
            {new Vector2Int(2022, 22),  null },
            {new Vector2Int(2022, 23),  null },
            {new Vector2Int(2022, 24),  null },
            {new Vector2Int(2022, 25),  null },

            // 2023
            {new Vector2Int(2023, 1),  new Day1() },
            {new Vector2Int(2023, 2),  null },
            {new Vector2Int(2023, 3),  null },
            {new Vector2Int(2023, 4),  null },
            {new Vector2Int(2023, 5),  null },
            {new Vector2Int(2023, 6),  null },
            {new Vector2Int(2023, 7),  null },
            {new Vector2Int(2023, 8),  null },
            {new Vector2Int(2023, 9),  null },
            {new Vector2Int(2023, 10),  null },
            {new Vector2Int(2023, 11),  null },
            {new Vector2Int(2023, 12),  null },
            {new Vector2Int(2023, 13),  null },
            {new Vector2Int(2023, 14),  null },
            {new Vector2Int(2023, 15),  null },
            {new Vector2Int(2023, 16),  null },
            {new Vector2Int(2023, 17),  null },
            {new Vector2Int(2023, 18),  null },
            {new Vector2Int(2023, 19),  null },
            {new Vector2Int(2023, 20),  null },
            {new Vector2Int(2023, 21),  null },
            {new Vector2Int(2023, 22),  null },
            {new Vector2Int(2023, 23),  null },
            {new Vector2Int(2023, 24),  null },
            {new Vector2Int(2023, 25),  null }
        };

    public static DaySolution? Get(int year, int day) {
        return solutions[new Vector2Int(year, day)];
    }

}
