// ********************************
// <copyright file="Engine.cs" company="Gadolinium">
// Copyright (c) 2013 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************

namespace BattleFieldGame
{
    /// <summary>
    /// Class used for starting the game
    /// </summary>
    public class Engine
    {
        /// <summary>
        /// Create instance of BattleFieldConsole and run it.
        /// </summary>
        public static void Main()
        {
            BattleFieldConsole game = new BattleFieldConsole();
            game.StartGame();
        }
    }
}