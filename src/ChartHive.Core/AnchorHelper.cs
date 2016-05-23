using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Office.Interop.PowerPoint;

namespace JJBoom.Core
{
    public class AnchorHelper
    {
        private Shape _shape;

        public AnchorHelper(Shape shape_1)
        {
            this.SetShape(shape_1);
        }

        public Shape GetShape()
        {
            return this._shape;
        }

        private void SetShape(Shape shape_1)
        {
            this._shape = shape_1;
        }

        private AnchorType GetAnchorType(SmartResizeSettingEnum smartResizeSettingEnum)
        {
            AnchorType type;
            Tags tags = this.GetShape().Tags;
            try
            {
                type = AnchorTypeConvert.ConvertToEnum(tags["Anchor" + smartResizeSettingEnum]);
            }
            finally
            {
                Marshal.ReleaseComObject(tags);
            }
            return type;
        }

        public bool CheckRelative()
        {
            return ((((this.GetAnchorTypeLeft() == AnchorType.Relative) && (this.GetAnchorTypeTop() == AnchorType.Relative)) && (this.GetAnchorTypeRight() == AnchorType.Relative)) && (this.GetAnchorTypeBottom() == AnchorType.Relative));
        }

        public string BuildXMLString()
        {
            string str;
            XmlDocument document = new XmlDocument();
            XmlElement element = document.AppendChild(document.CreateElement("SmartResizeSettings")) as XmlElement;
            element.AppendChild(document.CreateElement("AnchorLeft")).InnerText = this.GetAnchorTypeLeft().ToString();
            element.AppendChild(document.CreateElement("AnchorTop")).InnerText = this.GetAnchorTypeTop().ToString();
            element.AppendChild(document.CreateElement("AnchorRight")).InnerText = this.GetAnchorTypeRight().ToString();
            element.AppendChild(document.CreateElement("AnchorBottom")).InnerText = this.GetAnchorTypeBottom().ToString();
            using (StringWriter writer = new StringWriter())
            {
                using (XmlTextWriter writer2 = new XmlTextWriter(writer))
                {
                    document.WriteTo(writer2);
                    str = writer.ToString();
                }
            }
            return str;
        }

        public AnchorType GetAnchorTypeLeft()
        {
            return this.GetAnchorType(SmartResizeSettingEnum.Left);
        }

        public AnchorType GetAnchorTypeTop()
        {
            return this.GetAnchorType(SmartResizeSettingEnum.Top);
        }

        public AnchorType GetAnchorTypeRight()
        {
            return this.GetAnchorType(SmartResizeSettingEnum.Right);
        }

        public AnchorType GetAnchorTypeBottom()
        {
            return this.GetAnchorType(SmartResizeSettingEnum.Bottom);
        }
    }
}
