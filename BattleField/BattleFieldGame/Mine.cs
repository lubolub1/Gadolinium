namespace BattleField
{
    public class Mine
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public Mine(int row, int col)
        {
            this.Row = row;
            this.Col = col;
        }
    }
}
