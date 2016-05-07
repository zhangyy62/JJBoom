using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AddinExpress.MSO;
using Microsoft.Office.Interop.PowerPoint;
using Application = Microsoft.Office.Interop.PowerPoint.Application;

namespace ChartHive.Core
{
    public static class StencilCategoryHelper
    {
        private static Func<IStencilCategory, bool> func_0;
        private static Func<IStencilCategory, string> func_1;

        public static IStencil smethod_3()
        {
            try
            {
        
                IStencilCategory category = new StencilCategory();
                if (category != null)
                {
                    return smethod_4(category);
                }
            }
            catch (Exception exception)
            {
 
            }
            return null;
        }

        public static IStencil smethod_4(IStencilCategory istencilCategory_0)
        {
            Cursor current = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
              
                Application documentWindow = ADXAddinModule.CurrentInstance.HostApplication as Application;
                Selection selection = documentWindow.ActiveWindow.Selection;
                ShapeRange shapeRange = selection.HasChildShapeRange ? selection.ChildShapeRange : selection.ShapeRange;
      
         
                try
                {
                    if (shapeRange != null)
                    {
                        Dictionary<Shape, string> dictionary = ShapeRangeDeCompose.smethod_1(shapeRange);
                        try
                        {
                            shapeRange.Copy();
                            CustomTwoTuples<string, Stream> class2 = ClipBoardHelper.smethod_0();
                            Stream stream = ClipBoardHelper.smethod_1();
                            if (((class2 != null) && (class2.GetRightOne() != null)) && (stream != null))
                            {
                                IStencil stencil = istencilCategory_0.AddStencil();
                                stencil.PreviewImage = new Bitmap(stream);
                        
                                stencil.ShapeDataFormat = class2.GetLeftOne();
                                stencil.ShapeData = class2.GetRightOne();
                                return stencil;
                            }
                        }
                        finally
                        {
                 
                            foreach (KeyValuePair<Shape, string> pair in dictionary)
                            {
                                Marshal.ReleaseComObject(pair.Key);
                            }
                        }
                    }
                }
                finally
                {
                /*    if (range != null)
                    {
                        Marshal.ReleaseComObject(range);
                    }*/
                }
            }
            catch (Exception exception)
            {
     
            }
            finally
            {
                Cursor.Current = current;
            }
            return null;
        }
    }
}
