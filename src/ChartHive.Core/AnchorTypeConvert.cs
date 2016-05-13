using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartHive.Core
{
    public class AnchorTypeConvert
    {
        internal static AnchorType ConvertToEnum(string anchorTypeSting)
        {
            if (!string.IsNullOrEmpty(anchorTypeSting) && Enum.IsDefined(typeof(AnchorType), anchorTypeSting))
            {
                return (AnchorType)Enum.Parse(typeof(AnchorType), anchorTypeSting);
            }
            return AnchorType.Relative;
        }
    }
}


