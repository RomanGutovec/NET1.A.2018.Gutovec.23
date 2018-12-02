using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixLib.Visitor
{
    public static class MatrixExtension
    {
        public static Martix<T> GetSumMatrix<T>(this Martix<T> matrix, Martix<T> secondMatrix, Func<T, T, T> condition)
        {
            var visitor = new SumMatrixVisitor<T>(condition);
            visitor.DynamicVisit(matrix, secondMatrix);
            return visitor.resultMatrix;
        }
    }
}
