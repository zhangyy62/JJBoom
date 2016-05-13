using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Core;
using GroupShapes = Microsoft.Office.Interop.PowerPoint.GroupShapes;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;
using ShapeRange = Microsoft.Office.Interop.PowerPoint.ShapeRange;

namespace ChartHive.Core
{
    public class ShapeRangeDeCompose
    {
        public static Dictionary<Shape, string> smethod_1(ShapeRange shapeRange)
        {
            Dictionary<Shape, string> dictionary = new Dictionary<Shape, string>();
            for (int i = 1; i <= shapeRange.Count; i++)
            {
                foreach (KeyValuePair<Shape, string> pair in smethod_0(shapeRange[i]))
                {
                    dictionary.Add(pair.Key, pair.Value);
                }
            }
            return dictionary;
        }

        private static Dictionary<Shape, string> smethod_0(Shape shape)
        {
            Dictionary<Shape, string> dictionary = new Dictionary<Shape, string>();
            if (shape.Type ==  MsoShapeType.msoGroup)
            {
                ShapeBorder shapeBorder = new ShapeBorder(shape);
                GroupShapes o = shape.GroupItems;
                try
                {
                    if (!shapeBorder.method_8())
                    {
                        dictionary.Add(shape, shape.AlternativeText);
          
                    }
                    else
                    {
                        Marshal.ReleaseComObject(shape);
                        shape = null;
                    }
                    for (int i = 1; i <= o.Count; i++)
                    {
                        foreach (KeyValuePair<Shape, string> pair in smethod_0(o[i]))
                        {
                            dictionary.Add(pair.Key, pair.Value);
                        }
                    }
                    return dictionary;
                }
                finally
                {
                    Marshal.ReleaseComObject(o);
                    o = null;
                }
            }
            AnchorHelper class3 = new AnchorHelper(shape);
            if (!class3.CheckRelative())
            {
                dictionary.Add(shape, shape.AlternativeText);
                shape.AlternativeText = class3.BuildXMLString();
                ;
                return dictionary;
            }
            Marshal.ReleaseComObject(shape);
            shape = null;
            return dictionary;
        }
    }
}
