using Advent2021.Inputs;
using System.Drawing;

namespace Advent2021.Days
{
    internal static class Day02
    {
        public static int Part1()
        {
            var inputs = PuzzleInput.Get("Day02.txt");

            var position = new Point();

            foreach (var input in inputs)
            {
                var instruction = new Instruction(input);

                switch (instruction.Direction)
                {
                    case "forward":
                        position.X = position.X + instruction.Value;
                        break;
                    case "up":
                        position.Y = position.Y - instruction.Value;
                        break;
                    case "down":
                        position.Y = position.Y + instruction.Value;
                        break;
                }
            }

            return position.X * position.Y;
        }

        public static int Part2()
        {
            var inputs = PuzzleInput.Get("Day02.txt");

            var position = new Point();

            var aim = 0;

            foreach (var input in inputs)
            {
                var instruction = new Instruction(input);

                switch (instruction.Direction)
                {
                    case "forward":
                        position.X = position.X + instruction.Value;
                        position.Y = position.Y + (aim * instruction.Value);
                        break;
                    case "up":
                        aim = aim - instruction.Value;
                        break;
                    case "down":
                        aim = aim + instruction.Value;
                        break;
                }
            }

            return position.X * position.Y;
        }
    }

    internal class Instruction
    {
        public Instruction(string instrunction)
        {
            var parse = instrunction.Split(" ");

            Direction = parse[0];
            Value = Convert.ToInt32(parse[1]);
        }

        public int Value { get; set; }
        public string Direction { get; set; }
    }
}
