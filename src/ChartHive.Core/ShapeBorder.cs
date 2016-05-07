using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

namespace ChartHive.Core
{
    internal sealed class ShapeBorder
    {
        private Shape _shape;

        public ShapeBorder(Shape shape)
        {
            this.SetShape(shape);
        }

        public Shape GetCurrentShape()
        {
            return this._shape;
        }

        public bool method_2()
        {
            bool flag;
            if (this.GetCurrentShape().Type != MsoShapeType.msoGroup)
            {
                return false;
            }
            Tags o = this.GetCurrentShape().Tags;
            try
            {
                flag = o["EnableSmartResize"] == "True";
            }
            finally
            {
                Marshal.ReleaseComObject(o);
            }
            return flag;
        }
        public bool method_8()
        {
            return ((!this.method_2() && (this.method_4() == 0.0)) && (this.method_6() == 0.0));
        }

        public double method_4()
        {
            double num = 0.0;
            if (this.GetCurrentShape().Type == MsoShapeType.msoGroup)
            {
                string str = string.Empty;
                Tags o = this.GetCurrentShape().Tags;
                try
                {
                    str = o["MinWidth"];
                }
                finally
                {
                    Marshal.ReleaseComObject(o);
                }
                if (string.IsNullOrEmpty(str))
                {
                    return num;
                }
                try
                {
                    num = Convert.ToDouble(str, NumberFormatInfo.InvariantInfo);
                    if (num < 0.0)
                    {
                        num = 0.0;
                    }
                }
                catch (FormatException)
                {
                }
            }
            return num;
        }

        public double method_6()
        {
            double num = 0.0;
            if (this.GetCurrentShape().Type == MsoShapeType.msoGroup)
            {
                string str = string.Empty;
                Tags o = this.GetCurrentShape().Tags;
                try
                {
                    str = o["MinHeight"];
                }
                finally
                {
                    Marshal.ReleaseComObject(o);
                }
                if (string.IsNullOrEmpty(str))
                {
                    return num;
                }
                try
                {
                    num = Convert.ToDouble(str, NumberFormatInfo.InvariantInfo);
                    if (num < 0.0)
                    {
                        num = 0.0;
                    }
                }
                catch (FormatException)
                {
                }
            }
            return num;
        }

        private void SetShape(Shape shape)
        {
            this._shape = shape;
        }


    }
}
