using Advent2021.Inputs;

namespace Advent2021.Days
{
    internal static class Day12
    {
        private static IEnumerable<string>? _puzzleInput;

        public static void Part1()
        {
            _puzzleInput = PuzzleInput.Get("Day12.txt");

            var starts = _puzzleInput.Where(x => x.StartsWith("start"));

            var paths = new List<Route>();

            foreach (var start in starts)
            {
                var search = start.Split("-")[1];

                var isSmallCave = search == search.ToLower();

                paths.Add(new Route(search, isSmallCave ? new List<string>() { search } : new List<string>()));

                while (true)
                {
                    paths = GetPaths(paths).ToList();

                    if (!paths.Any(p => !p.IsComplete))
                    {
                        break;
                    }
                }
            }

            Console.WriteLine(paths.Count);
        }

        private static IEnumerable<Route> GetPaths(IEnumerable<Route> paths)
        {
            var newPaths = new List<Route>();

            foreach (var path in paths)
            {
                if (path.IsComplete)
                {
                    newPaths.Add(path);
                    continue;
                }

                var search = path.Path.Split(",").Last();

                var nextSpots = _puzzleInput.Where(p => p.Split("-").Any(s => s == search));

                foreach (var nextSpot in nextSpots)
                {
                    var options = nextSpot.Split("-");

                    var toCheck = options.First(o => o != search);

                    if (toCheck == "start")
                    {
                        continue;
                    }

                    var isBigCave = toCheck == toCheck.ToUpper();

                    var smallCaves = new List<string>();

                    smallCaves.AddRange(path.SmallCaves);

                    if (!isBigCave)
                    {
                        if (path.SmallCaves.Contains(toCheck))
                        {
                            continue;
                        }
                        else
                        {
                            smallCaves.Add(toCheck);
                        }
                    }

                    newPaths.Add(new Route(path.Path + "," + toCheck, smallCaves));
                }
            }

            return newPaths;
        }

        public static void Part2()
        {
            _puzzleInput = PuzzleInput.Get("Day12.txt");

            var starts = _puzzleInput.Where(x => x.StartsWith("start"));

            var paths = new List<Route>();

            foreach (var start in starts)
            {
                var search = start.Split("-")[1];

                var isSmallCave = search == search.ToLower();

                paths.Add(new Route(search, isSmallCave ? new List<string>() { search } : new List<string>()));

                while (true)
                {
                    paths = GetPathsPart2(paths).ToList();

                    if (!paths.Any(p => !p.IsComplete))
                    {
                        break;
                    }
                }
            }

            Console.WriteLine(paths.Count);
        }

        private static IEnumerable<Route> GetPathsPart2(IEnumerable<Route> paths)
        {
            var newPaths = new List<Route>();

            foreach (var path in paths)
            {
                if (path.IsComplete)
                {
                    newPaths.Add(path);
                    continue;
                }

                var search = path.Path.Split(",").Last();

                var nextSpots = _puzzleInput.Where(p => p.Split("-").Any(s => s == search));

                foreach (var nextSpot in nextSpots)
                {
                    var options = nextSpot.Split("-");

                    var toCheck = options.First(o => o != search);

                    if (toCheck == "start")
                    {
                        continue;
                    }

                    var isBigCave = toCheck == toCheck.ToUpper();

                    var smallCaves = new List<string>();

                    smallCaves.AddRange(path.SmallCaves);

                    var isMax = false;

                    if (!isBigCave)
                    {
                        if (path.SmallCaves.Contains(toCheck))
                        {
                            if (path.IsSmallVisitMax)
                            {
                                continue;
                            }
                            else
                            {
                                isMax = true;
                            }
                        }
                        else
                        {
                            smallCaves.Add(toCheck);
                        }
                    }

                    newPaths.Add(new Route(path.Path + "," + toCheck, smallCaves, isMax ? isMax : path.IsSmallVisitMax));
                }
            }

            return newPaths;
        }
    }

    internal class Route
    {
        public Route(string path, List<string> caves, bool isMax = false)
        {
            Path = path;
            SmallCaves = caves;
            IsSmallVisitMax = isMax;
        }

        public string Path { get; private set; }
        public List<string> SmallCaves { get; private set; }
        public bool IsComplete => Path.EndsWith("end");
        public bool IsSmallVisitMax { get; set; }
    }
}
