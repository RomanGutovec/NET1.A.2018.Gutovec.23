using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixLib
{
    public class DiagonalMatrix<T> : Martix<T>
    {
        private T[] elements;

        #region Constructors
        public DiagonalMatrix(int size) : base(size)
        {
            this.elements = new T[size];
        }

        public DiagonalMatrix(int size, T[,] elements) : this(size)
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
                for (int i = 0; i < size; i++)
                {
                    this.SetValueByIndices(i, i, tempArrayToInitialize[count++]);
                }
            }
        }
        #endregion

        #region Indexator
        public override T this[int rowIndex, int columnIndex]
        {
            get
            {
                CheckIndeces(rowIndex, columnIndex);
                return this.elements[((rowIndex - 1) * Size) + columnIndex];
            }

            set
            {
                T oldValue = elements[((rowIndex - 1) * Size) + columnIndex];
                this.SetValueByIndices(rowIndex, columnIndex, value);
                this.OnMatrixChanged(new MatrixChangedEventArgs<T>(oldValue, value));
            }
        }
        #endregion

        #region Overrided methods
        public override void Clear()
        {
            for (int i = 0; i < Size * Size; i++)
            {
                elements[i] = default(T);
            }
        }

        public override void SetValueByIndices(int rowIndex, int columnIndex, T value)
        {
            CheckIndeces(rowIndex, columnIndex);
            elements[(rowIndex - 1) * Size + columnIndex] = value;
        }

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
