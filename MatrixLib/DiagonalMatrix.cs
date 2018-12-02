using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixLib
{
    public class DiagonalMatrix<T> : Martix<T>
    {
        #region Constructors
        public DiagonalMatrix(int rowsNumber) : base(rowsNumber, rowsNumber)
        {
        }
        #endregion

        #region Constructors
        public DiagonalMatrix(IEnumerable<T> elements) : base(elements.Count(), elements.Count(), elements)
        {
            if (elements == null)
            {
                throw new ArgumentNullException($"Elements {nameof(elements)} has no necessary values");
            }

            T[] tempArrayToInitialize = new T[elements.Count()];
            elements.ToArray().CopyTo(tempArrayToInitialize, 0);
            int count = 0;
            while (count < tempArrayToInitialize.Length)
            {
                for (int i = 0; i < this.RowsNumber; i++)
                {
                    this.SetValueByIndices(i, i, tempArrayToInitialize[count++]);
                }
            }
        }

        public DiagonalMatrix(int rowsNumber, T[,] elements) : base(rowsNumber, rowsNumber)
        {
            if (elements == null)
            {
                throw new ArgumentNullException($"Elements {nameof(elements)} has no necessary values");
            }

            T[] tempArrayToInitialize = new T[elements.Length];
            elements.CopyTo(tempArrayToInitialize, 0);
            int count = 0;
            while (count < tempArrayToInitialize.Length)
            {
                for (int i = 0; i < rowsNumber; i++)
                {
                    this.SetValueByIndices(i, i, tempArrayToInitialize[count++]);
                }
            }
        }
        #endregion
    }
}
