﻿using System;

namespace BattleField
{
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

            while (!int.TryParse(readBuffer, out size))
            {
                Console.WriteLine("Wrong format!");
                Console.Write("Enter battle field size: n=");
                readBuffer = Console.ReadLine();
            }

            if (size > 10 || size <= 0)
            {
                Console.WriteLine("Try number between 1 and 10!");
                Start(); 
            }
            else
            {
                gameField = GameServices.CreateField(size);
                StartInteraction();
            }
        }

        private void StartInteraction()
        {
            string readBuffer = null;
            int blownMines = 0;
            Console.WriteLine();            

            while (GameServices.AreMinesLeft(gameField))
            {
                GameServices.ShowFiledOnConsole(gameField);

                Console.Write("Please enter coordinates: ");
                readBuffer = Console.ReadLine();
                Mine mineCoordinates = GameServices.ExtractMineFromString(readBuffer);

                while (mineCoordinates == null)
                {
                    Console.Write("Please enter coordinates: ");
                    readBuffer = Console.ReadLine();
                    mineCoordinates = GameServices.ExtractMineFromString(readBuffer);
                }

                if (!GameServices.IsValidMove(gameField, mineCoordinates.Row, mineCoordinates.Col))
                {
                    Console.WriteLine("Invalid move!");
                    continue;
                }

                GameServices.DestroyFieldCells(gameField, mineCoordinates);
                blownMines++;
            }

            GameServices.ShowFiledOnConsole(gameField);
            Console.WriteLine("Game over. Detonated mines: {0}", blownMines);
        }
    }
}
