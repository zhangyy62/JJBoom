using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChartHive.Core
{
    public class StencilEventArgs : EventArgs
    {
        private IStencil _iStencil;

		public IStencil Stencil
        {
            [CompilerGenerated]
            get { return _iStencil; }
        }

        public StencilEventArgs(IStencil stencil)
        {
 
            SetStencil(stencil);
        }

        public void SetStencil(IStencil iStencil)
        {
            this._iStencil = iStencil;
        }
	}
}
