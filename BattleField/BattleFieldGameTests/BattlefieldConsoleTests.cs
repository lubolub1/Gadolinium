namespace BattleFieldGameTests
{
    using BattleFieldGame;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class BattlefieldConsoleTests
    {
        [TestMethod]
        public void ExtractMineFromStringCorrectInputTest()
        {
            string test = "1 1";
            Mine correctMine = new Mine(1, 1);
            BattleFieldConsole testGame = new BattleFieldConsole();
            Mine testedMine = testGame.ExtractMineFromString(test);

            Assert.IsTrue(correctMine.Col == testedMine.Col && correctMine.Row == testedMine.Row);
        }

        [TestMethod]
        public void ExtractMineFromStringIncorrectInputTest1()
        {
            string test = "a 1";
            BattleFieldConsole testGame = new BattleFieldConsole();
            Assert.IsNull(testGame.ExtractMineFromString(test));
        }

        [TestMethod]
        public void ExtractMineFromStringIncorrectInputTest2()
        {
            string test = "1 a";
            BattleFieldConsole testGame = new BattleFieldConsole();
            Assert.IsNull(testGame.ExtractMineFromString(test));
        }

        [TestMethod]
        public void ExtractMineFromStringIncorrectInputNullTest()
        {
            string test = null;
            BattleFieldConsole testGame = new BattleFieldConsole();
            Assert.IsNull(testGame.ExtractMineFromString(test));
        }

        [TestMethod]
        public void StringifyFieldTest()
        {
            char[,] testedField = new char[,]
            {
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL},
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL},
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,'1',GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL},
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL},
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL}
            };
            string expectedResult = "   0 1 2 3 4 " + Environment.NewLine +
                "   ----------" + Environment.NewLine +
                "0 |- - - - - " + Environment.NewLine +
                "1 |- - - - - " + Environment.NewLine +
                "2 |- - 1 - - " + Environment.NewLine +
                "3 |- - - - - " + Environment.NewLine +
                "4 |- - - - - " + Environment.NewLine;

            BattleFieldConsole testGame = new BattleFieldConsole();
            Assert.AreEqual(expectedResult, testGame.StringifyField(testedField));
        }
    }
}
