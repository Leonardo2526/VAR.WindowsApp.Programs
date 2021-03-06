using System;
using System.Linq;
using HPASharp.Infrastructure;

namespace HPASharp
{
    public partial class Program
    {
        public class ExamplePassability : IPassability
        {
            private string map = @"
                    0000000000000000000100000000000000000000
                    0111111111111111111111111111111111111110
                    0000000000000000000100000000000000000000
                    0000000000000000000100000000000000000000
                    0000000000000000000100000000000000000000
                    0000000000000000000100000000000000000000
                    0000000000000000000100000000000000000000
                    0001000000000000000100000000000000000000
                    0001000000000000000100000000000000000000
                    0001000000000000000100000000000000000000
                    0001000000000000000100000000000000000000
                    0001000000000000000100000000000000000000
                    0001000000000000000100000000000000000000
                    0000000000000000000100000000000000000000
                    0000000000000000000100000000000000000000
                    0000000000000000000100000000000000000000
                    0000000000000000000100000000000000000000
                    0000000000000000000100000000000000000000
                    0000000000000000000100000000000000000000
                    0111111111111111111100000000000000000000
                    0000000000000000000100000000000000000000
                    0000000000000000000100000000000000000000
                    0000000000000000000100000000000000000000
                    0000000000000000000100000000000000000000
                    0000000000000000000100000000000000000000
                    0000000000000000000100000000000000000000
                    0000000000000000000100000000000000000000
                    0000000000000000000100000000000000000000
                    0000000000000000000100000000000000000000
                    0000000000000000000100000000000000000000
                    0000000000000000000100000000000000000000
                    0000000000000000000100000000000000000000
                    0000000000000000000100000000000000000000
                    0000000000000000000100000000000000000000
                    0000000000000000000100000000000000000000
                    0000000000000000000100000000000000000000
                    0000000000000000000100000000000000000000
                    0000000000000000000100000000000000000000
                    0111111111111111111111111111111111111110
                    0000000000000000000000000000000000000000
                ";

            public ExamplePassability()
            {
                obstacles = new bool[40, 40];
                var charlines = map.Split('\n')
                    .Select(line => line.Trim())
                    .Where(line => !string.IsNullOrWhiteSpace(line))
                    .Select(line => line.ToCharArray()).ToArray();
                for(int j = 0; j < obstacles.GetLength(1); j++)
                    for (int i = 0; i < obstacles.GetLength(0); i++)
                    {
                        obstacles[i, j] = charlines[i][j] == '1';
                    }
            }

            Random rnd = new Random(700);

            public Position GetRandomFreePosition()
            {
                var x = rnd.Next(40);
                var y = rnd.Next(40);
                while (obstacles[x, y])
                {
                    x = rnd.Next(40);
                    y = rnd.Next(40);
                }

                return new Position(x, y);
            }
            
            private bool[,] obstacles;

            public bool CanEnter(Position pos, out int cost)
            {
                cost = Constants.COST_ONE;
                return !obstacles[pos.Y, pos.X];
            }
        }
    }
}