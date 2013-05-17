// ********************************
// <copyright file="BattleFieldConsole.cs" company="Gadolinium">
// Copyright (c) 2013 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************

using System.Collections.Generic;

namespace BattleFieldGame
{
    class ExplosionOne : Explosion
    {
        /// <summary>
        /// Field is filled with given mine explosion of type one.
        /// </summary>
        /// <param name="field">Given field.</param>
        /// <param name="mine">Given mine.</param>
        public override void Explode(char[,] field, Mine mine)
        {
            List<Mine> hitPositions = new List<Mine>
            {
                new Mine(mine.Row - 1, mine.Col - 1),
                new Mine(mine.Row - 1, mine.Col + 1),
                new Mine(mine.Row + 1, mine.Col - 1),
                new Mine(mine.Row + 1, mine.Col + 1),
                new Mine(mine.Row, mine.Col)
            };

            GameFieldServices.MineHits(field, hitPositions);
        }
    }
}
