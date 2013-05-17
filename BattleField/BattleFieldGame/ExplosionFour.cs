// ********************************
// <copyright file="BattleFieldConsole.cs" company="Gadolinium">
// Copyright (c) 2013 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************

namespace BattleFieldGame
{
    class ExplosionFour : Explosion
    {
        /// <summary>
        /// Field is filled with given mine explosion of type four.
        /// </summary>
        /// <param name="field">Given field.</param>
        /// <param name="mine">Given mine.</param>
        public override void Explode(char[,] field, Mine mine)
        {
            for (int row = mine.Row - 2; row <= mine.Row + 2; row++)
            {
                for (int col = mine.Col - 2; col <= mine.Col + 2; col++)
                {
                    bool upperDoubleRight = (row == mine.Row - 2) && (col == mine.Col - 2);
                    bool upperDoubleLeft = (row == mine.Row - 2) && (col == mine.Col + 2);
                    bool downDoubleRight = (row == mine.Row + 2) && (col == mine.Col - 2);
                    bool downDoubleLeft = (row == mine.Row + 2) && (col == mine.Col + 2);

                    if (GameFieldServices.AreCordinatesInAField(field, row, col) && !upperDoubleRight && !upperDoubleLeft &&
                        !downDoubleRight && !downDoubleLeft)
                    {
                        field[row, col] = GameFieldServices.DESTROYED_SYMBOL;
                    }
                }
            }
        }
    }
}
