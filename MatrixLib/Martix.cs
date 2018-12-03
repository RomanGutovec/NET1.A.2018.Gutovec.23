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

        #region Constructors
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
        public event EventHandler<MatrixChangedEventArgs<T>> MatrixChanged = delegate { };
        #endregion

        #region Properties
        public int Size { get; private set; }        
        #endregion

        #region Indexator
        public abstract T this[int rowIndex, int columnIndex] { get; set; }
        #endregion

        #region Implementations
        public abstract IEnumerator<T> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        #region Methods
        public abstract void SetValueByIndices(int rowIndex, int columnIndex, T value);        

        public abstract void Clear();        

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
