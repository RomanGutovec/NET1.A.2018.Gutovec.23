using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixLib
{
    public class MatrixChangedEventArgs<T> : EventArgs
    {
        #region Constructors
        public MatrixChangedEventArgs(T oldValue, T newValue)
        {
            this.OldValue = oldValue;
            this.NewValue = newValue;
        }
        #endregion

        #region Properties
        public T OldValue { get; set; }

        public T NewValue { get; set; }
        #endregion
    }
}
