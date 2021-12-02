namespace Advent2021.Inputs
{
    internal static class PuzzleInput
    {
        public static IEnumerable<string> Get(string fileName)
        {
            return File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), $"inputs\\{fileName}"));
        }
    }
}
