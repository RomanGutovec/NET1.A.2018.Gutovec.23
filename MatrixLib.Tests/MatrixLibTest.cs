using System;
using MatrixLib.Visitor;
using NUnit.Framework;

namespace MatrixLib.Tests
{
    [TestFixture]
    public class MatrixLibTest
    {
        [Test]
        public void SetValueByIndicesTest_InputArrayfrom1to9AndFirstAndLastSpecifiedWith555()
        {
            int[,] firstArray = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            var actualMatrix = new SquareMatrix<int>(3, firstArray);

            actualMatrix.SetValueByIndices(0, 0, 555);
            actualMatrix.SetValueByIndices(2, 2, 555);

            int[,] expectedArray = { { 555, 2, 3 }, { 4, 5, 6 }, { 7, 8, 555 } };

            CollectionAssert.AreEqual(expectedArray, actualMatrix);
        }

        [Test]
        public void InsexatorTest_InputArrayfrom1to9AndFirstAndLastSpecifiedWith555()
        {
            int[,] firstArray = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            var actualMatrix = new SquareMatrix<int>(3, firstArray);

            actualMatrix.SetValueByIndices(0, 0, 555);
            actualMatrix.SetValueByIndices(2, 2, 555);

            Assert.AreEqual(5, actualMatrix[1, 1]);
            Assert.AreEqual(555, actualMatrix[2, 2]);
        }

        [Test]
        public void InsexatorTest_InputIncorrectIndex_()
        => Assert.Throws<ArgumentException>(() =>
        {
            int[,] firstArray = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            var actualMatrix = new SquareMatrix<int>(3, firstArray);
            var number = actualMatrix[3, 2];
        });

        [Test]
        public void SetValueByIndices_InputIncorrectIndex_ThrownArgumentException()
            => Assert.Throws<ArgumentException>(() =>
                {
                    int[,] firstArray = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
                    var actualMatrix = new SquareMatrix<int>(3, firstArray);

                    actualMatrix.SetValueByIndices(0, 0, 555);
                    actualMatrix.SetValueByIndices(-1, 2, 555);
                });

        [Test]
        public void SumMatrixesTest_InputTwoMatrixes_AssertExpectedAndActualResult()
        {
            int[,] firstArray = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            var firstMatrix = new SquareMatrix<int>(3, firstArray);

            int[,] secondArray = { { 0, 0, 0 }, { -2, -2, -2 }, { 15, 15, 15 } };
            var secondMatrix = new SquareMatrix<int>(3, secondArray);

            int[,] expectedArray = { { 1, 2, 3 }, { 2, 3, 4 }, { 22, 23, 24 } };
            var expectedMatrix = new SquareMatrix<int>(3, expectedArray);

            SquareMatrix<int> actualMatrix;
            actualMatrix = (SquareMatrix<int>)firstMatrix.Add<int>(secondMatrix);
            CollectionAssert.AreEqual(expectedMatrix, actualMatrix);
        }

        [Test]
        public void SumMatrixesTest_InputSymmetricMatrixAndDiagonal_AssertExpectedAndActualResult()
        {
            int[,] firstArray = { { 1, 3, 0 }, { 3, 2, 6 }, { 0, 6, 5 } };
            Martix<int> firstMatrix = new SymmetricMatrix<int>(3, firstArray);

            int[] secondArray = { 1, 2, 5 };
            Martix<int> secondMatrix = new DiagonalMatrix<int>(3, secondArray);

            int[,] expectedArray = { { 2, 3, 0 }, { 3, 4, 6 }, { 0, 6, 10 } };
            var expectedMatrix = new SymmetricMatrix<int>(3, expectedArray);

            Martix<int> actualMatrix;
            actualMatrix = firstMatrix.Add<int>(secondMatrix);
            CollectionAssert.AreEqual(expectedMatrix, actualMatrix);
        }

        [Test]
        public void SumMatrixesTest_InputDiagonalMatrixAndDiagonal_AssertExpectedAndActualResult()
        {
            int[] firstArray = { 1, 2, 5 };
            Martix<int> firstMatrix = new DiagonalMatrix<int>(3, firstArray);

            int[] secondArray = { 1, 2, 5 };
            Martix<int> secondMatrix = new DiagonalMatrix<int>(3, secondArray);

            int[] expectedArray = { 2, 4, 10 };
            var expectedMatrix = new DiagonalMatrix<int>(3, expectedArray);

            DiagonalMatrix<int> actualMatrix;
            actualMatrix = (DiagonalMatrix<int>)firstMatrix.Add<int>(secondMatrix);
            CollectionAssert.AreEqual(expectedMatrix, actualMatrix);
        }

        [Test]
        public void ConstructorSymmetricTest_InputSymmEtricArrayfrom()
        {
            int[,] firstArray = { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } };
            var actualMatrix = new SymmetricMatrix<int>(3, firstArray);

            int[,] expectedArray = { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } };
            CollectionAssert.AreEqual(expectedArray, actualMatrix);
        }
    }
}
