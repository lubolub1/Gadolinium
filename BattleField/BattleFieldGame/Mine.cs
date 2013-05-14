// ********************************
// <copyright file="Mine.cs" company="Gadolinium">
// Copyright (c) 2013 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************


namespace BattleFieldGame
{
    using System;

    /// <summary>
    /// Two dimension mine representation.
    /// </summary>
    public class Mine
    {
        #region Fields

        /// <summary>
        /// X(row) coordinate.
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// Y(col) coordinate.
        /// </summary>
        public int Col { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Mine"/> class.
        /// </summary>
        /// <param name="row">X(row) coordinate.</param>
        /// <param name="col">Y(col) coordinate.</param>
        public Mine(int row, int col)
        { 
            this.Row = row;
            this.Col = col;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Set equal method.
        /// </summary>
        /// <param name="obj">Compared object</param>
        /// <returns>Returs are the two objects equal.</returns>
        public override bool Equals(object obj)
        {
            Mine mine = obj as Mine;
            if (mine == null)
            {
                return false;
            }
            return this.Row == mine.Row && this.Col == mine.Col;
        }
                
        /// <summary>
        /// Set new GetHashCodeMethod.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return 11 * this.Row + this.Col;
        }
        #endregion
    }
}