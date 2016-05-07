using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartHive.Core
{
    class StencilCategory : IStencilCategory
    {
        public event EventHandler NameChanged;
        public event EventHandler<StencilEventArgs> StencilAdded;
        public event EventHandler<StencilEventArgs> StencilRemoved;
        public event EventHandler IsUserDefinedChanged;
        public event EventHandler ShowStencilNamesChanged;
        public string Name { get; set; }
        public IEnumerable<IStencil> Stencils { get; }
        public bool IsUserDefined { get; }
        public bool IsEditable { get; }
        public bool ShowStencilNames { get; }
        public IStencil AddStencil()
        {
            throw new NotImplementedException();
        }

        public bool RemoveStencil(IStencil stencil)
        {
            throw new NotImplementedException();
        }
    }
}
