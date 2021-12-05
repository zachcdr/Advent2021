using Advent2021.Inputs;
using System.Drawing;

namespace Advent2021.Days
{
    internal static class Day05
    {
        private static IEnumerable<string>? _puzzleInput;

        public static void ProcessVents(bool doPart2 = false)
        {
            _puzzleInput = PuzzleInput.Get("Day05.txt");

            var vents = new List<LineVents>();

            foreach (var input in _puzzleInput)
            {
                vents.Add(new LineVents(input, doPart2));
            }

            var combinedPaths = new Dictionary<Point, int>();

            foreach (var vent in vents)
            {
                foreach (var point in vent.Points)
                {
                    if (combinedPaths.ContainsKey(point))
                    {
                        combinedPaths[point]++;
                    }
                    else
                    {
                        combinedPaths.Add(point, 1);
                    }
                }
            }

            Console.WriteLine(combinedPaths.Where(path => path.Value > 1).Count());
        }
    }

    internal class LineVents
    {
        public LineVents(string raw, bool doPart2)
        {
            var values = raw.Split(" -> ");

            var first = new Point(Convert.ToInt32(values[0].Split(",")[0]), Convert.ToInt32(values[0].Split(",")[1]));
            var second = new Point(Convert.ToInt32(values[1].Split(",")[0]), Convert.ToInt32(values[1].Split(",")[1]));

            if (first.X == second.X)
            {
                if (first.Y < second.Y)
                {
                    Start = first;
                    End = second;
                }
                else 
                {
                    Start = second;
                    End = first;
                }
            }
            else
            {
                if (first.X < second.X)
                {
                    Start = first;
                    End = second;
                }
                else
                {
                    Start = second;
                    End = first;
                }
            }

            Points = GetPoints(doPart2);
        }

        public Point Start { get; set; }
        public Point End { get; set; }
        public List<Point> Points { get; set; }

        private List<Point> GetPoints(bool doPart2)
        {
            var points = new List<Point>();

            if (Start.X == End.X)
            {
                var x = Start.X;

                var y = Start.Y;

                points.Add(Start);

                while (y < End.Y - 1)
                {
                    y++;

                    points.Add(new Point(x, y));
                }

                points.Add(End);
            }
            else if (Start.Y == End.Y)
            {
                var y = Start.Y;

                var x = Start.X;

                points.Add(Start);

                while (x < End.X - 1)
                {
                    x++;

                    points.Add(new Point(x, y));
                }

                points.Add(End);
            }
            else if (doPart2)
            {
                var start = Start;

                points.Add(Start);

                while (start != End)
                {
                    if (start.X < End.X)
                    {
                        if (start.Y < End.Y)
                        {
                            start = new Point(start.X + 1, start.Y + 1);
                        }
                        else
                        {
                            start = new Point(start.X + 1, start.Y - 1);
                        }
                    }
                    else
                    {
                        if (start.Y < End.Y)
                        {
                            start = new Point(start.X - 1, start.Y + 1);
                        }
                        else
                        {
                            start = new Point(start.X - 1, start.Y - 1);
                        }
                    }

                    points.Add(start);
                }
            }

            return points;
        }
    }
}