using Advent2021.Inputs;

namespace Advent2021.Days
{
    internal static class DayX
    {
        private static IEnumerable<string>? _puzzleInput;

        public static string Part1()
        {
            _puzzleInput = PuzzleInput.Get("DayX.txt");

            return _puzzleInput.First();
        }

        public static string Part2()
        {
            _puzzleInput = PuzzleInput.Get("DayX.txt");

            return _puzzleInput.First();
        }
    }
}
