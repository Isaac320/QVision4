using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;

namespace QVision.VisionTools.CheckLineTool
{
    [Serializable]
    public class CheckLineTool
    {
        public HImage himage = null;
        public HImage Image
        {
            get { return himage; }
            set { himage = value; }
        }
    }
}
