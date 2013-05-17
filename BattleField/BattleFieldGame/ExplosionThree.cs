// ********************************
// <copyright file="BattleFieldConsole.cs" company="Gadolinium">
// Copyright (c) 2013 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************

using System.Collections.Generic;

namespace BattleFieldGame
{
    class ExplosionThree : Explosion
    {
        /// <summary>
        /// Field is filled with given mine explosion of type three.
        /// </summary>
        /// <param name="field">Given field.</param>
        /// <param name="mine">Given mine.</param>
        public override void Explode(char[,] field, Mine mine)
        {
            List<Mine> hitPositions = new List<Mine>
            {
                new Mine(mine.Row - 2, mine.Col),
                new Mine(mine.Row + 2, mine.Col),
                new Mine(mine.Row, mine.Col - 2),
                new Mine(mine.Row, mine.Col + 2)
            };

            GameFieldServices.MineHits(field, hitPositions);
            new ExplosionTwo().Explode(field, mine);
        }
    }
}
