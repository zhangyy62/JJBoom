using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartHive.Core
{
    public interface IStencilManager
    {
        event EventHandler<ProgressEventArgs> LoadingProgress;

       /* event EventHandler<StencilCategoryEventArgs> CategoryAdded;

        event EventHandler<StencilCategoryEventArgs> CategoryRemoved;*/

        object SyncRoot
        {
            get;
        }

        IEnumerable<IStencilCategory> Categories
        {
            get;
        }

        IStencilCategory AddCategory(bool isUserDefined);

        bool RemoveCategory(IStencilCategory stencilCategory);

        IStencilCategory ImportCategory(string categoryFileName);

        void LoadStencils();
    }
}
