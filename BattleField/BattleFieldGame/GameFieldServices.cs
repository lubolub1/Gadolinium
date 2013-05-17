// ********************************
// <copyright file="GameFieldServices.cs" company="Gadolinium">
// Copyright (c) 2013 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************

namespace BattleFieldGame
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents GameFieldServices class who includes all methods
    /// needed for playing the game.
    /// </summary>
    public class GameFieldServices
    {
        #region Fields
        
        /// <summary>
        /// Represents minimal mines count in percentages.
        /// </summary>
        private const double LOWER_MINES_COUNT = 0.15;
        
        /// <summary>
        /// Represents maximal mines count in percentages.
        /// </summary>
        private const double UPPER_MINES_COUNT = 0.3;
        
        /// <summary>
        /// Represents empty cell on the field.
        /// </summary>
        public const char FIELD_SYMBOL = '-';
        
        /// <summary>
        /// Represents destroyed cell on the field.
        /// </summary>
        public const char DESTROYED_SYMBOL = 'X';
        
        /// <summary>
        /// Represents random numbers generator.
        /// </summary>
        private static readonly Random Rand = new Random();
        
        #endregion
        
        #region Methods
        
        /// <summary>
        /// Create a game field from given size.
        /// </summary>
        /// <param name="size">Given size</param>
        /// <returns>returns the created field in char two dimension array.</returns>
        public static char[,] CreateField(int size)
        {
            char[,] field = new char[size, size];
            
            FillFieldWithDefaultSymbol(field);
            PlaceMines(field);
            
            return field;
        }
        
        /// <summary>
        /// Locate mines in a given field. Mines count is between 15% and 30%
        /// of the field size. The mines and located in random positions.
        /// </summary>
        /// <param name="field">Given Field</param>
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
        
        /// <summary>
        /// Field is filled with the default symbol.
        /// </summary>
        /// <param name="field">Given field.</param>
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
        
        /// <summary>
        /// Determine mines count from default min and max border.
        /// </summary>
        /// <param name="size">Size of the field</param>
        /// <returns>returns the determined numbers of mines</returns>
        private static int DetermineMinesCount(int size)
        {
            int fieldSize = size * size;
            int lowerMinesCount = (int)Math.Ceiling(LOWER_MINES_COUNT * fieldSize);
            int upperMinesCount = (int)Math.Ceiling(UPPER_MINES_COUNT * fieldSize);
            int minesCount = Rand.Next(lowerMinesCount, upperMinesCount);
            
            if (size == 1)
            {
                minesCount--;
            }
            
            return minesCount;
        }
        
        /// <summary>
        /// Check if there are mines on the field.
        /// </summary>
        /// <param name="field">Given field.</param>
        /// <returns>Returns are there mines in the field or not.</returns>
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
        
        /// <summary>
        /// Check if given coordinates are in the field.
        /// </summary>
        /// <param name="field">Given field.</param>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        /// <returns>Returns are the coordinates in the field or not.</returns>
        public static bool AreCordinatesInAField(char[,] field, int x, int y)
        {
            if (x < 0 || y < 0 || x >= field.GetLength(0) || y >= field.GetLength(1))
            {
                return false;
            }
            
            return true;
        }
        
        /// <summary>
        /// Fill field with destroy symbols when curtain cells are hit by a mine.
        /// </summary>
        /// <param name="field">Given field</param>
        /// <param name="hitPositions">List containing cell which must be destroyed.</param>
        public static void MineHits(char[,] field, List<Mine> hitPositions)
        {
            foreach (var hitPosition in hitPositions)
            {
                if (AreCordinatesInAField(field, hitPosition.Row, hitPosition.Col))
                {
                    field[hitPosition.Row, hitPosition.Col] = DESTROYED_SYMBOL;
                }
            }
        }
                     
        /// <summary>
        /// Method which determine mine type of explosion.
        /// </summary>
        /// <param name="field">Given field.</param>
        /// <param name="mine">Given mine.</param>
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
                    new ExplosionOne().Explode(field, mine);
                    break;
                case ExplosionType.Two:
                    new ExplosionTwo().Explode(field, mine);
                    break;
                case ExplosionType.Three:
                    new ExplosionThree().Explode(field, mine);
                    break;
                case ExplosionType.Four:
                    new ExplosionFour().Explode(field, mine);
                    break;
                case ExplosionType.Five:
                    new ExplosionFive().Explode(field, mine);
                    break;
                default:
                    throw new NotImplementedException("This type of explosion is not supported yet.");
            }
        }
        
        /// <summary>
        /// Check if the user given coordinates are valid move.
        /// </summary>
        /// <param name="field">Given field.</param>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        /// <returns>Returns is valid move or not.</returns>
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
    
        #endregion
    }
}