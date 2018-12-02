using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixLib.Visitor
{
    public abstract class MatrixVisitor<T>
    {
        public void DynamicVisit(Martix<T> firstMatrix, Martix<T> secondMatrix)
        => Visit((dynamic)firstMatrix, (dynamic)secondMatrix);

        protected abstract void Visit(SquareMatrix<T> firstSquareMatrix, SquareMatrix<T> secondSquareMatrix);

        protected abstract void Visit(DiagonalMatrix<T> firstDiagonalMatrix, DiagonalMatrix<T> secondDiagonalMatrix);

        protected abstract void Visit(SymmetricMatrix<T> firstSymmetricMatrix, SymmetricMatrix<T> secondDiagonalMatrix);

        protected abstract void Visit(SquareMatrix<T> firstMatrix, DiagonalMatrix<T> secondMatrix);

        protected abstract void Visit(SquareMatrix<T> firstMatrix, SymmetricMatrix<T> secondMatrix);

        protected abstract void Visit(DiagonalMatrix<T> firstMatrix, SymmetricMatrix<T> secondMatrix);
    }
}
