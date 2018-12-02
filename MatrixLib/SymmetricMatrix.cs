using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixLib
{
    public class SymmetricMatrix<T> : Martix<T>
    {
        public SymmetricMatrix(int rowNumber) : base(rowNumber, rowNumber)
        {
        }

        public SymmetricMatrix(int rowNumber, T[,] elements) : base(rowNumber, rowNumber)
        {
            T[] tempArrayToInitialize = new T[elements.Length];
            elements.CopyTo(tempArrayToInitialize, 0);
            int count = 0;
            while (count < tempArrayToInitialize.Length)
            {
                for (int i = 0; i < rowNumber; i++)
                {
                    for (int j = 0; j < rowNumber; j++)
                    {
                        this.SetValueByIndices(j, i, tempArrayToInitialize[count++]);
                    }
                }
            }
        }
    }
}