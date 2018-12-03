using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixLib.Visitor
{
    public class SumMatrixVisitor<T> : MatrixVisitor<T>
    {
        public SquareMatrix<T> resultMatrix;

        private readonly Func<T, T, T> condtion;

        public SumMatrixVisitor(Func<T, T, T> condtion)
        {
            this.condtion = condtion ?? throw new ArgumentNullException($"Condition {nameof(condtion)} has null value");
        }

        protected override void Visit(SquareMatrix<T> firstSquareMatrix, SquareMatrix<T> secondSquareMatrix)
            => this.Sum(firstSquareMatrix, secondSquareMatrix);

        protected override void Visit(DiagonalMatrix<T> firstDiagonalMatrix, DiagonalMatrix<T> secondDiagonalMatrix)
            => this.Sum(firstDiagonalMatrix, secondDiagonalMatrix);

        protected override void Visit(SymmetricMatrix<T> firstSymmetricMatrix, SymmetricMatrix<T> secondDiagonalMatrix)
            => this.Sum(firstSymmetricMatrix, secondDiagonalMatrix);

        protected override void Visit(SquareMatrix<T> firstMatrix, DiagonalMatrix<T> secondMatrix)
           => this.Sum(firstMatrix, secondMatrix);

        protected override void Visit(SquareMatrix<T> firstMatrix, SymmetricMatrix<T> secondMatrix)
            => this.Sum(firstMatrix, secondMatrix);

        protected override void Visit(DiagonalMatrix<T> firstMatrix, SymmetricMatrix<T> secondMatrix)
        {
            this.Sum(firstMatrix, secondMatrix);
        }

        private void Sum(Martix<T> firstMatrix, Martix<T> secondMatrix)
        {
            if (CheckMatrixes(firstMatrix, secondMatrix))
            {
                throw new ArgumentException($"Matrixes {nameof(firstMatrix)}, {nameof(secondMatrix)} are not conformed");
            }

            resultMatrix = new SquareMatrix<T>(firstMatrix.Size);
            for (int i = 0; i < firstMatrix.Size; i++)
            {
                for (int j = 0; j < firstMatrix.Size; j++)
                {
                    this.resultMatrix[i, j] = this.condtion(firstMatrix[i, j], secondMatrix[i, j]);
                }
            }
        }

        private bool CheckMatrixes(Martix<T> firstMatrix, Martix<T> secondMatrix)
        => firstMatrix.Size == secondMatrix.Size;
    }
}
