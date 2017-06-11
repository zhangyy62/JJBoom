using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JJBoom.Core
{
    public class StencilEventArgs : EventArgs
    {
        private IBoom _iStencil;

		public IBoom Stencil
        {
            [CompilerGenerated]
            get { return _iStencil; }
        }

        public StencilEventArgs(IBoom stencil)
        {
 
            SetStencil(stencil);
        }

        public void SetStencil(IBoom iStencil)
        {
            this._iStencil = iStencil;
        }
	}
}
