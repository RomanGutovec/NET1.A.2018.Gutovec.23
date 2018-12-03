using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixLib
{
    public class SquareMatrix<T> : Martix<T>
    {
        private T[,] elements;

        #region Constructors
        public SquareMatrix(int size) : base(size)
        {
            elements = new T[size, size];
        }

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
        public override void Clear()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    elements[i, j] = default(T);
                }
            }
        }

        public override void SetValueByIndices(int rowIndex, int columnIndex, T value)
        {
            CheckIndeces(rowIndex, columnIndex);
            elements[rowIndex, columnIndex] = value;
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
