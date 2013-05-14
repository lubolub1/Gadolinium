using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BattleFieldGame;

namespace BattleFieldGameTests
{
    [TestClass]
    public class GameFieldServicesTests
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
            char[,] testedField = GameFieldServices.CreateField(5);
            Assert.IsTrue(testedField.GetLength(0) == 5 && testedField.GetLength(1) == 5);
        }

        [TestMethod]
        public void CreateFieldSizeFiveMinesCountTest()
        {
            char[,] testedField = GameFieldServices.CreateField(5);
            int minesCount = 0;

            for (int i = 0; i < testedField.GetLength(0); i++)
            {
                for (int j = 0; j < testedField.GetLength(1); j++)
                {
                    if (testedField[i, j] != GameFieldServices.FIELD_SYMBOL && testedField[i, j] != GameFieldServices.DESTROYED_SYMBOL)
                    {
                        minesCount++;
                    }
                }
            }

            Assert.IsTrue(3.75 <= minesCount && minesCount <= 7.5);
        }

        [TestMethod]
        public void CreateFieldSizeOneMinesCountTest()
        {
            char[,] testedField = GameFieldServices.CreateField(1);
            int minesCount = 0;

            for (int i = 0; i < testedField.GetLength(0); i++)
            {
                for (int j = 0; j < testedField.GetLength(1); j++)
                {
                    if (testedField[i, j] != GameFieldServices.FIELD_SYMBOL && testedField[i, j] != GameFieldServices.DESTROYED_SYMBOL)
                    {
                        minesCount++;
                    }
                }
            }

            Assert.IsTrue(minesCount == 0);
        }

        [TestMethod]
        public void CreateFieldSizeTenMinesCountTest()
        {
            char[,] testedField = GameFieldServices.CreateField(10);
            int minesCount = 0;

            for (int i = 0; i < testedField.GetLength(0); i++)
            {
                for (int j = 0; j < testedField.GetLength(1); j++)
                {
                    if (testedField[i, j] != GameFieldServices.FIELD_SYMBOL && testedField[i, j] != GameFieldServices.DESTROYED_SYMBOL)
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
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL},
            {GameFieldServices.FIELD_SYMBOL,'1'},
            };

            Assert.IsTrue(GameFieldServices.AreMinesLeft(testedField));
        }

        [TestMethod]
        public void AreMinesLeftFalseTest()
        {
            char[,] testedField = new char[,]{
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL},
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL},
            };

            Assert.IsFalse(GameFieldServices.AreMinesLeft(testedField));
        }

        [TestMethod]
        public void IsvalidMoveOutOfRangeTest()
        {
            char[,] testedField = new char[,]{
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL},
            {GameFieldServices.FIELD_SYMBOL,'1'},
            };

            Assert.IsFalse(GameFieldServices.IsValidMove(testedField, -1, 0));
        }

        [TestMethod]
        public void IsvalidMoveNotMineTest()
        {
            char[,] testedField = new char[,]{
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL},
            {GameFieldServices.FIELD_SYMBOL,'1'},
            };

            Assert.IsFalse(GameFieldServices.IsValidMove(testedField, 0, 0));
        }

        [TestMethod]
        public void IsvalidMoveMineTest()
        {
            char[,] testedField = new char[,]{
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL},
            {GameFieldServices.FIELD_SYMBOL,'1'},
            };

            Assert.IsTrue(GameFieldServices.IsValidMove(testedField, 1, 1));
        }        

        [TestMethod]
        public void ExposionTypeOneTest()
        {
            char[,] testedField = new char[,]{
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL},
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL},
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,'1',GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL},
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL},
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL}
            };
            char[,] expectedResultField = new char[,]{
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL},
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.FIELD_SYMBOL},
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL},
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.FIELD_SYMBOL},
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL}
            };
            Mine mine = new Mine(2, 2);

            GameFieldServices.DestroyFieldCells(testedField, mine);

            Assert.IsTrue(EqualityCharArrayCheck(testedField, expectedResultField));
        }

        private static bool EqualityCharArrayCheck(char[,] testedField, char[,] expectedResultField)
        {
            for (int i = 0; i < testedField.GetLength(0); i++)
            {
                for (int j = 0; j < testedField.GetLength(1); j++)
                {
                    if (testedField[i, j] != expectedResultField[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        [TestMethod]
        public void ExposionTypeTwoTest()
        {
            char[,] testedField = new char[,]{
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL},
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL},
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,'2',GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL},
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL},
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL}
            };
            char[,] expectedResultField = new char[,]{
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL},
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.FIELD_SYMBOL},
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.FIELD_SYMBOL},
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.FIELD_SYMBOL},
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL}
            };
            Mine mine = new Mine(2, 2);

            GameFieldServices.DestroyFieldCells(testedField, mine);

            Assert.IsTrue(EqualityCharArrayCheck(testedField, expectedResultField));
        }

        [TestMethod]
        public void ExposionTypeThreeTest()
        {
            char[,] testedField = new char[,]{
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL},
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL},
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,'3',GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL},
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL},
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL}
            };
            char[,] expectedResultField = new char[,]{
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL},
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.FIELD_SYMBOL},
            {GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL},
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.FIELD_SYMBOL},
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL}
            };
            Mine mine = new Mine(2, 2);

            GameFieldServices.DestroyFieldCells(testedField, mine);

            Assert.IsTrue(EqualityCharArrayCheck(testedField, expectedResultField));
        }

        [TestMethod]
        public void ExposionTypeFourTest()
        {
            char[,] testedField = new char[,]{
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL},
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL},
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,'4',GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL},
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL},
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL}
            };
            char[,] expectedResultField = new char[,]{
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.FIELD_SYMBOL},
            {GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL},
            {GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL},
            {GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL},
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.FIELD_SYMBOL}
            };
            Mine mine = new Mine(2, 2);

            GameFieldServices.DestroyFieldCells(testedField, mine);

            Assert.IsTrue(EqualityCharArrayCheck(testedField, expectedResultField));
        }

        [TestMethod]
        public void ExposionTypeFiveTest()
        {
            char[,] testedField = new char[,]{
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL},
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL},
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,'5',GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL},
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL},
            {GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL,GameFieldServices.FIELD_SYMBOL}
            };
            char[,] expectedResultField = new char[,]{
            {GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL},
            {GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL},
            {GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL},
            {GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL},
            {GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL,GameFieldServices.DESTROYED_SYMBOL}
            };
            Mine mine = new Mine(2, 2);

            GameFieldServices.DestroyFieldCells(testedField, mine);

            Assert.IsTrue(EqualityCharArrayCheck(testedField, expectedResultField));
        }
    }
}
