using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JJBoom.Core
{
    public class ProgressEventArgs : EventArgs
    {
        // Fields
        private double _progressEventArgs;

        // Methods
        public ProgressEventArgs(double progress)
        {
            this.SetProgressEventArgs(progress);
        }

        private void SetProgressEventArgs(double double_1)
        {
            this._progressEventArgs = double_1;
        }

        // Properties
        public double Progress
        {
            [CompilerGenerated]
            get
            {
                return this._progressEventArgs;
            }
        }

    }
}
