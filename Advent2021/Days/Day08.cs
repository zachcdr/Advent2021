using Advent2021.Inputs;

namespace Advent2021.Days
{
    internal static class Day08
    {
        private static IEnumerable<string>? _puzzleInput;

        private static List<int> unique = new List<int>() { 2, 4, 3, 7 };

        public static void Part1()
        {
            _puzzleInput = PuzzleInput.Get("Day08.txt");

            var total = 0;

            foreach (var input in _puzzleInput)
            {
                var output = input.Split("|")[1].Trim().Split(" ");

                foreach (var op in output)
                {
                    if (unique.Contains(op.Length))
                    {
                        total++;
                    }
                }
            }

            Console.WriteLine(total);
        }

        public static void Part2()
        {
            _puzzleInput = PuzzleInput.Get("Day08.txt");

            var total = 0;

            foreach (var input in _puzzleInput)
            {
                var cracker = new NumberCracker(input);

                cracker.Process();

                total += int.Parse(cracker.Value);
            }

            Console.WriteLine(total);
        }
    }

    internal class SubMap
    {
        public SubMap(int number, int count)
        {
            Number = number;
            Count = count;
        }

        public int Number { get; }
        public int Count { get; }
    }

    internal class SubNumber
    {
        private readonly IEnumerable<SubMap> _subNumbers = new List<SubMap>()
        {
            new SubMap(0, 6),
            new SubMap(1, 2),
            new SubMap(2, 5),
            new SubMap(3, 5),
            new SubMap(4, 4),
            new SubMap(5, 5),
            new SubMap(6, 6),
            new SubMap(7, 3),
            new SubMap(8, 7),
            new SubMap(9, 6),
        };

        public SubNumber(string wire)
        {
            Wire = wire;
            PotentialNumbers = _subNumbers.Where(sn => sn.Count == wire.Length).Select(sn => sn.Number).ToList();
        }

        public List<int> PotentialNumbers { get; set; }
        public string Wire { get; set; }
        public bool IsSolved => PotentialNumbers.Count == 1;
        public int Number => IsSolved ? PotentialNumbers.Single() : -1;
    }

    internal class NumberCracker
    {
        private readonly List<SubNumber> _input;
        private readonly List<string> _output;

        public string Value { get; private set; }

        public NumberCracker(string data)
        {
            _input = data.Split("|")[0].Trim().Split(" ").ToList().Select(v => new SubNumber(v)).ToList();
            _output = data.Split("|")[1].Trim().Split(" ").ToList();
        }

        public void Process()
        {
            while (_input.Any(i => !i.IsSolved))
            {
                foreach (var input in _input)
                {
                    if (input.IsSolved)
                    {
                        _input.ForEach(i =>
                        {
                            if (i.Number != input.Number)
                            {
                                i.PotentialNumbers.Remove(input.Number);
                            }
                        });

                        continue;
                    }

                    var toRemove = new List<int>();

                    foreach (var num in input.PotentialNumbers)
                    {
                        var parents = new List<int>();
                        var children = new List<int>();

                        children.AddRange(GetChildren(num));

                        var solvedChilden = _input.Where(i => children.Contains(i.Number));

                        var remove = false;

                        solvedChilden.ToList().ForEach(c =>
                        {
                            if (!Solve(input.Wire, c.Wire))
                            {
                                remove = true;
                            }
                        });

                        if (remove)
                        {
                            toRemove.Add(num);
                            remove = false;
                        }

                        parents.AddRange(GetParents(num));

                        var solvedParents = _input.Where(i => parents.Contains(i.Number));

                        solvedParents.ToList().ForEach(p =>
                        {
                            if (!Solve(p.Wire, input.Wire))
                            {
                                remove = true;
                            }
                        });

                        if (remove)
                        {
                            toRemove.Add(num);
                            remove = false;
                        }
                    }

                    foreach (var remove in toRemove)
                    {
                        input.PotentialNumbers.Remove(remove);
                    }
                }
            }

            foreach (var output in _output)
            {
                var matches = _input.Where(i => i.Wire.Length == output.Length);

                foreach (var match in matches)
                {
                    var success = true;
                    foreach (var w in match.Wire)
                    {
                        if (!output.Contains(w))
                        {
                            success = false;
                            break;
                        }
                    }

                    if (success)
                    {
                        Value += match.Number.ToString();
                    }
                }
            }
        }

        private bool Solve(string parentWire, string childWire)
        {
            foreach (var w in childWire)
            {
                if (!parentWire.Contains(w))
                {
                    return false;
                }
            }

            return true;
        }

        private List<int> GetParents(int child)
        {
            var parents = new List<int>();

            switch (child)
            {
                case 0:
                    parents = new List<int>() { 8 };
                    break;
                case 1:
                    parents = new List<int>() { 0, 3, 4, 7, 8, 9 };
                    break;
                case 2:
                    parents = new List<int>() { 8 };
                    break;
                case 3:
                    parents = new List<int>() { 8, 9 };
                    break;
                case 4:
                    parents = new List<int>() { 8, 9 };
                    break;
                case 5:
                    parents = new List<int>() { 6, 8, 9 };
                    break;
                case 6:
                    parents = new List<int>() { 8 };
                    break;
                case 7:
                    parents = new List<int>() { 0, 3, 8, 9 };
                    break;
                case 8:
                    parents = new List<int>() { };
                    break;
                case 9:
                    parents = new List<int>() { 8 };
                    break;
            }

            return parents;
        }

        private List<int> GetChildren(int parent)
        {
            var children = new List<int>();

            switch (parent)
            {
                case 0:
                    children = new List<int>() { 1, 7 };
                    break;
                case 1:
                    children = new List<int>() { };
                    break;
                case 2:
                    children = new List<int>() { };
                    break;
                case 3:
                    children = new List<int>() { 1, 7 };
                    break;
                case 4:
                    children = new List<int>() { 1 };
                    break;
                case 6:
                    children = new List<int>() { 5 };
                    break;
                case 7:
                    children = new List<int>() { 1 };
                    break;
                case 9:
                    children = new List<int>() { 1, 3, 4, 5, 7 };
                    break;
            }

            return children;
        }
    }
}
