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
        public double[] Rect1 = new double[4] { 50, 50, 200, 200 };
        private double sigma=1.5, low=3, high=10;

        private string lightDark = "dark";

        private HXLDCont lines;

        public double Sigma
        {
            set { sigma = value; }
            get { return sigma; }
        }

        public double Low
        {
            set { low = value; }
            get { return low; }
        }

        public double High
        {
            set { high = value; }
            get { return high; }
        }



        public HImage Image
        {
            get { return himage; }
            set { himage = value; }
        }

        public HXLDCont Lines
        {
            get { return lines; }
        }


        public void Run()
        {
            try
            {
                HRegion hRegion = new HRegion(Rect1[0], Rect1[1], Rect1[2], Rect1[3]);
                HImage ROIImage = himage.ReduceDomain(hRegion);
                lines = ROIImage.LinesGauss(sigma, low, high, lightDark, "true", "bar-shaped", "true");
            }
            catch(Exception ee)
            {

            }
        }





    }
}
