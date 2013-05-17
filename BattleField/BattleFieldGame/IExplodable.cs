// ********************************
// <copyright file="BattleFieldConsole.cs" company="Gadolinium">
// Copyright (c) 2013 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************

namespace BattleFieldGame
{
    interface IExplodable
    {
        void Explode(char[,] field, Mine mine);
    }
}
