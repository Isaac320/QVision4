using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HalconDotNet;

namespace QVision.VisionTools.CheckLineTool
{
    public partial class CheckLineToolCtr : UserControl
    {
        private CheckLineTool tool;
        HDrawingObject rectCheck = null;        
        HTuple rectParams = new HTuple("row1", "column1", "row2", "column2");
        public CheckLineToolCtr()
        {
            InitializeComponent();
        }

        public CheckLineToolCtr(CheckLineTool tool)
        {
            this.tool = tool;
            InitializeComponent();
        }

        private void hSmartWindowControl1_Load(object sender, EventArgs e)
        {
            hSmartWindowControl1.MouseWheel += hSmartWindowControl1.HSmartWindowControl_MouseWheel;
        }

        private void CheckLineToolCtr_Load(object sender, EventArgs e)
        {
            if (tool.Image != null)
            {
                hSmartWindowControl1.HalconWindow.DispObj(tool.Image);
                hSmartWindowControl1.HalconWindow.SetPart(0, 0, -2, -2);

                rectCheck = new HDrawingObject(tool.Rect1[0], tool.Rect1[1], tool.Rect1[2], tool.Rect1[3]);
                rectCheck.SetDrawingObjectParams("color", "green");

                rectCheck.OnAttach(hProcess);
                rectCheck.OnDrag(hProcess);
                rectCheck.OnResize(hProcess);

                DrawRegions();
            }

            

            
            

        }

        private void hProcess(HDrawingObject dobj,HWindow hwin,string type)
        {
            hwin.SetWindowParam("flush", "false");
            hwin.ClearWindow();
            process();
            hwin.DispObj(tool.Image);
            hwin.SetColor("red");
            hwin.DispObj(tool.Lines);
            hwin.SetWindowParam("flush", "true");
            hwin.FlushBuffer();
        }

        private void DrawRegions()
        {
            hSmartWindowControl1.HalconWindow.AttachDrawingObjectToWindow(rectCheck);
        }

        private void ShowLines()
        {
            HTuple htemp1 = new HTuple(rectCheck.GetDrawingObjectParams(rectCheck));
            tool.Rect1 = new double[4] { htemp1[0], htemp1[1], htemp1[2], htemp1[3] };
        }

        private void process()
        {
            HTuple htemp1 = new HTuple(rectCheck.GetDrawingObjectParams(rectParams));
            tool.Rect1 = new double[4] { htemp1[0], htemp1[1], htemp1[2], htemp1[3] };
            tool.Run();            
        }
             

    }
}
