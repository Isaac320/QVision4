using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QVision.VisionTools.CheckLineTool
{
    public partial class CheckLineToolCtr : UserControl
    {
        private CheckLineTool tool;
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
            }
        }
    }
}
