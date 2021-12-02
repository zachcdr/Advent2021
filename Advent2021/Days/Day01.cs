using Advent2021.Inputs;

namespace Advent2021.Days
{
    internal class Day01
    {
        public static int Part1()
        {
            var depths = PuzzleInput.Get("Day01.txt");

            var increaseCount = 0;

            var previousDepth = 0;

            foreach (var depth in depths)
            {
                var d = Convert.ToInt32(depth);

                if (previousDepth < d)
                {
                    increaseCount++;
                }

                previousDepth = d;
            }

            return increaseCount - 1;
        }

        public static int Part2()
        {
            var depths = PuzzleInput.Get("Day01.txt").ToList();

            var increaseCount = 0;

            var currentSum = 0;

            var previousSum = 0;

            for (var i = 0; i <= depths.Count - 3; i++)
            {
                currentSum = Convert.ToInt32(depths[i]) + Convert.ToInt32(depths[i + 1]) + Convert.ToInt32(depths[i + 2]);

                if (i != 0 && previousSum < currentSum)
                {
                    increaseCount++;
                }

                previousSum = currentSum;
            }

            return increaseCount;
        }
    }
}
