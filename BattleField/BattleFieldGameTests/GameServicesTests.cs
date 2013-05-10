using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BattleField;

namespace BattleFieldGameTests
{
    [TestClass]
    public class GameServicesTests
    {
        [TestMethod]
        public void CheckMineConstructor()
        {
            Mine mine = new Mine(5, 6);
            Assert.IsTrue(mine.Row == 5 && mine.Col == 6);
        }

        [TestMethod]
        public void CreateFieldDimensionsWithFiveTest()
        {
           char[,] testedField = GameServices.CreateField(5);
           Assert.IsTrue(testedField.GetLength(0) == 5 &&testedField.GetLength(1) == 5);
        }

        [TestMethod]
        public void CreateFieldSizeFiveMinesCountTest()
        {
            char[,] testedField = GameServices.CreateField(5);
            int minesCount = 0;

            for (int i = 0; i < testedField.GetLength(0); i++)
            {
                for (int j = 0; j < testedField.GetLength(1); j++)
                {
                    if (testedField[i,j]!=GameServices.FIELD_SYMBOL&&testedField[i,j]!=GameServices.DESTROYED_SYMBOL)
                    {
                        minesCount++;
                    }
                }
            }

            Assert.IsTrue(3.75<=minesCount&&minesCount<=7.5);
        }

        [TestMethod]
        public void CreateFieldSizeOneMinesCountTest()
        {
            char[,] testedField = GameServices.CreateField(1);
            int minesCount = 0;

            for (int i = 0; i < testedField.GetLength(0); i++)
            {
                for (int j = 0; j < testedField.GetLength(1); j++)
                {
                    if (testedField[i, j] != GameServices.FIELD_SYMBOL && testedField[i, j] != GameServices.DESTROYED_SYMBOL)
                    {
                        minesCount++;
                    }
                }
            }

            Assert.IsTrue(minesCount==0);
        }

        [TestMethod]
        public void CreateFieldSizeTenMinesCountTest()
        {
            char[,] testedField = GameServices.CreateField(10);
            int minesCount = 0;

            for (int i = 0; i < testedField.GetLength(0); i++)
            {
                for (int j = 0; j < testedField.GetLength(1); j++)
                {
                    if (testedField[i, j] != GameServices.FIELD_SYMBOL && testedField[i, j] != GameServices.DESTROYED_SYMBOL)
                    {
                        minesCount++;
                    }
                }
            }

            Assert.IsTrue(15 <= minesCount && minesCount <= 30);
        }

        [TestMethod]
        public void AreMinesLeftTrueTest()
        {
            char[,] testedField = new char[,]{
            {GameServices.FIELD_SYMBOL,GameServices.FIELD_SYMBOL},
            {GameServices.FIELD_SYMBOL,'1'},
            };

            Assert.IsTrue(GameServices.AreMinesLeft(testedField));
        }

        [TestMethod]
        public void AreMinesLeftFalseTest()
        {
            char[,] testedField = new char[,]{
            {GameServices.FIELD_SYMBOL,GameServices.FIELD_SYMBOL},
            {GameServices.FIELD_SYMBOL,GameServices.FIELD_SYMBOL},
            };

            Assert.IsFalse(GameServices.AreMinesLeft(testedField));
        }

        [TestMethod]
        public void IsvalidMoveOutOfRangeTest()
        {
            char[,] testedField = new char[,]{
            {GameServices.FIELD_SYMBOL,GameServices.FIELD_SYMBOL},
            {GameServices.FIELD_SYMBOL,'1'},
            };

            Assert.IsFalse(GameServices.IsValidMove(testedField,-1,0));
        }

        [TestMethod]
        public void IsvalidMoveNotMineTest()
        {
            char[,] testedField = new char[,]{
            {GameServices.FIELD_SYMBOL,GameServices.FIELD_SYMBOL},
            {GameServices.FIELD_SYMBOL,'1'},
            };

            Assert.IsFalse(GameServices.IsValidMove(testedField, 0, 0));
        }

        [TestMethod]
        public void IsvalidMoveMineTest()
        {
            char[,] testedField = new char[,]{
            {GameServices.FIELD_SYMBOL,GameServices.FIELD_SYMBOL},
            {GameServices.FIELD_SYMBOL,'1'},
            };

            Assert.IsTrue(GameServices.IsValidMove(testedField, 1, 1));
        }

        [TestMethod]
        public void ExtractMineFromStringCorrectInputTest()
        {
            string test = "1 1";
            Mine correctMine = new Mine(1, 1);
            Mine testedMine=GameServices.ExtractMineFromString(test);

            Assert.IsTrue(correctMine.Col == testedMine.Col && correctMine.Row == testedMine.Row);
        }

        [TestMethod]
        public void ExtractMineFromStringIncorrectInputTest()
        {
            string test = "a 1";            

            Assert.IsNull(GameServices.ExtractMineFromString(test));
        }
    }
}
