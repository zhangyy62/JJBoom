using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartHive.Core
{
    public interface IStencilCategory
    {
        event EventHandler NameChanged;

        event EventHandler<StencilEventArgs> StencilAdded;

        event EventHandler<StencilEventArgs> StencilRemoved;

        event EventHandler IsUserDefinedChanged;

        event EventHandler ShowStencilNamesChanged;

        string Name
        {
            get;
            set;
        }

        IEnumerable<IStencil> Stencils
        {
            get;
        }

        bool IsUserDefined
        {
            get;
        }

        bool IsEditable
        {
            get;
        }

        bool ShowStencilNames
        {
            get;
        }

        IStencil AddStencil();

        bool RemoveStencil(IStencil stencil);
    }
}
