namespace BattleFieldGame
{
    using System;

    class Battlefield
    {
        private char[,] gameField;

        public Battlefield()
        {
            gameField = null;
        }

        public void Start()
        {
            Console.WriteLine(@"Welcome to ""Battle Field"" game. ");
            int size = 0;
            string readBuffer = null;

            Console.Write("Enter battle field size: n=");
            readBuffer = Console.ReadLine();

            while (!int.TryParse(readBuffer, out size) || size > 10 || size <= 0)
            {
                Console.WriteLine("Wrong format!");
                Console.Write("Enter battle field size: n=");
                readBuffer = Console.ReadLine();
            }

            gameField = GameServices.CreateField(size);
            StartInteraction();
        }

        private void StartInteraction()
        {
            string readBuffer = null;
            int blownMines = 0;
            Console.WriteLine();

            while (GameServices.AreMinesLeft(gameField))
            {
                string stringifiedField = GameServices.StringifyField(gameField);
                Console.WriteLine(stringifiedField);
                Mine mineCoordinates;
                do
                {
                    Console.Write("Please enter coordinates: ");
                    readBuffer = Console.ReadLine();
                    mineCoordinates = GameServices.ExtractMineFromString(readBuffer);
                }
                while (mineCoordinates == null);

                if (GameServices.IsValidMove(gameField, mineCoordinates.Row, mineCoordinates.Col))
                {
                    GameServices.DestroyFieldCells(gameField, mineCoordinates);
                    blownMines++;
                }
                else
                {
                    Console.WriteLine("Invalid move!");
                }
            }

            string stringifiedFieldEnd = GameServices.StringifyField(gameField);
            Console.WriteLine(stringifiedFieldEnd);
            Console.WriteLine("Game over. Detonated mines: {0}", blownMines);
        }
    }
}