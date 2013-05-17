// ********************************
// <copyright file="BattleFieldConsole.cs" company="Gadolinium">
// Copyright (c) 2013 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************

namespace BattleFieldGame
{
    class ExplosionTwo : Explosion
    {
        /// <summary>
        /// Field is filled with given mine explosion of type two.
        /// </summary>
        /// <param name="field">Given field.</param>
        /// <param name="mine">Given mine.</param>
        public override void Explode(char[,] field, Mine mine)
        {
            for (int i = mine.Row - 1; i <= mine.Row + 1; i++)
            {
                for (int j = mine.Col - 1; j <= mine.Col + 1; j++)
                {
                    if (GameFieldServices.AreCordinatesInAField(field, i, j))
                    {
                        field[i, j] = GameFieldServices.DESTROYED_SYMBOL;
                    }
                }
            }
        }
    }
}
