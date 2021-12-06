using Advent2021.Inputs;
using System.Numerics;

namespace Advent2021.Days
{
    internal static class Day06
    {
        private static IEnumerable<string>? _puzzleInput;

        public static void Part1(int days)
        {
            _puzzleInput = PuzzleInput.Get("Day06.txt").First().Split(",");

            var groups = _puzzleInput.GroupBy(p => p);

            var school = new List<LanternFish>();

            foreach (var input in groups)
            {
                school.Add(new LanternFish(int.Parse(input.Key), input.Count()));
            }

            for (int i = 0; i < days; i++)
            {
                BigInteger newFish = 0;

                foreach (var fish in school)
                {
                    fish.Timer--;

                    if (fish.Timer < 0)
                    {
                        fish.Reset();

                        newFish += fish.Total;
                    }
                }

                if (newFish > 0)
                {
                    school.Add(new LanternFish(8, newFish));
                }
            }

            BigInteger totalFish = 0;

            foreach (var fish in school)
            {
                if (totalFish < 0)
                {
                    Console.WriteLine(totalFish);
                }

                totalFish += fish.Total;
            }

            Console.WriteLine(totalFish);
        }
    }

    internal class LanternFish
    {
        public LanternFish(int timer, BigInteger total)
        {
            Timer = timer;
            Total = total;
        }

        public int Timer { get; set; }
        public BigInteger Total { get; set; }
        public void Reset()
        {
            Timer = 6;
        }
    }
}
