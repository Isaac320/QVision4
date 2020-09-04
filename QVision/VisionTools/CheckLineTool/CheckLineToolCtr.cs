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

        bool checkFlag = true;
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
                update();
            }
                                 
        }

        private void hProcess(HDrawingObject dobj,HWindow hwin,string type)
        {
            if (checkFlag)
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

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            tool.Sigma = trackBar3.Value * 1.0 / 10;
            update();
            updateRect();
        }

        private void update()
        {
            if(this.InvokeRequired)
            {
                BeginInvoke(new Action(update), new object[] { });
            }
            else
            {
                lb_high.Text = tool.High.ToString();
                lb_low.Text = tool.Low.ToString();
                lb_sigma.Text = tool.Sigma.ToString();
                if (tool.LightDark == "dark")
                {
                    comboBox1.SelectedIndex = 1;
                }
                else
                {
                    comboBox1.SelectedIndex = 0;
                }
                
            }
        }

        private void updateRect()
        {
            hSmartWindowControl1.HalconWindow.DetachDrawingObjectFromWindow(rectCheck);
            hSmartWindowControl1.HalconWindow.AttachDrawingObjectToWindow(rectCheck);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            tool.Low = trackBar1.Value * 1.0 / 2;
            if(trackBar2.Value<trackBar1.Value)
            {
                trackBar2.Value = trackBar1.Value;
            }
            update();
            updateRect();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            tool.High = trackBar2.Value * 1.0 / 2;
            if(trackBar2.Value<trackBar1.Value)
            {
                trackBar1.Value = trackBar2.Value;
            }
            update();
            updateRect();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedItem.ToString()=="Light")
            {
                tool.LightDark = "light";
            }
            else
            {
                tool.LightDark = "dark";
            }
            updateRect();

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                checkFlag = true;
            }
            else
            {
                checkFlag = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
