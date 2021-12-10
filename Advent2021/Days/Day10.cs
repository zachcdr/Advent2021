using Advent2021.Inputs;

namespace Advent2021.Days
{
    internal static class Day10
    {
        private static IEnumerable<string>? _puzzleInput;

        public static void Part1()
        {
            _puzzleInput = PuzzleInput.Get("Day10.txt");

            var values = new Dictionary<char, int>();
            values.Add(')', 3);
            values.Add(']', 57);
            values.Add('}', 1197);
            values.Add('>', 25137);

            var illegals = new List<char>();

            foreach (var input in _puzzleInput)
            {
                var balancer = new Balancer();

                var remaining = balancer.Check(input);

                if (remaining.Count == 1)
                {
                    illegals.Add(remaining.Single());
                }
            }

            var total = 0;

            foreach (var illegal in illegals)
            {
                total += values[illegal];
            }

            Console.WriteLine(total);
        }

        public static void Part2()
        {
            _puzzleInput = PuzzleInput.Get("Day10.txt");

            var values = new Dictionary<char, int>();
            values.Add(')', 1);
            values.Add(']', 2);
            values.Add('}', 3);
            values.Add('>', 4);

            var scores = new List<long>();

            foreach (var input in _puzzleInput)
            {
                var balancer = new Balancer();

                var remaining = balancer.Check(input);

                long score = 0;

                if (remaining.Count > 1)
                {
                    foreach (var r in remaining)
                    {
                        score = score * 5;

                        score += values[r];
                    }

                    scores.Add(score);
                }
            }

            scores = scores.OrderBy(x => x).ToList();

            Console.WriteLine(scores[scores.Count / 2]);
        }
    }

    internal class Balancer
    {
        private List<char> openers = new List<char>() { '(', '{', '<', '[' };
        private List<char> closers = new List<char>() { ')', '}', '>', ']' };
        private Stack<char> stack = new Stack<char>();

        public List<char> Check(string input)
        {
            foreach (char c in input)
            {
                if (openers.Contains(c))
                {
                    stack.Push(c);
                }

                if (closers.Contains(c))
                {
                    if (stack.Count == 0)
                    {
                        return new List<char>() { c };
                    }
                    else if (openers.IndexOf(stack.Pop()) != closers.IndexOf(c))
                    {
                        return new List<char>() { c };
                    }
                }
            }

            return stack.ToList().Select(c => closers[openers.IndexOf(c)]).ToList();
        }
    }
}
