namespace BattleFieldGame
{
    using System;

    public class Mine
    {
        public int Row { get; set; }

        public int Col { get; set; }

        public Mine(int row, int col)
        { 
            this.Row = row;
            this.Col = col;
        }

        public override bool Equals(object obj)
        {
            Mine mine = obj as Mine;
            if (mine == null)
            {
                return false;
            }
            return this.Row == mine.Row && this.Col == mine.Col;
        }

        // just for the warning
        public override int GetHashCode()
        {
            return 11 * this.Row + this.Col;
        }
    }
}