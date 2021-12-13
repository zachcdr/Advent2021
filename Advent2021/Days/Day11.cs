using Advent2021.Inputs;
using System.Drawing;

namespace Advent2021.Days
{
    internal static class Day11
    {
        private static IEnumerable<string>? _puzzleInput;

        public static void Part1()
        {
            _puzzleInput = PuzzleInput.Get("Day11.txt");

            var octopuses = new List<Octopus>();

            var y = 0;

            foreach (var input in _puzzleInput)
            {
                var x = 0;

                foreach (var i in input)
                {
                    octopuses.Add(new Octopus(int.Parse(i.ToString()), new Point(x, y)));

                    x++;
                }

                y++;
            }

            var lightShow = new OctopusLightShow(octopuses);

            lightShow.DoSteps();

            Console.WriteLine(lightShow.Flashes);
        }

        public static void Part2()
        {
            _puzzleInput = PuzzleInput.Get("Day11.txt");

            var octopuses = new List<Octopus>();

            var y = 0;

            foreach (var input in _puzzleInput)
            {
                var x = 0;

                foreach (var i in input)
                {
                    octopuses.Add(new Octopus(int.Parse(i.ToString()), new Point(x, y)));

                    x++;
                }

                y++;
            }

            var lightShow = new OctopusLightShow(octopuses);

            lightShow.DoSteps(doPart2: true);

            Console.WriteLine(lightShow.Flashes);
        }
    }

    internal class OctopusLightShow
    {
        private List<Octopus> _octopuses;

        public int Flashes { get; private set; }

        public OctopusLightShow(List<Octopus> octopuses)
        {
            _octopuses = octopuses;
        }

        public void DoSteps(bool doPart2 = false)
        {
            for (var i = 0; i < (doPart2 ? 100000: 100); i++)
            {
                Flashes += Step(i);
            }
        }

        public int Step(int step)
        {
            var hasFlashed = new List<Point>();

            _octopuses.ForEach(x => x.Charge++);

            var emitting = true;

            var flashesThisStep = 0;
            while (emitting)
            {
                var newFlashes = _octopuses.Where(x => x.Charge > 9).Select(x => x.Location).ToList();

                flashesThisStep += newFlashes.Count;

                if (flashesThisStep == _octopuses.Count)
                {
                    Console.WriteLine(step + 1);
                    Console.ReadKey();
                }

                hasFlashed.AddRange(newFlashes);

                _octopuses.ForEach(o =>
                {
                    if (o.Charge > 9)
                    {
                        o.Charge = 0;
                    }
                });

                var surroundings = GetSurrounding(newFlashes);

                foreach (var flash in hasFlashed)
                {
                    if (surroundings.ContainsKey(flash))
                    {
                        surroundings.Remove(flash);
                    }
                }

                if (surroundings.Count == 0)
                {
                    emitting = false;
                    continue;
                }

                foreach (var kvp in surroundings)
                {
                    _octopuses.Single(o => o.Location == kvp.Key).Charge += kvp.Value;
                }
            }

            return hasFlashed.Count;
        }

        private Dictionary<Point, int> GetSurrounding(List<Point> justFlashed)
        {
            var surrounding = new Dictionary<Point, int>();

            foreach (var point in justFlashed)
            {
                var points = GetSurrounding(point);

                foreach (var p in points)
                {
                    if (!justFlashed.Contains(p))
                    {
                        if (surrounding.ContainsKey(p))
                        {
                            surrounding[p]++;
                        }
                        else
                        {
                            surrounding.Add(p, 1);
                        }
                    }
                }
            }

            return surrounding;
        }

        private List<Point> GetSurrounding(Point point)
        {
            var points = new List<Point>();

            var top = GetTop(point);
            var bottom = GetBottom(point);
            var right = GetRight(point);
            var left = GetLeft(point);
            var topLeft = GetTopLeft(point);
            var topRight = GetTopRight(point);
            var bottomleft = GetBottomLeft(point);
            var bottomRight = GetBottomRight(point);

            if (top != null)
            {
                points.Add((Point)top);
            }
            if (bottom != null)
            {
                points.Add((Point)bottom);
            }
            if (right != null)
            {
                points.Add((Point)right);
            }
            if (left != null)
            {
                points.Add((Point)left);
            }

            if (topLeft != null)
            {
                points.Add((Point)topLeft);
            }
            if (topRight != null)
            {
                points.Add((Point)topRight);
            }
            if (bottomleft != null)
            {
                points.Add((Point)bottomleft);
            }
            if (bottomRight != null)
            {
                points.Add((Point)bottomRight);
            }

            return points;
        }

        private Point? GetTop(Point point)
        {
            return _octopuses.FirstOrDefault(x => x.Location == new Point(point.X, point.Y + 1))?.Location;
        }

        private Point? GetBottom(Point point)
        {
            return _octopuses.FirstOrDefault(x => x.Location == new Point(point.X, point.Y - 1))?.Location;
        }

        private Point? GetRight(Point point)
        {
            return _octopuses.FirstOrDefault(x => x.Location == new Point(point.X + 1, point.Y))?.Location;
        }

        private Point? GetLeft(Point point)
        {
            return _octopuses.FirstOrDefault(x => x.Location == new Point(point.X - 1, point.Y))?.Location;
        }

        private Point? GetTopLeft(Point point)
        {
            return _octopuses.FirstOrDefault(x => x.Location == new Point(point.X - 1, point.Y + 1))?.Location;
        }

        private Point? GetTopRight(Point point)
        {
            return _octopuses.FirstOrDefault(x => x.Location == new Point(point.X + 1, point.Y + 1))?.Location;
        }

        private Point? GetBottomLeft(Point point)
        {
            return _octopuses.FirstOrDefault(x => x.Location == new Point(point.X - 1, point.Y - 1))?.Location;
        }

        private Point? GetBottomRight(Point point)
        {
            return _octopuses.FirstOrDefault(x => x.Location == new Point(point.X + 1, point.Y - 1))?.Location;
        }
    }

    internal class Octopus
    {
        public Octopus(int charge, Point location)
        {
            Charge = charge;
            Location = location;
        }

        public int Charge { get; set; }
        public Point Location { get; set; }
    }
}
