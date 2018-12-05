using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixLib.Visitor
{
    /// <summary>
    /// Provides methods for operation with different kinds of square matrixes
    /// </summary>
    public static class MatrixExtension
    {
        /// <summary>
        /// Represents add operation to any square matrixes
        /// </summary>
        /// <typeparam name="T">The type of objects include in the matrix.</typeparam>
        /// <param name="firstMatrix">The first matrix to add</param>
        /// <param name="secondMatrix">The second matrix to add</param>
        /// <returns>Matrix is an result of sum operation</returns>
        public static Martix<T> Add<T>(this Martix<T> firstMatrix, Martix<T> secondMatrix)
        {
            Martix<T> resultMatrix;
            try
            {
                resultMatrix = (Martix<T>)Add<T>((dynamic)firstMatrix, (dynamic)secondMatrix);
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException ex)
            {
                throw new NotSupportedException($"Operation is not defined", ex);
            }

            return resultMatrix;
        }

        private static SquareMatrix<T> Add<T>(SquareMatrix<T> firstMatrix, SquareMatrix<T> secondMatrix)
        {
            CheckMatrixes<T>(firstMatrix, secondMatrix);
            SquareMatrix<T> result = new SquareMatrix<T>(firstMatrix.Size);
            for (int i = 0; i < firstMatrix.Size; i++)
            {
                for (int j = 0; j < firstMatrix.Size; j++)
                {
                    result[i, j] = (T)((dynamic)firstMatrix[i, j] + secondMatrix[i, j]);
                }
            }

            return result;
        }

        private static DiagonalMatrix<T> Add<T>(DiagonalMatrix<T> firstMatrix, DiagonalMatrix<T> secondMatrix)
        {
            CheckMatrixes<T>(firstMatrix, secondMatrix);
            DiagonalMatrix<T> result = new DiagonalMatrix<T>(firstMatrix.Size);
            for (int i = 0; i < firstMatrix.Size; i++)
            {
                result[i, i] = (T)((dynamic)firstMatrix[i, i] + secondMatrix[i, i]);
            }

            return result;
        }

        private static SymmetricMatrix<T> Add<T>(SymmetricMatrix<T> firstMatrix, SymmetricMatrix<T> secondMatrix)
        {
            CheckMatrixes<T>(firstMatrix, secondMatrix);
            SymmetricMatrix<T> result = new SymmetricMatrix<T>(firstMatrix.Size);
            for (int i = 0; i < firstMatrix.Size; i++)
            {
                for (int j = 0; j < firstMatrix.Size; j++)
                {
                    result[i, j] = (T)((dynamic)firstMatrix[i, j] + secondMatrix[i, j]);
                }
            }

            return result;
        }

        private static SquareMatrix<T> Add<T>(SquareMatrix<T> firstMatrix, DiagonalMatrix<T> secondMatrix)
        {
            CheckMatrixes<T>(firstMatrix, secondMatrix);
            SquareMatrix<T> result = new SquareMatrix<T>(firstMatrix.Size);
            SumEachElement<T>(firstMatrix, secondMatrix, result);

            return result;
        }

        private static SquareMatrix<T> Add<T>(SquareMatrix<T> firstMatrix, SymmetricMatrix<T> secondMatrix)
        {
            CheckMatrixes<T>(firstMatrix, secondMatrix);
            SquareMatrix<T> result = new SquareMatrix<T>(firstMatrix.Size);
            SumEachElement<T>(firstMatrix, secondMatrix, result);

            return result;
        }

        private static SymmetricMatrix<T> Add<T>(SymmetricMatrix<T> firstMatrix, DiagonalMatrix<T> secondMatrix)
        {
            CheckMatrixes<T>(firstMatrix, secondMatrix);
            SymmetricMatrix<T> result = new SymmetricMatrix<T>(firstMatrix.Size);
            SumEachElement<T>(firstMatrix, secondMatrix, result);

            return result;
        }     

        private static void SumEachElement<T>(dynamic firstMatrix, dynamic secondMatrix, Martix<T> resultMatrix)
        {           
            for (int i = 0; i < firstMatrix.Size; i++)
            {
                for (int j = 0; j < firstMatrix.Size; j++)
                {
                    resultMatrix[i, j] = (T)(firstMatrix[i, j] + secondMatrix[i, j]);
                }
            }           
        }

        private static void CheckMatrixes<T>(Martix<T> firstMatrix, Martix<T> secondMatrix)
        {
            if (firstMatrix == null)
            {
                throw new ArgumentNullException($"Matrix {nameof(firstMatrix)} has incorrect value");
            }

            if (secondMatrix == null)
            {
                throw new ArgumentNullException($"Matrix {nameof(secondMatrix)} has incorrect value");
            }

            if (firstMatrix.Size != secondMatrix.Size)
            {
                throw new ArgumentException($"Matrixes {nameof(firstMatrix)} and {nameof(secondMatrix)} has incorrect size");
            }
        }
    }
}
