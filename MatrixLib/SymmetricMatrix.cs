using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixLib
{
    /// <summary>
    /// Represents a strongly typed symmetric matrix of objects that can be accessed by index. Provides
    /// methods to set by indices.
    /// </summary>
    /// <typeparam name="T">The type of objects include in the matrix.</typeparam>
    public class SymmetricMatrix<T> : Martix<T>
    {
        #region Fields
        private T[][] elements;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the SymmetricMatrix class that
        /// with specified size. 
        /// </summary>
        /// /// <exception cref="ArgumentNullException">Thrown when size less or equal to 0 </exception>
        /// <param name="size">Amount of rows and columns</param>
        public SymmetricMatrix(int size) : base(size)
        {
            this.elements = new T[size][];
            for (int i = 0; i < size; i++)
            {
                this.elements[i] = new T[i + 1];
            }
        }

        /// <summary>
        /// Initializes a new instance of the SymmetricMatrix class that
        /// with specified size and two-dimensional array. 
        /// </summary>
        /// /// <exception cref="ArgumentNullException">Thrown when size less or equal to 0 </exception>
        /// <param name="size">Amount of rows and columns</param>
        /// <param name="elements">Elements to initialize the SymmetricMatrix</param>
        public SymmetricMatrix(int size, T[,] elements) : this(size)
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
                        if (j > i)
                        {
                            count += size - j;
                            break;
                        }
                        else
                        {
                            this.elements[i][j] = tempArrayToInitialize[count++];
                        }
                    }
                }
            }
        }
        #endregion

        #region Indexators
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
                this.CheckIndeces(rowIndex, columnIndex);
                if (columnIndex > rowIndex)
                {
                    return this.elements[columnIndex][rowIndex];
                }

                return this.elements[rowIndex][columnIndex];
            }

            set
            {
                T oldValue;
                if (columnIndex > rowIndex)
                {
                    oldValue = this.elements[columnIndex][rowIndex];
                }
                else
                {
                    oldValue = this.elements[rowIndex][columnIndex];
                }

                this.SetValueByIndices(rowIndex, columnIndex, value);
                this.OnMatrixChanged(new MatrixChangedEventArgs<T>(oldValue, value));
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
            this.CheckIndeces(rowIndex, columnIndex);

            if (columnIndex > rowIndex)
            {
                this.elements[columnIndex][rowIndex] = value;
            }
            else
            {
                this.elements[rowIndex][columnIndex] = value;
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the matrix.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the matrix.</returns>
        public override IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Size; i++)
            {
                for (int j = 0; j < this.Size; j++)
                {
                    if (j > this.elements[i].Length - 1)
                    {
                        yield return this.elements[j][i];
                    }
                    else
                    {
                        yield return this.elements[i][j];
                    }
                }
            }
        }
        #endregion
    }
}