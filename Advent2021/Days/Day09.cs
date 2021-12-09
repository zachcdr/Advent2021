using Advent2021.Inputs;
using System.Drawing;

namespace Advent2021.Days
{
    internal static class Day09
    {
        private static IEnumerable<string>? _puzzleInput;

        public static void Part1()
        {
            _puzzleInput = PuzzleInput.Get("Day09.txt");

            var heightMap = new Dictionary<Point, int>();

            var x = 0;
            var y = 0;

            foreach (var input in _puzzleInput)
            {
                x = 0;
                foreach (var i in input)
                {
                    heightMap.Add(new Point(x, y), int.Parse(i.ToString()));
                    x++;
                }

                y--;
            }

            var lows = new List<int>();

            var left = 10;
            var right = 10;
            var top = 10;
            var bottom = 10;

            foreach (var item in heightMap)
            {
                if (heightMap.ContainsKey(new Point(item.Key.X - 1, item.Key.Y)))
                {
                    left = heightMap[new Point(item.Key.X - 1, item.Key.Y)];
                }
                if (heightMap.ContainsKey(new Point(item.Key.X + 1, item.Key.Y)))
                {
                    right = heightMap[new Point(item.Key.X + 1, item.Key.Y)];
                }
                if (heightMap.ContainsKey(new Point(item.Key.X, item.Key.Y + 1)))
                {
                    top = heightMap[new Point(item.Key.X, item.Key.Y + 1)];
                }
                if (heightMap.ContainsKey(new Point(item.Key.X, item.Key.Y - 1)))
                {
                    bottom = heightMap[new Point(item.Key.X, item.Key.Y - 1)];
                }

                if (item.Value < left && item.Value < right && item.Value < top && item.Value < bottom)
                {
                    lows.Add(item.Value);
                }

                left = 10;
                right = 10;
                top = 10;
                bottom = 10;
            }

            Console.WriteLine(lows.Sum() + lows.Count);
        }

        public static void Part2()
        {
            _puzzleInput = PuzzleInput.Get("Day09.txt");

            var heightMap = new Dictionary<Point, int>();

            var x = 0;
            var y = 0;

            foreach (var input in _puzzleInput)
            {
                x = 0;
                foreach (var i in input)
                {
                    heightMap.Add(new Point(x, y), int.Parse(i.ToString()));
                    x++;
                }

                y--;
            }

            var lows = new List<Point>();

            var left = 10;
            var right = 10;
            var top = 10;
            var bottom = 10;

            foreach (var item in heightMap)
            {
                if (heightMap.ContainsKey(new Point(item.Key.X - 1, item.Key.Y)))
                {
                    left = heightMap[new Point(item.Key.X - 1, item.Key.Y)];
                }
                if (heightMap.ContainsKey(new Point(item.Key.X + 1, item.Key.Y)))
                {
                    right = heightMap[new Point(item.Key.X + 1, item.Key.Y)];
                }
                if (heightMap.ContainsKey(new Point(item.Key.X, item.Key.Y + 1)))
                {
                    top = heightMap[new Point(item.Key.X, item.Key.Y + 1)];
                }
                if (heightMap.ContainsKey(new Point(item.Key.X, item.Key.Y - 1)))
                {
                    bottom = heightMap[new Point(item.Key.X, item.Key.Y - 1)];
                }

                if (item.Value < left && item.Value < right && item.Value < top && item.Value < bottom)
                {
                    lows.Add(item.Key);
                }

                left = 10;
                right = 10;
                top = 10;
                bottom = 10;
            }

            var basins = new List<int>();

            foreach (var low in lows)
            {
                var basin = new Basin(low, heightMap);

                basin.Seach();

                basins.Add(basin.Total);
            }

            var winners = basins.OrderByDescending(x => x).Take(3).ToList();

            Console.WriteLine(winners[0] * winners[1] * winners[2]);
        }
    }

    internal class Basin
    {
        public int Total => _points.Count;

        private Dictionary<Point, int> _map;
        private Point _low;

        private List<Point> _points = new List<Point>();

        public Basin(Point low, Dictionary<Point, int> map)
        {
            _low = low;
            _map = map;
            _points.Add(low);
        }

        public void Seach()
        {
            var doSearch = true;

            var points = new List<Point>() { _low };

            while (doSearch)
            {
                points = GetSurroundingPoints(points);

                if (points.Count == 0)
                {
                    doSearch = false;
                }
                else
                {
                    _points.AddRange(points);
                }
            }
        }

        private List<Point> GetSurroundingPoints(List<Point> centers)
        {
            var points = new List<Point>();

            foreach (var center in centers)
            {
                if (_map.ContainsKey(new Point(center.X + 1, center.Y)))
                {
                    if (_map[new Point(center.X + 1, center.Y)] != 9 && _map[new Point(center.X + 1, center.Y)] > _map[center])
                    {
                        if (!_points.Contains(new Point(center.X + 1, center.Y)) && !points.Contains(new Point(center.X + 1, center.Y)))
                        {
                            points.Add(new Point(center.X + 1, center.Y));
                        }
                    }
                }

                if (_map.ContainsKey(new Point(center.X - 1, center.Y)))
                {
                    if (_map[new Point(center.X - 1, center.Y)] != 9 && _map[new Point(center.X - 1, center.Y)] > _map[center])
                    {
                        if (!_points.Contains(new Point(center.X - 1, center.Y)) && !points.Contains(new Point(center.X - 1, center.Y)))
                        {
                            points.Add(new Point(center.X - 1, center.Y));
                        }
                    }
                }

                if (_map.ContainsKey(new Point(center.X, center.Y + 1)))
                {
                    if (_map[new Point(center.X, center.Y + 1)] != 9 && _map[new Point(center.X, center.Y + 1)] > _map[center])
                    {
                        if (!_points.Contains(new Point(center.X, center.Y + 1)) && !points.Contains(new Point(center.X, center.Y + 1)))
                        {
                            points.Add(new Point(center.X, center.Y + 1)); 
                        }
                    }
                }

                if (_map.ContainsKey(new Point(center.X, center.Y - 1)))
                {
                    if (_map[new Point(center.X, center.Y - 1)] != 9 && _map[new Point(center.X, center.Y - 1)] > _map[center])
                    {
                        if (!_points.Contains(new Point(center.X, center.Y - 1)) && !points.Contains(new Point(center.X, center.Y - 1)))
                        {
                            points.Add(new Point(center.X, center.Y - 1)); 
                        }
                    }
                }

            }

            return points;
        }
    }
}
