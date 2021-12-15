using Advent2021.Inputs;
using System.Numerics;

namespace Advent2021.Days
{
    internal static class Day14
    {
        private static Dictionary<string, string> _puzzleInput = new Dictionary<string, string>();

        private static Dictionary<string, long> _counts = new Dictionary<string, long>();

        private static Dictionary<string, long> _takers = new Dictionary<string, long>();

        public static void Part1()
        {
            DoSteps(10);
        }

        public static void Part2()
        {
            DoSteps(40);
        }

        public static void DoSteps(int totalSteps)
        {
            var puzzleInput = PuzzleInput.Get("Day14.txt");

            var start = puzzleInput.First();

            for (var i = 0; i < start.Length; i++)
            {
                if (_counts.ContainsKey(start[i].ToString()))
                {
                    _counts[start[i].ToString()]++;
                }
                else
                {
                    _counts.Add(start[i].ToString(), 1);
                }

                if (i < (start.Length - 1))
                {
                    if (_takers.ContainsKey(start[i].ToString() + start[i + 1]))
                    {
                        _takers[start[i].ToString() + start[i + 1]]++;
                    }
                    else
                    {
                        _takers.Add(start[i].ToString() + start[i + 1], 1);
                    }

                }
            }

            foreach (var input in puzzleInput)
            {
                if (!input.Contains("->"))
                {
                    continue;
                }

                var dict = input.Split(" -> ");

                _puzzleInput.Add(dict[0], dict[1]);
            }

            for (var i = 0; i < totalSteps; i++)
            {
                DoInsertion2();
            }

            var min = _counts.Values.Min();
            var max = _counts.Values.Max();

            Console.WriteLine(max - min);
        }

        private static void DoInsertion2()
        {
            var takerList = new List<Dictionary<string, long>>();

            foreach (var kvp in _takers)
            {
                var takers = new Dictionary<string, long>();

                var toInsert = _puzzleInput[kvp.Key];

                if (_counts.ContainsKey(toInsert))
                {
                    _counts[toInsert] += kvp.Value;
                }
                else
                {
                    _counts.Add(toInsert, kvp.Value);
                }

                var key1 = kvp.Key[0] + toInsert;
                var key2 = toInsert + kvp.Key[1];

                takers.Add(key1, kvp.Value);
                takers.Add(key2, kvp.Value);

                takerList.Add(takers);
            }

            _takers = new Dictionary<string, long>();

            foreach (var t in takerList)
            {
                foreach (var kvp in t)
                {
                    if (_takers.ContainsKey(kvp.Key))
                    {
                        _takers[kvp.Key] += kvp.Value;
                    }
                    else
                    {
                        _takers.Add(kvp.Key, kvp.Value);
                    }
                }
            }
        }
    }
}
