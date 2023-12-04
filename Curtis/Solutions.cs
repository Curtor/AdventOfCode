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
            {new Vector2Int(2021, 4),  new GiantSquid() },
            {new Vector2Int(2021, 5),  new HydrothermalVenture() },
            {new Vector2Int(2021, 6),  new Lanternfish() },
            {new Vector2Int(2021, 7),  new TreacheryOfWhales() },
            {new Vector2Int(2021, 8),  null },
            {new Vector2Int(2021, 9),  new SmokeBasin() },
            {new Vector2Int(2021, 10),  new SyntaxScoring() },

            // 2022
            {new Vector2Int(2022, 1),  new CalorieCounting() },
            {new Vector2Int(2022, 2),  new RockPaperScissors() },
            {new Vector2Int(2022, 3),  new RucksackReorganization() },
            {new Vector2Int(2022, 4),  null },
            {new Vector2Int(2022, 5),  null },
            {new Vector2Int(2022, 6),  new TuningTrouble() },
            {new Vector2Int(2022, 7),  new NoSpaceLeftOnDevice() },
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
            {new Vector2Int(2023, 1),  new Trebuchet() },
            {new Vector2Int(2023, 2),  new CubeConundrum() },
            {new Vector2Int(2023, 3),  new GearRatios() },
            {new Vector2Int(2023, 4),  new Scratchcards() },
            {new Vector2Int(2023, 5),  new Day5() },
            {new Vector2Int(2023, 6),  new Day6() },
            {new Vector2Int(2023, 7),  new Day7() },
            {new Vector2Int(2023, 8),  new Day8() },
            {new Vector2Int(2023, 9),  new Day9() },
            {new Vector2Int(2023, 10),  new Day10() },
            {new Vector2Int(2023, 11),  new Day11() },
            {new Vector2Int(2023, 12),  new Day12() },
            {new Vector2Int(2023, 13),  new Day13() },
            {new Vector2Int(2023, 14),  new Day14() },
            {new Vector2Int(2023, 15),  new Day15() },
            {new Vector2Int(2023, 16),  new Day16() },
            {new Vector2Int(2023, 17),  new Day17() },
            {new Vector2Int(2023, 18),  new Day18() },
            {new Vector2Int(2023, 19),  new Day19() },
            {new Vector2Int(2023, 20),  new Day20() },
            {new Vector2Int(2023, 21),  new Day21() },
            {new Vector2Int(2023, 22),  new Day22() },
            {new Vector2Int(2023, 23),  new Day23() },
            {new Vector2Int(2023, 24),  new Day24() },
            {new Vector2Int(2023, 25),  new Day25() },

            // 2024
            {new Vector2Int(2024, 1), new Advent2024.Day1() },
            {new Vector2Int(2024, 2),  new Advent2024.Day2() },
            {new Vector2Int(2024, 3),  new Advent2024.Day3() },
            {new Vector2Int(2024, 4),  new Advent2024.Day4() },
            {new Vector2Int(2024, 5),  new Advent2024.Day5() },
            {new Vector2Int(2024, 6),  new Advent2024.Day6() },
            {new Vector2Int(2024, 7),  new Advent2024.Day7() },
            {new Vector2Int(2024, 8),  new Advent2024.Day8() },
            {new Vector2Int(2024, 9),  new Advent2024.Day9() },
            {new Vector2Int(2024, 10),  new Advent2024.Day10() },
            {new Vector2Int(2024, 11),  new Advent2024.Day11() },
            {new Vector2Int(2024, 12),  new Advent2024.Day12() },
            {new Vector2Int(2024, 13),  new Advent2024.Day13() },
            {new Vector2Int(2024, 14),  new Advent2024.Day14() },
            {new Vector2Int(2024, 15),  new Advent2024.Day15() },
            {new Vector2Int(2024, 16),  new Advent2024.Day16() },
            {new Vector2Int(2024, 17),  new Advent2024.Day17() },
            {new Vector2Int(2024, 18),  new Advent2024.Day18() },
            {new Vector2Int(2024, 19),  new Advent2024.Day19() },
            {new Vector2Int(2024, 20),  new Advent2024.Day20() },
            {new Vector2Int(2024, 21),  new Advent2024.Day21() },
            {new Vector2Int(2024, 22),  new Advent2024.Day22() },
            {new Vector2Int(2024, 23),  new Advent2024.Day23() },
            {new Vector2Int(2024, 24),  new Advent2024.Day24() },
            {new Vector2Int(2024, 25),  new Advent2024.Day25() }
        };

    public static DaySolution? Get(int year, int day) {
        return solutions.GetValueOrDefault(new Vector2Int(year, day));
    }

}
