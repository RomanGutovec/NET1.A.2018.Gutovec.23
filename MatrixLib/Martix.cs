using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixLib
{
    public abstract class Martix<T> : IEnumerable<T>
    {
        #region Fields    
        private T[,] elements;
        #endregion

        #region Constructors
        public Martix(int rowsNumber, int columnsNumber)
        {
            if (rowsNumber <= 0)
            {
                throw new ArgumentNullException($"Amount of rows {nameof(rowsNumber)} has incorrect value");
            }

            if (columnsNumber <= 0)
            {
                throw new ArgumentNullException($"Amount of rows {nameof(columnsNumber)} has incorrect value");
            }

            this.RowsNumber = rowsNumber;
            this.ColumnsNumber = columnsNumber;
            elements = new T[rowsNumber, columnsNumber];
        }

        public Martix(int rowsNumber, int columnsNumber, T[,] elements) : this(rowsNumber, columnsNumber)
        {
            if (elements.Length > rowsNumber * columnsNumber)
            {
                throw new ArgumentException($"Incorrect length of sequency {nameof(elements)}");
            }

            T[] tempArrayToInitialize = new T[elements.GetLength(0) * elements.GetLength(1)];
            T[] temp = elements.Cast<T>().ToArray();
            temp.CopyTo(tempArrayToInitialize, 0);

            if (elements != null)
            {
                int count = 0;
                while (count < elements.Length)
                {
                    for (int i = 0; i < rowsNumber; i++)
                    {
                        for (int j = 0; j < columnsNumber; j++)
                        {
                            this.elements[i, j] = tempArrayToInitialize[count++];
                        }
                    }
                }
            }
        }

        public Martix(int rowsNumber, int columnsNumber, IEnumerable<T> elements) : this(rowsNumber, columnsNumber)
        {
            if (elements.Count() > rowsNumber * columnsNumber)
            {
                throw new ArgumentException($"Incorrect length of sequency {nameof(elements)}");
            }

            T[] tempArrayToInitialize = new T[elements.Count()];
            elements.ToArray().CopyTo(tempArrayToInitialize, 0);

            if (elements != null)
            {
                int count = 0;
                while (count < elements.Count())
                {
                    for (int i = 0; i < rowsNumber; i++)
                    {
                        for (int j = 0; j < columnsNumber; j++)
                        {
                            this.elements[i, j] = tempArrayToInitialize[count++];
                        }
                    }
                }
            }
        }
        #endregion

        #region Events
        public event EventHandler<MatrixChangedEventArgs<T>> MatrixChanged = delegate { };
        #endregion

        #region Properties
        public int RowsNumber { get; }

        public int ColumnsNumber { get; }
        #endregion

        #region Indexator
        public T this[int rowIndex, int columnIndex]
        {
            get
            {
                CheckIndeces(rowIndex, columnIndex);
                return this.elements[rowIndex, columnIndex];
            }

            set
            {
                T oldValue = value;
                SetValueByIndices(rowIndex, columnIndex, value);
                OnMatrixChanged(new MatrixChangedEventArgs<T>(oldValue, value));
            }
        }
        #endregion

        #region Implementations
        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in elements)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        #region Methods
        public void SetValueByIndices(int rowIndex, int columnIndex, T value)
        {
            CheckIndeces(rowIndex, columnIndex);

            elements[rowIndex, columnIndex] = value;
        }

        public void Clear()
        {
            for (int i = 0; i < RowsNumber; i++)
            {
                for (int j = 0; j < ColumnsNumber; j++)
                {
                    elements[i, j] = default(T);
                }
            }
        }

        protected virtual void OnMatrixChanged(MatrixChangedEventArgs<T> e)
        {
            MatrixChanged?.Invoke(this, e);
        }

        protected void CheckIndeces(int rowIndex, int columnIndex)
        {
            if (rowIndex >= RowsNumber || rowIndex < 0)
            {
                throw new ArgumentException($"Index of row {nameof(rowIndex)} has incorrect value");
            }

            if (columnIndex >= ColumnsNumber || columnIndex < 0)
            {
                throw new ArgumentException($"Index of column {nameof(columnIndex)} has incorrect value");
            }
        }
        #endregion
    }
}
