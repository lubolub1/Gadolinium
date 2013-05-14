namespace BattleFieldGame
{
    using System;
    using System.Collections.Generic;

    public class GameServices
    {
        public static readonly Random Rand = new Random();
        public const double LOWER_MINES_COUNT = 0.15;
        public const double UPPER_MINES_COUNT = 0.3;
        public const char FIELD_SYMBOL = '-';
        public const char DESTROYED_SYMBOL = 'X';

        public static char[,] CreateField(int size)
        {
            char[,] field = new char[size, size];

            FillFieldWithDefaultSymbol(field);
            PlaceMines(field);

            return field;
        }

        private static void PlaceMines(char[,] field)
        {
            List<Mine> mines = new List<Mine>();
            int size = field.GetLength(0);
            int minesCount = DetermineMinesCount(size);

            for (int i = 0; i < minesCount; i++)
            {
                int mineRow = Rand.Next(0, size);
                int mineCol = Rand.Next(0, size);
                Mine newMine = new Mine(mineRow, mineCol);

                if (!mines.Contains(newMine))
                {
                    mines.Add(newMine);

                    int mineType = Rand.Next('1', '6');
                    field[mineRow, mineCol] = Convert.ToChar(mineType);
                }
                else
                {
                    minesCount++;
                }
            }
        }

        private static void FillFieldWithDefaultSymbol(char[,] field)
        {
            int size = field.GetLength(0);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    field[i, j] = FIELD_SYMBOL;
                }
            }
        }

        private static int DetermineMinesCount(int size)
        {
            int fieldSize = size * size;
            int lowerMinesCount = (int)(Math.Ceiling(LOWER_MINES_COUNT * fieldSize));
            int upperMinesCount = (int)(Math.Ceiling(UPPER_MINES_COUNT * fieldSize));
            int minesCount = Rand.Next(lowerMinesCount, upperMinesCount);

            if (size == 1)
            {
                minesCount--;
            }

            return minesCount;
        }

        public static bool AreMinesLeft(char[,] field)
        {
            int size = field.GetLength(0);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (field[i, j] != FIELD_SYMBOL && field[i, j] != DESTROYED_SYMBOL)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool AreCordinatesInAField(char[,] field, int x, int y)
        {
            if (x < 0 || y < 0 || x >= field.GetLength(0) || y >= field.GetLength(1))
            {
                return false;
            }

            return true;
        }

        private static void MineHits(char[,] field, List<Mine> hitPositions)
        {
            foreach (var hitPosition in hitPositions)
            {
                if (AreCordinatesInAField(field, hitPosition.Row, hitPosition.Col))
                {
                    field[hitPosition.Row, hitPosition.Col] = DESTROYED_SYMBOL;
                }
            }
        }

        private static void ExpolosionTypeOne(char[,] field, Mine mine)
        {
            List<Mine> hitPositions = new List<Mine>
            {
                new Mine(mine.Row - 1, mine.Col - 1),
                new Mine(mine.Row - 1,mine.Col + 1),
                new Mine(mine.Row + 1, mine.Col - 1),
                new Mine(mine.Row + 1,mine.Col + 1),
                new Mine(mine.Row,mine.Col)
            };

            MineHits(field, hitPositions);
        }

        private static void ExplosionTypeTwo(char[,] field, Mine mine)
        {
            for (int i = mine.Row - 1; i <= mine.Row + 1; i++)
            {
                for (int j = mine.Col - 1; j <= mine.Col + 1; j++)
                {
                    if (AreCordinatesInAField(field, i, j))
                    {
                        field[i, j] = DESTROYED_SYMBOL;
                    }
                }
            }
        }

        private static void ExplosionTypeThree(char[,] field, Mine mine)
        {
            List<Mine> hitPositions = new List<Mine>
            {
                new Mine(mine.Row - 2, mine.Col),
                new Mine(mine.Row + 2,mine.Col),
                new Mine(mine.Row , mine.Col - 2),
                new Mine(mine.Row,mine.Col + 2)
            };

            MineHits(field, hitPositions);
            ExplosionTypeTwo(field, mine);
        }

        private static void ExplosionTypeFour(char[,] field, Mine mine)
        {
            for (int row = mine.Row - 2; row <= mine.Row + 2; row++)
            {
                for (int col = mine.Col - 2; col <= mine.Col + 2; col++)
                {
                    bool upperDoubleRight = (row == mine.Row - 2) && (col == mine.Col - 2);
                    bool upperDoubleLeft = (row == mine.Row - 2) && (col == mine.Col + 2);
                    bool downDoubleRight = (row == mine.Row + 2) && (col == mine.Col - 2);
                    bool downDoubleLeft = (row == mine.Row + 2) && (col == mine.Col + 2);

                    if (AreCordinatesInAField(field, row, col) && !upperDoubleRight && !upperDoubleLeft &&
                        !downDoubleRight && !downDoubleLeft)
                    {
                        field[row, col] = DESTROYED_SYMBOL;
                    }
                }
            }
        }

        private static void ExplosionTypeFive(char[,] field, Mine mine)
        {
            for (int i = mine.Row - 2; i <= mine.Row + 2; i++)
            {
                for (int j = mine.Col - 2; j <= mine.Col + 2; j++)
                {
                    if (AreCordinatesInAField(field, i, j))
                    {
                        field[i, j] = DESTROYED_SYMBOL;
                    }
                }
            }
        }

        public static void DestroyFieldCells(char[,] field, Mine mine)
        {
            string mineType = field[mine.Row, mine.Col].ToString();
            ExplosionType explosionType = ExplosionType.One;

            try
            {
                explosionType = (ExplosionType)Enum.Parse(typeof(ExplosionType), mineType);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("'{0}' is not a member of the ExplosionType enumeration.", mineType);
            }

            switch (explosionType)
            {
                case ExplosionType.One:
                    {
                        ExpolosionTypeOne(field, mine);
                        break;
                    }
                case ExplosionType.Two:
                    {
                        ExplosionTypeTwo(field, mine);
                        break;
                    }
                case ExplosionType.Three:
                    {
                        ExplosionTypeThree(field, mine);
                        break;
                    }
                case ExplosionType.Four:
                    {
                        ExplosionTypeFour(field, mine);
                        break;
                    }
                case ExplosionType.Five:
                    {
                        ExplosionTypeFive(field, mine);
                        break;
                    }
                default:
                    throw new NotImplementedException("This type of explosion is not supported yet.");
            }
        }

        public static bool IsValidMove(char[,] field, int x, int y)
        {
            if (!AreCordinatesInAField(field, x, y))
            {
                return false;
            }
            if (field[x, y] == DESTROYED_SYMBOL || field[x, y] == FIELD_SYMBOL)
            {
                return false;
            }

            return true;
        }

        public static void ShowFieldOnConsole(char[,] field)
        {
            Console.Write("   ");
            int size = field.GetLength(0);

            for (int i = 0; i < size; i++)
            {
                Console.Write("{0} ", i);
            }

            Console.WriteLine();
            Console.Write("   ");

            for (int i = 0; i < size * 2; i++)
            {
                Console.Write("-");
            }

            Console.WriteLine();

            for (int i = 0; i < size; i++)
            {
                Console.Write("{0} |", i);
                for (int j = 0; j < size; j++)
                {
                    Console.Write("{0} ", field[i, j]);
                }
                Console.WriteLine();
            }
        }

        public static Mine ExtractMineFromString(string line)
        {
            if (line == null || line.Length < 3 || !line.Contains(" "))
            {
                Console.WriteLine("Invalid input for indices!");
                return null;
            }

            string[] splited = line.Split(' ');

            int x = 0;
            int y = 0;

            if (!int.TryParse(splited[0], out x))
            {
                Console.WriteLine("Invalid index!");
                return null;
            }

            if (!int.TryParse(splited[1], out y))
            {
                Console.WriteLine("Invalid index!");
                return null;
            }

            return new Mine(x, y);
        }
    }
}