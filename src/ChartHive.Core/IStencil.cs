using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartHive.Core
{
    public interface IStencil
    {
        event EventHandler NameChanged;

        event EventHandler TagsChanged;

        event EventHandler IconChanged;

        string Name
        {
            get;
            set;
        }

        string ShapeDataFormat
        {
            get;
            set;
        }

        Stream ShapeData
        {
            get;
            set;
        }

        Image Icon
        {
            get;
            set;
        }

        Image PreviewImage
        {
            get;
            set;
        }

        IEnumerable<string> Tags
        {
            get;
        }
    }
}
