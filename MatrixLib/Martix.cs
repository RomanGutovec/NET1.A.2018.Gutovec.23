using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixLib
{
    /// <summary>
    /// Represents class which includes types in the square matrix with specified size
    /// </summary>
    /// <typeparam name="T">The type of objects include in the matrix.</typeparam>
    public abstract class Martix<T> : IEnumerable<T>
    {
        #region Constructors
        /// <summary>
        /// Initialize fields of the Matrix class  
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when size less or equal to 0 </exception>
        /// <param name="size">Amount of rows and columns</param>
        public Martix(int size)
        {
            if (size <= 0)
            {
                throw new ArgumentNullException($"Amount of rows {nameof(size)} has incorrect value");
            }

            this.Size = size;
        }
        #endregion

        #region Events
        /// <summary>
        /// Represents event when data of the matrix changed
        /// </summary>
        public event EventHandler<MatrixChangedEventArgs<T>> MatrixChanged = delegate { };
        #endregion

        #region Properties
        /// <summary>
        /// Returns size of the matrix
        /// </summary>
        public int Size { get; private set; }
        #endregion

        #region Indexator
        /// <summary>
        /// Returns element of the matrix by chosen indices
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when one of any row or columns has incorrect value</exception>
        /// <param name="rowIndex">Index of row to get element</param>
        /// <param name="columnIndex">Index of row to set element</param>
        /// <returns>Element of the matrix by chosen indices</returns>
        public abstract T this[int rowIndex, int columnIndex] { get; set; }
        #endregion

        #region Implementations
        /// <summary>
        /// Returns an enumerator that iterates through the matrix.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the matrix.</returns>
        public abstract IEnumerator<T> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Set value of element by the chosen row and columns indices
        /// </summary>
        /// <param name="rowIndex">Index of the row in matrix</param>
        /// <param name="columnIndex">Index of the columns in matrix</param>
        /// <param name="value">Value to set</param>
        public abstract void SetValueByIndices(int rowIndex, int columnIndex, T value);

        protected virtual void OnMatrixChanged(MatrixChangedEventArgs<T> e)
        {
            MatrixChanged?.Invoke(this, e);
        }

        protected void CheckIndeces(int rowIndex, int columnIndex)
        {
            if (rowIndex >= Size || rowIndex < 0)
            {
                throw new ArgumentException($"Index of row {nameof(rowIndex)} has incorrect value");
            }

            if (columnIndex >= Size || columnIndex < 0)
            {
                throw new ArgumentException($"Index of column {nameof(columnIndex)} has incorrect value");
            }
        }
        #endregion
    }
}
