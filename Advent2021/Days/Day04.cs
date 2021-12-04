using Advent2021.Inputs;
using System.Drawing;

namespace Advent2021.Days
{
    internal static class Day04
    {
        private static IEnumerable<string>? _puzzleInput;

        public static void Part1()
        {
            _puzzleInput = PuzzleInput.Get("Day04.txt");

            var boards = GetBingoBoards();

            BingoBoard? winningBoard = null;

            var currentDraw = 0;

            foreach (var draw in _puzzleInput.First().Split(","))
            {
                currentDraw = Convert.ToInt32(draw);

                foreach (var board in boards)
                {
                    board.DabBoard(currentDraw);

                    if (board.IsWinner())
                    {
                        winningBoard = board;

                        break;
                    }
                }

                if (winningBoard != null)
                {
                    break;
                }
            }

            var unDabbed = winningBoard?.Board.Where(b => b.Value.IsDabbed == false).Select(b => b.Value.Value);

            if (unDabbed != null)
            {
                Console.WriteLine(unDabbed.Sum() * currentDraw);
            }
        }

        public static void Part2()
        {
            _puzzleInput = PuzzleInput.Get("Day04.txt");

            var boards = GetBingoBoards();

            BingoBoard? winningBoard = null;

            var currentDraw = 0;

            foreach (var draw in _puzzleInput.First().Split(","))
            {
                currentDraw = Convert.ToInt32(draw);

                foreach (var board in boards)
                {
                    if (board.WinningNumber == 0)
                    {
                        board.DabBoard(currentDraw);
                        board.IsWinner();
                    }
                }
            }

            boards = boards.OrderBy(board => board.WonAt).ToList();

            var lastBoard = boards.Last();

            var unDabbed = lastBoard.Board.Where(b => b.Value.IsDabbed == false).Select(b => b.Value.Value);

            if (unDabbed != null)
            {
                Console.WriteLine(unDabbed.Sum() * lastBoard.WinningNumber);
            }
        }

        private static List<BingoBoard> GetBingoBoards()
        {
            var boards = new List<BingoBoard>();

            var currentBoard = new BingoBoard();
            var row = 0;

            foreach (var input in _puzzleInput)
            {
                if (input.Contains(","))
                {
                    continue;
                }

                if (string.IsNullOrWhiteSpace(input))
                {
                    if (currentBoard.Board.Any())
                    {
                        boards.Add(currentBoard);

                        currentBoard = new BingoBoard();
                        row = 0;
                    }

                    continue;
                }

                var values = input.Split(" ");

                var index = 0;

                foreach (var value in values)
                {
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        continue;
                    }

                    currentBoard.Board.Add(new Point(row, index), new BingoBoardPosition(Convert.ToInt32(value)));

                    index++;
                }

                row++;
            }

            boards.Add(currentBoard);

            return boards;
        }
    }

    internal class BingoBoard
    {
        private const int _boardSize = 5;
        private int _currentDraw;

        public BingoBoard()
        {
            Board = new Dictionary<Point, BingoBoardPosition>();
        }

        public Dictionary<Point, BingoBoardPosition> Board { get; set; }
        public int WinningNumber { get; set; }
        public DateTime WonAt { get; set; }

        public void DabBoard(int currentDraw)
        {
            _currentDraw = currentDraw;
            Board.Where(b => b.Value.Value == currentDraw).ToList().ForEach(b => b.Value.IsDabbed = true);
        }

        public bool IsWinner()
        {
            for (int i = 0; i < _boardSize; i++)
            {
                var row = Board.Where(board => board.Key.X == i).ToList();

                if (!row.Any(position => !position.Value.IsDabbed))
                {
                    WeHaveAWinner();

                    return true;
                }

                var column = Board.Where(board => board.Key.Y == i).ToList();

                if (!column.Any(position => !position.Value.IsDabbed))
                {
                    WeHaveAWinner();

                    return true;
                }
            }

            return false;
        }

        private void WeHaveAWinner()
        {
            WinningNumber = _currentDraw;
            WonAt = DateTime.Now;
        }
    }

    internal class BingoBoardPosition
    {
        public BingoBoardPosition(int value)
        {
            Value = value;
        }

        public int Value { get; }
        public bool IsDabbed { get; set; }
    }
}
