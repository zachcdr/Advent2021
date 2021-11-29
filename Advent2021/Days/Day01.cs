namespace Advent2021.Days
{
    internal class Day01
    {
        public Day01(string? inputOverride = null)
        {
            if (!string.IsNullOrEmpty(inputOverride))
            {
                _puzzleInput = inputOverride;
            }
        }

        public string Part1()
        {
            return _puzzleInput;
        }

        private readonly string _puzzleInput = @"";
    }
}
