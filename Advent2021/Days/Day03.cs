using Advent2021.Inputs;

namespace Advent2021.Days
{
    internal static class Day03
    {
        private static IEnumerable<string>? _puzzleInput;

        public static void Part1()
        {
            var puzzleInput = PuzzleInput.Get("Day03.txt");

            var gamma = "";
            var epsilon = "";

            var total = puzzleInput.Count();

            for (var i = 0; i < puzzleInput.First().Length; i++)
            {
                var chars = puzzleInput.Select(x => x[i]);

                if (chars.Count(c => c == '0') > chars.Count(c => c == '1'))
                {
                    gamma += "0";
                    epsilon += "1";
                }
                else
                {
                    gamma += "1";
                    epsilon += "0";
                }
            }

            Console.WriteLine(Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2));
        }

        public static async Task Part2()
        {
            _puzzleInput = PuzzleInput.Get("Day03.txt");

            var o2Task = GetRating(_puzzleInput.ToList(), isOxygen: true);
            var co2Task = GetRating(_puzzleInput.ToList(), isOxygen: false);

            await Task.WhenAll(new Task[] { o2Task, co2Task });

            Console.WriteLine(o2Task.Result * co2Task.Result);
        }

        private static async Task<int> GetRating(List<string> diagnosticReport, bool isOxygen)
        {
            return await Task.Run(() =>
            {
                for (var i = 0; i < _puzzleInput?.First().Length; i++)
                {
                    if (diagnosticReport.Count() == 1)
                    {
                        break;
                    }

                    var chars = diagnosticReport.Select(x => x[i]);

                    if (isOxygen)
                    {
                        if (chars.Count(c => c == '0') > chars.Count(c => c == '1'))
                        {
                            diagnosticReport = diagnosticReport.Where(x => x[i] == '0').ToList();
                        }
                        else
                        {
                            diagnosticReport = diagnosticReport.Where(x => x[i] == '1').ToList();
                        }
                    }
                    else
                    {
                        if (chars.Count(c => c == '1') < chars.Count(c => c == '0'))
                        {
                            diagnosticReport = diagnosticReport.Where(x => x[i] == '1').ToList();
                        }
                        else
                        {
                            diagnosticReport = diagnosticReport.Where(x => x[i] == '0').ToList();
                        }
                    }
                }

                return Convert.ToInt32(diagnosticReport.Single(), 2);
            });
        }
    }
}
