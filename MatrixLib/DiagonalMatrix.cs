using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixLib
{
    /// <summary>
    /// Represents a strongly typed diagonal matrix of objects that can be accessed by index. Provides
    /// methods to set by indices.
    /// </summary>
    /// <typeparam name="T">The type of objects include in the matrix.</typeparam>
    public class DiagonalMatrix<T> : Martix<T>
    {
        #region Fields
        private T[] elements;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the DiagonalMatrix class that
        /// with specified size. 
        /// </summary>
        /// /// <exception cref="ArgumentNullException">Thrown when size less or equal to 0 </exception>
        /// <param name="size">Amount of rows and columns</param>
        public DiagonalMatrix(int size) : base(size)
        {
            this.elements = new T[size];
        }

        /// <summary>
        /// Initializes a new instance of the DiagonalMatrix class that
        /// with specified size and array. 
        /// </summary>
        /// /// <exception cref="ArgumentNullException">Thrown when size less or equal to 0 </exception>
        /// <param name="size">Amount of rows and columns</param>
        /// <param name="elements">Elements on the main diagonal to initialize the DiagonalMatrix</param>
        public DiagonalMatrix(int size, T[] elements) : this(size)
        {
            CheckNullInputData(elements);

            T[] tempArrayToInitialize = new T[elements.Length];
            elements.CopyTo(tempArrayToInitialize, 0);
            int count = 0;
            while (count < tempArrayToInitialize.Length)
            {
                for (int i = 0; i < size; i++)
                {
                    this.SetValueByIndices(i, i, tempArrayToInitialize[count++]);
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the DiagonalMatrix class that
        /// with specified size and two-dimensional array. 
        /// </summary>
        /// /// <exception cref="ArgumentNullException">Thrown when size less or equal to 0 </exception>
        /// <param name="size">Amount of rows and columns</param>
        /// <param name="elements">Elements to initialize the DiagonalMatrix</param>
        public DiagonalMatrix(int size, T[,] elements) : this(size)
        {
            CheckNullInputData(elements);

            T[] tempArrayToInitialize = new T[elements.GetLength(0) * elements.GetLength(1)];
            T[] temp = elements.Cast<T>().ToArray();
            int count = 0;
            while (count < tempArrayToInitialize.Length)
            {
                for (int i = 0; i < size; i++)
                {
                    this.SetValueByIndices(i, i, tempArrayToInitialize[count++]);
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
                if (rowIndex != columnIndex)
                {
                    return default(T);
                }

                return this.elements[rowIndex];
            }

            set
            {
                if (rowIndex == columnIndex)
                {
                    T oldValue = elements[rowIndex];
                    this.SetValueByIndices(rowIndex, columnIndex, value);
                    this.OnMatrixChanged(new MatrixChangedEventArgs<T>(oldValue, value));
                }
            }
        }
        #endregion

        #region Overrided methods
        /// <summary>
        /// Set value of element by the chosen row and column indices
        /// </summary>
        /// <param name="rowIndex">Index of the row in matrix</param>
        /// <param name="columnIndex">Index of the columns in matrix</param>
        /// <param name="value">Value to set</param>
        public override void SetValueByIndices(int rowIndex, int columnIndex, T value)
        {
            CheckIndeces(rowIndex, columnIndex);
            if (rowIndex == columnIndex)
            {
                elements[rowIndex] = value;
            }
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

        #region Private methods
        private void CheckNullInputData(Array elements)
        {
            if (elements == null)
            {
                throw new ArgumentNullException($"Elements {nameof(elements)} has no necessary values");
            }
        }
        #endregion
    }
}
