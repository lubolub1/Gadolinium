// ********************************
// <copyright file="BattleFieldConsole.cs" company="Gadolinium">
// Copyright (c) 2013 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************

namespace BattleFieldGame
{
    abstract class Explosion : IExplodable
    {
        public abstract void Explode(char[,] field, Mine mine);
    }
}
