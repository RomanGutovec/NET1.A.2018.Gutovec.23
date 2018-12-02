using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixLib
{
    public class SquareMatrix<T> : Martix<T>
    {
        #region Constructors
        public SquareMatrix(int size) : base(size, size)
        {
        }

        public SquareMatrix(int rowsNumber, T[,] elements) : base(rowsNumber, rowsNumber, elements)
        {            
        }
        #endregion
    }
}
