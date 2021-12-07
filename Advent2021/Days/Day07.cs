using Advent2021.Inputs;

namespace Advent2021.Days
{
    internal static class Day07
    {
        private static IEnumerable<string>? _puzzleInput;

        public static void Part1()
        {
            _puzzleInput = PuzzleInput.Get("Day07.txt").First().Split(",");

            var positions = _puzzleInput.Select(x => int.Parse(x)).OrderBy(x => x).ToList();

            var fuels = new Dictionary<int, int>();

            for (var i = positions.First(); i < positions.Last(); i++)
            {
                if (!fuels.ContainsKey(i))
                {
                    fuels.Add(i, 0);
                }
                else
                {
                    continue;
                }

                foreach (var position in positions)
                {
                    var totalSteps = Math.Abs(i - position);

                    fuels[i] += totalSteps;
                }
            }

            Console.WriteLine(fuels.Min(f => f.Value));
        }

        public static void Part2()
        {
            _puzzleInput = PuzzleInput.Get("Day07.txt").First().Split(",");

            var positions = _puzzleInput.Select(x => int.Parse(x)).OrderBy(x => x).ToList();

            var fuels = new Dictionary<int, int>();

            for (var i = positions.First(); i < positions.Last(); i++)
            {
                if (!fuels.ContainsKey(i))
                {
                    fuels.Add(i, 0);
                }
                else
                {
                    continue;
                }

                foreach (var position in positions)
                {
                    var totalSteps = Math.Abs(i - position);

                    for (int j = 1; j <= totalSteps; j++)
                    {
                        fuels[i] += j;
                    }
                }
            }

            Console.WriteLine(fuels.Min(f => f.Value));
        }
    }
}
