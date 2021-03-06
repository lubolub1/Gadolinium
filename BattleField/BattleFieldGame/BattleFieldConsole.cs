﻿// ********************************
// <copyright file="BattleFieldConsole.cs" company="Gadolinium">
// Copyright (c) 2013 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************

namespace BattleFieldGame
{
    using System;
    using System.Text;

    /// <summary>
    /// Class responsible for console interface input and output.
    /// </summary>
    public class BattleFieldConsole
    {
        #region Fields
        
        /// <summary>
        /// Represents a game field.
        /// </summary>
        private char[,] gameField;
        
        #endregion
        
        #region Constructors
        
        /// <summary>
        /// Initializes a new instance of the <see cref="BattleFieldConsole"/> class.
        /// </summary>
        public BattleFieldConsole()
        {
            this.gameField = null;
        }
        
        #endregion
        
        #region Methods
        
        /// <summary>
        /// Run BattleFieldGame using console.
        /// </summary>
        public void StartGame()
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
            
            this.gameField = GameFieldServices.CreateField(size);
            this.StartInteraction();
        }
        
        /// <summary>
        /// Method which transform a field in a string the required form.
        /// </summary>
        /// <param name="field">Given field.</param>
        /// <returns>Returns string.</returns>
        public string StringifyField(char[,] field)
        {
            StringBuilder fieldStringify = new StringBuilder();
            int size = field.GetLength(0);
            
            fieldStringify.Append("   ");
            
            for (int i = 0; i < size; i++)
            {
                fieldStringify.AppendFormat("{0} ", i);
            }
            
            fieldStringify.Append(Environment.NewLine);
            fieldStringify.Append("   ");
            
            for (int i = 0; i < size * 2; i++)
            {
                fieldStringify.Append("-");
            }
            
            fieldStringify.Append(Environment.NewLine);
            
            for (int i = 0; i < size; i++)
            {
                fieldStringify.AppendFormat("{0} |", i);
                
                for (int j = 0; j < size; j++)
                {
                    fieldStringify.AppendFormat("{0} ", field[i, j]);
                }
                
                fieldStringify.Append(Environment.NewLine);
            }
            
            return fieldStringify.ToString();
        }
        
        /// <summary>
        /// From a given string extract mine.
        /// </summary>
        /// <param name="line">Given string.</param>
        /// <returns>Return a mine.</returns>
        public Mine ExtractMineFromString(string line)
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
        
        /// <summary>
        /// Responsible for users interactions.
        /// </summary>
        private void StartInteraction()
        {
            string readBuffer = null;
            int blownMines = 0;
            Console.WriteLine();
            
            while (GameFieldServices.AreMinesLeft(this.gameField))
            {
                string stringifiedField = this.StringifyField(this.gameField);
                Console.WriteLine(stringifiedField);
                Mine mineCoordinates;
                do
                {
                    Console.WriteLine("Please enter coordinates of a bomb.");
                    Console.Write("Two digits separated by space: ");
                    readBuffer = Console.ReadLine();
                    mineCoordinates = this.ExtractMineFromString(readBuffer);
                }
                while (mineCoordinates == null);
                
                if (GameFieldServices.IsValidMove(this.gameField, mineCoordinates.Row, mineCoordinates.Col))
                {
                    GameFieldServices.DestroyFieldCells(this.gameField, mineCoordinates);
                    blownMines++;
                }
                else
                {
                    Console.WriteLine("Invalid move!");
                }
            }
            
            string stringifiedFieldEnd = this.StringifyField(this.gameField);
            Console.WriteLine(stringifiedFieldEnd);
            Console.WriteLine("Game over. Your score is  {0} detonated mines: ", blownMines);
        }
    
        #endregion
    }
}