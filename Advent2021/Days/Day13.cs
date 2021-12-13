using Advent2021.Inputs;
using System.Drawing;

namespace Advent2021.Days
{
    internal static class Day13
    {
        private static IEnumerable<string>? _puzzleInput;

        public static void Part1()
        {
            _puzzleInput = PuzzleInput.Get("Day13.txt");

            var points = new List<Point>();

            var folds = new List<string>();

            var startFolds = false;

            foreach (var puzzle in _puzzleInput)
            {
                if (!startFolds && string.IsNullOrEmpty(puzzle))
                {
                    startFolds = true;
                    continue;
                }

                if (!startFolds)
                {
                    var pair = puzzle.Split(",");

                    var point = new Point(int.Parse(pair[0].ToString()), int.Parse(pair[1].ToString()));

                    if (!points.Contains(point))
                    {
                        points.Add(point);
                    }
                }
                else
                {
                    folds.Add(puzzle);
                }
            }

            var firstFold = folds.First();

            var equals = firstFold.IndexOf("=");
            var foldOn = firstFold[equals - 1];
            var foldLocation = int.Parse(firstFold.Substring(equals + 1, firstFold.Length - equals - 1));

            if (foldOn == 'x')
            {
                points = points.Where(p => p.X != foldLocation).ToList();

                for (var i = 0; i < points.Count; i++)
                {
                    if (points[i].X > foldLocation)
                    {
                        points[i] = new Point(foldLocation * 2 - points[i].X, points[i].Y);
                    }
                }
            }
            else
            {
                points = points.Where(p => p.Y != foldLocation).ToList();

                for (var i = 0; i < points.Count; i++)
                {
                    if (points[i].Y > foldLocation)
                    {
                        points[i] = new Point(points[i].X, foldLocation * 2 - points[i].Y);
                    }
                }
            }

            points = points.Where(p => p.Y < foldLocation).ToList();

            Console.WriteLine(points.Distinct().Count());
        }

        public static void Part2()
        {
            _puzzleInput = PuzzleInput.Get("Day13.txt");

            var points = new List<Point>();

            var folds = new List<string>();

            var startFolds = false;

            foreach (var puzzle in _puzzleInput)
            {
                if (!startFolds && string.IsNullOrEmpty(puzzle))
                {
                    startFolds = true;
                    continue;
                }

                if (!startFolds)
                {
                    var pair = puzzle.Split(",");

                    var point = new Point(int.Parse(pair[0].ToString()), int.Parse(pair[1].ToString()));

                    if (!points.Contains(point))
                    {
                        points.Add(point);
                    }
                }
                else
                {
                    folds.Add(puzzle);
                }
            }

            var xMax = points.Max(x => x.X);
            var xMin = points.Min(x => x.X);
            var yMax = points.Max(x => x.Y);
            var yMin = points.Min(x => x.Y);

            foreach (var fold in folds)
            {
                var equals = fold.IndexOf("=");
                var foldOn = fold[equals - 1];
                var foldLocation = int.Parse(fold.Substring(equals + 1, fold.Length - equals - 1));

                if (foldOn == 'x')
                {
                    points = points.Where(p => p.X != foldLocation).ToList();

                    for (var i = 0; i < points.Count; i++)
                    {
                        if (points[i].X > foldLocation)
                        {
                            points[i] = new Point(foldLocation * 2 - points[i].X, points[i].Y);
                        }
                    }
                }
                else
                {
                    points = points.Where(p => p.Y != foldLocation).ToList();

                    for (var i = 0; i < points.Count; i++)
                    {
                        if (points[i].Y > foldLocation)
                        {
                            points[i] = new Point(points[i].X, foldLocation * 2 - points[i].Y);
                        }
                    }
                }

                points = points.Distinct().ToList();

                xMax = points.Max(x => x.X);
                xMin = points.Min(x => x.X);
                yMax = points.Max(x => x.Y);
                yMin = points.Min(x => x.Y);
            }

            var line = "";

            for (var y = 0; y <= yMax; y++)
            {
                for (var x = 0; x <= xMax; x++)
                {
                    var point = new Point(x, y);

                    if (points.Contains(point))
                    {
                        line += '#';
                    }
                    else
                    {
                        line += '.';
                    }
                }

                Console.WriteLine(line);
                line = "";
            }
        }
    }
}
