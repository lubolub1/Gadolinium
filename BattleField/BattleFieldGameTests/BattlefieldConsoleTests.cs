namespace BattleFieldGameTests
{
    using System;
    using BattleFieldGame;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        public void StringifyFieldTestOne()
        {
            char[,] testedField = new char[,]
            {
                { GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL },
                { GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL },
                { GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, '1', GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL },
                { GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL },
                { GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL }
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

        [TestMethod]
        public void StringifyFieldTestFive()
        {
            char[,] testedField = new char[,]
            {
                { '5', GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL },
                { GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL },
                { GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL },
                { GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL },
                { GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL }
            };
            string expectedResult = "   0 1 2 3 4 " + Environment.NewLine +
                                    "   ----------" + Environment.NewLine +
                                    "0 |5 - - - - " + Environment.NewLine +
                                    "1 |- - - - - " + Environment.NewLine +
                                    "2 |- - - - - " + Environment.NewLine +
                                    "3 |- - - - - " + Environment.NewLine +
                                    "4 |- - - - - " + Environment.NewLine;

            BattleFieldConsole testGame = new BattleFieldConsole();
            Assert.AreEqual(expectedResult, testGame.StringifyField(testedField));
        }

        [TestMethod]
        public void StringifyFieldTestMineTypeOneExplosion()
        {
            char[,] testedField = new char[,]
            {
                { GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL },
                { GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL },
                { GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, '1', GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL },
                { GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL },
                { GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL }
            };

            Mine mine = new Mine(2, 2);

            GameFieldServices.DestroyFieldCells(testedField, mine);

            string expectedResult = "   0 1 2 3 4 " + Environment.NewLine +
                                    "   ----------" + Environment.NewLine +
                                    "0 |- - - - - " + Environment.NewLine +
                                    "1 |- X - X - " + Environment.NewLine +
                                    "2 |- - X - - " + Environment.NewLine +
                                    "3 |- X - X - " + Environment.NewLine +
                                    "4 |- - - - - " + Environment.NewLine;

            BattleFieldConsole testGame = new BattleFieldConsole();
            Assert.AreEqual(expectedResult, testGame.StringifyField(testedField));
        }

        [TestMethod]
        public void StringifyFieldTestMineTypeTwoExplosion()
        {
            char[,] testedField = new char[,]
            {
                { GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL },
                { GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL },
                { GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, '2', GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL },
                { GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL },
                { GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL }
            };

            Mine mine = new Mine(2, 2);

            GameFieldServices.DestroyFieldCells(testedField, mine);

            string expectedResult = "   0 1 2 3 4 " + Environment.NewLine +
                                    "   ----------" + Environment.NewLine +
                                    "0 |- - - - - " + Environment.NewLine +
                                    "1 |- X X X - " + Environment.NewLine +
                                    "2 |- X X X - " + Environment.NewLine +
                                    "3 |- X X X - " + Environment.NewLine +
                                    "4 |- - - - - " + Environment.NewLine;

            BattleFieldConsole testGame = new BattleFieldConsole();
            Assert.AreEqual(expectedResult, testGame.StringifyField(testedField));
        }

        [TestMethod]
        public void StringifyFieldTestMineTypeThreeExplosion()
        {
            char[,] testedField = new char[,]
            {
                { GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL },
                { GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL },
                { GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, '3', GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL },
                { GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL },
                { GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL }
            };

            Mine mine = new Mine(2, 2);

            GameFieldServices.DestroyFieldCells(testedField, mine);

            string expectedResult = "   0 1 2 3 4 " + Environment.NewLine +
                                    "   ----------" + Environment.NewLine +
                                    "0 |- - X - - " + Environment.NewLine +
                                    "1 |- X X X - " + Environment.NewLine +
                                    "2 |X X X X X " + Environment.NewLine +
                                    "3 |- X X X - " + Environment.NewLine +
                                    "4 |- - X - - " + Environment.NewLine;

            BattleFieldConsole testGame = new BattleFieldConsole();
            Assert.AreEqual(expectedResult, testGame.StringifyField(testedField));
        }

        [TestMethod]
        public void StringifyFieldTestMineTypeFourExplosion()
        {
            char[,] testedField = new char[,]
            {
                { GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL },
                { GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL },
                { GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, '4', GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL },
                { GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL },
                { GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL }
            };

            Mine mine = new Mine(2, 2);

            GameFieldServices.DestroyFieldCells(testedField, mine);

            string expectedResult = "   0 1 2 3 4 " + Environment.NewLine +
                                    "   ----------" + Environment.NewLine +
                                    "0 |- X X X - " + Environment.NewLine +
                                    "1 |X X X X X " + Environment.NewLine +
                                    "2 |X X X X X " + Environment.NewLine +
                                    "3 |X X X X X " + Environment.NewLine +
                                    "4 |- X X X - " + Environment.NewLine;

            BattleFieldConsole testGame = new BattleFieldConsole();
            Assert.AreEqual(expectedResult, testGame.StringifyField(testedField));
        }

        [TestMethod]
        public void StringifyFieldTestMineTypeFiveExplosion()
        {
            char[,] testedField = new char[,]
            {
                { GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL },
                { GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL },
                { GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, '5', GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL },
                { GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL },
                { GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL }
            };

            Mine mine = new Mine(2, 2);

            GameFieldServices.DestroyFieldCells(testedField, mine);

            string expectedResult = "   0 1 2 3 4 " + Environment.NewLine +
                                    "   ----------" + Environment.NewLine +
                                    "0 |X X X X X " + Environment.NewLine +
                                    "1 |X X X X X " + Environment.NewLine +
                                    "2 |X X X X X " + Environment.NewLine +
                                    "3 |X X X X X " + Environment.NewLine +
                                    "4 |X X X X X " + Environment.NewLine;

            BattleFieldConsole testGame = new BattleFieldConsole();
            Assert.AreEqual(expectedResult, testGame.StringifyField(testedField));
        }

        [TestMethod]
        public void StringifyFieldTestMineComplexExplosion()
        {
            char[,] testedField = new char[,]
            {
                { '1', GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL },
                { GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL },
                { GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, '4', GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL },
                { GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL },
                { GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL, GameFieldServices.FIELD_SYMBOL }
            };

            Mine mine1 = new Mine(2, 2);
            Mine mine2 = new Mine(0, 0);

            GameFieldServices.DestroyFieldCells(testedField, mine1);
            GameFieldServices.DestroyFieldCells(testedField, mine2);

            string expectedResult = "   0 1 2 3 4 " + Environment.NewLine +
                                    "   ----------" + Environment.NewLine +
                                    "0 |X X X X - " + Environment.NewLine +
                                    "1 |X X X X X " + Environment.NewLine +
                                    "2 |X X X X X " + Environment.NewLine +
                                    "3 |X X X X X " + Environment.NewLine +
                                    "4 |- X X X - " + Environment.NewLine;

            BattleFieldConsole testGame = new BattleFieldConsole();
            Assert.AreEqual(expectedResult, testGame.StringifyField(testedField));
        }
    }
}