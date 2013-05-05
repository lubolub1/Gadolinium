namespace BattleField
{
    using System;
    using System.Collections.Generic;

    public class GameServices
    {
        private static readonly Random rand = new Random();
        private const double LOWER_MINES_COUNT = 0.15;
        private const double UPPER_MINES_COUNT = 0.3;
        private const char FIELD_SYMBOL = '-';
        private const char DESTROYED_SYMBOL = 'X';



        public static char[,] GreateField(int size)
        {
            char[,] field = new char[size, size];

            FullUpFieldWithSymbol(field);
            FullUpFieldWithMines(field);

            return field;
        }

        private static void FullUpFieldWithMines(char[,] field)
        {
            List<Mine> mines = new List<Mine>();
            int size = field.GetLength(0);
            int minesCount = DetermineMinesCount(size);

            for (int i = 0; i < minesCount; i++)
            {
                int mineRow = rand.Next(0, size);
                int mineCol = rand.Next(0, size);
                Mine newMine = new Mine(mineRow, mineCol);

                if (!GameServices.ListContainsMine(mines, newMine))
                {
                    mines.Add(newMine);

                    int mineType = rand.Next('1', '6');
                    field[mineRow, mineCol] = Convert.ToChar(mineType);
                }

                else
                {
                    minesCount++;
                }
            }
        }

        private static void FullUpFieldWithSymbol(char[,] field)
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
            int lowerMinesCount = (int)(LOWER_MINES_COUNT * fieldSize);
            int upperMinesCount = (int)(UPPER_MINES_COUNT * fieldSize);
            int minesCount = rand.Next(lowerMinesCount, upperMinesCount);

            return minesCount;
        }

        private static bool ListContainsMine(List<Mine> mines, Mine mine)
        {
            foreach (Mine nextMine in mines)
            {
                if (nextMine.Row == mine.Row && nextMine.Col == mine.Col)
                {
                    return true;
                }
            }

            return false;
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

        private static void MineHits(char[,] field, List<Mine> minesHits)
        {
            foreach (var mineHit in minesHits)
            {
                if (AreCordinatesInAField(field, mineHit.Row, mineHit.Col))
                {
                    field[mineHit.Row, mineHit.Col] = DESTROYED_SYMBOL;
                }
            }
        }

        private static void ExpolosionTypeOne(char[,] field, Mine mine)
        {
            List<Mine> minesHits = new List<Mine>{
                new Mine(mine.Row - 1, mine.Col - 1 ) ,
                 new Mine(mine.Row - 1,mine.Col + 1),
               new Mine(mine.Row + 1, mine.Col - 1), 
                new Mine(mine.Row + 1,mine.Col + 1 )};

            MineHits(field, minesHits);
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
            List<Mine> minesHits = new List<Mine>{
                new Mine(mine.Row - 2, mine.Col ) ,
                 new Mine(mine.Row +2,mine.Col),
               new Mine(mine.Row , mine.Col - 2), 
                new Mine(mine.Row,mine.Col + 2 )};

            MineHits(field, minesHits);
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
                    
                    if (AreCordinatesInAField(field, row, col)&&!upperDoubleRight&&!upperDoubleLeft&&
                        !downDoubleRight&&!downDoubleLeft)
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
            char mineType = field[mine.Row, mine.Col];

            switch (mineType)
            {
                case '1':
                    {
                        ExpolosionTypeOne(field, mine);
                    }
                    break;
                case '2':
                    {
                        ExplosionTypeTwo(field, mine);
                    }
                    break;
                case '3':
                    {
                        ExplosionTypeThree(field, mine);
                    }
                    break;
                case '4':
                    {
                        ExplosionTypeFour(field, mine);
                    }
                    break;
                case '5':
                    {
                        ExplosionTypeFive(field, mine);
                    }
                    break;
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

        public static void ShowFiledOnConsole(char[,] field)
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
                Console.WriteLine("Invalid index!");
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

            return new Mine(x,y);
        }
    }
}
