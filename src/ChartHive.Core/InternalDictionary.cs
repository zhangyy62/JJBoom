using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartHive.Core
{
    public sealed class CustomTwoTuples<T, U>
    {
        private T _leftOne;
        private U _rightOne;

        public CustomTwoTuples(T t, U u)
        {
            this.SetLeftOne(t);
            this.SetRightOne(u);
        }

        public T GetLeftOne()
        {
            return this._leftOne;
        }

        protected void SetLeftOne(T t)
        {
            this._leftOne = t;
        }

        public U GetRightOne()
        {
            return this._rightOne;
        }

        protected void SetRightOne(U u)
        {
            this._rightOne = u;
        }
    }
}
