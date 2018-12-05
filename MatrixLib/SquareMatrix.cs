using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixLib
{
    /// <summary>
    /// Represents a strongly typed square matrix of objects that can be accessed by index. Provides
    /// methods to set by indices.
    /// </summary>
    /// <typeparam name="T">The type of objects include in the matrix.</typeparam>
    public class SquareMatrix<T> : Martix<T>
    {
        #region Fields
        private T[,] elements;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the SquareMatrix class that
        /// with specified size. 
        /// </summary>
        /// /// <exception cref="ArgumentNullException">Thrown when size less or equal to 0 </exception>
        /// <param name="size">Amount of rows and columns</param>
        public SquareMatrix(int size) : base(size)
        {
            elements = new T[size, size];
        }

        /// <summary>
        /// Initializes a new instance of the SquareMatrix class that
        /// with specified size and two-dimensional array. 
        /// </summary>
        /// /// <exception cref="ArgumentNullException">Thrown when size less or equal to 0 </exception>
        /// <param name="size">Amount of rows and columns</param>
        /// <param name="elements">Elements to initialize the SquareMatrix</param>
        public SquareMatrix(int size, T[,] elements) : this(size)
        {
            if (elements == null)
            {
                throw new ArgumentNullException($"Array {nameof(elements)} has null value");
            }

            T[] tempArrayToInitialize = new T[elements.GetLength(0) * elements.GetLength(1)];
            T[] temp = elements.Cast<T>().ToArray();
            temp.CopyTo(tempArrayToInitialize, 0);

            int count = 0;
            while (count < elements.Length)
            {
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        this.elements[i, j] = tempArrayToInitialize[count++];
                    }
                }
            }
        }
        #endregion

        #region Indexator
        /// <summary>
        /// Returns element of the matrix by chosen indices
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when one of any row or columns has incorrect value</exception>
        /// <param name="rowIndex">Index of row to get element</param>
        /// <param name="columnIndex">Index of row to set element</param>
        /// <returns>Element of the matrix by chosen indices</returns>
        public override T this[int rowIndex, int columnIndex]
        {
            get
            {
                CheckIndeces(rowIndex, columnIndex);
                return this.elements[rowIndex, columnIndex];
            }

            set
            {
                T oldValue = elements[rowIndex, columnIndex];
                SetValueByIndices(rowIndex, columnIndex, value);
                OnMatrixChanged(new MatrixChangedEventArgs<T>(oldValue, value));
            }
        }
        #endregion

        #region Overrided Methods        
        /// <summary>
        /// Set value of element by the chosen row and column indices
        /// </summary>
        /// <param name="rowIndex">Index of the row in matrix</param>
        /// <param name="columnIndex">Index of the columns in matrix</param>
        /// <param name="value">Value to set</param>
        public override void SetValueByIndices(int rowIndex, int columnIndex, T value)
        {
            CheckIndeces(rowIndex, columnIndex);
            elements[rowIndex, columnIndex] = value;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the matrix.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the matrix.</returns>
        public override IEnumerator<T> GetEnumerator()
        {
            foreach (var item in elements)
            {
                yield return item;
            }
        }
        #endregion
    }
}
