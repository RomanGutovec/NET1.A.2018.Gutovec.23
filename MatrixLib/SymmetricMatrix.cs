using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixLib
{
    public class SymmetricMatrix<T> : Martix<T>
    {
        #region Fields
        private T[][] elements;
        #endregion

        #region Constructors
        public SymmetricMatrix(int size) : base(size)
        {
            this.elements = new T[size][];
            for (int i = 0; i < size; i++)
            {
                this.elements[i] = new T[i + 1];
            }
        }

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
        public override T this[int rowIndex, int columnIndex]
        {
            get
            {
                this.CheckIndeces(rowIndex, columnIndex);
                return this.elements[rowIndex][columnIndex];
            }

            set
            {
                T oldValue = this.elements[rowIndex][columnIndex];
                this.SetValueByIndices(rowIndex, columnIndex, value);
                this.OnMatrixChanged(new MatrixChangedEventArgs<T>(oldValue, value));
            }
        }
        #endregion

        #region Overrided Methods
        public override void Clear()
        {
            for (int i = 0; i < this.Size; i++)
            {
                for (int j = 0; j < this.Size; j++)
                {
                    this.elements[i][j] = default(T);
                }
            }
        }

        public override void SetValueByIndices(int rowIndex, int columnIndex, T value)
        {
            this.CheckIndeces(rowIndex, columnIndex);
            this.elements[rowIndex][columnIndex] = value;
        }

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