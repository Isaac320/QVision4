using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HalconDotNet;
using QVision.Params;

namespace QVision.Frm
{
    public partial class RecipeFrm : Form
    {
        public static Dictionary<string, object> Dict = new Dictionary<string, object>();  //字典，用来存放工具，将它序列化成一个二进制文件

        HImage hImage = null;
        public RecipeFrm()
        {
            InitializeComponent();
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                switch (listBox1.SelectedItem.ToString())
                {
                    case Global.ToolName1:
                        VisionTools.BParamTool.BParamTool bParamTool = (VisionTools.BParamTool.BParamTool)Dict["预填参数"];
                        VisionTools.BParamTool.BParamToolCtr bParamToolCtr = new VisionTools.BParamTool.BParamToolCtr(bParamTool);
                        panel2.Controls.Clear();
                        panel2.Controls.Add(bParamToolCtr);
                        bParamToolCtr.Dock = DockStyle.Fill;
                        break;
                    case Global.ToolName2:
                      //  VisionTools.MacthTool.MatchTool matchTool = (VisionTools.MacthTool.MatchTool)Dict["匹配工具"]; 
                        VisionTools.MatchTool.MatchTool matchTool= (VisionTools.MatchTool.MatchTool)Dict["匹配工具"];

                        if (hImage != null)
                        {
                            matchTool.Image = hImage;
                        }
                        VisionTools.MatchTool.MatchToolCtr matchToolCtr = new VisionTools.MatchTool.MatchToolCtr(matchTool);
                        panel2.Controls.Clear();
                        panel2.Controls.Add(matchToolCtr);
                        matchToolCtr.Dock = DockStyle.Fill;

                        break;
                    default:
                        break;
                }
            }
            catch (Exception ee)
            {

            }
        }

        private void RecipeFrm_Load(object sender, EventArgs e)
        {
            VisionTools.BParamTool.BParamTool bParamTool = new VisionTools.BParamTool.BParamTool();
            Dict.Add("预填参数", bParamTool);

            VisionTools.MatchTool.MatchTool matchTool = new VisionTools.MatchTool.MatchTool();
            Dict.Add("匹配工具", matchTool);
        }

        private void bt_readImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog opd = new OpenFileDialog();
            if (opd.ShowDialog() == DialogResult.OK)
            {
                hImage = new HImage(opd.FileName);
                hSmartWindowControl1.HalconWindow.DispImage(hImage);
                hSmartWindowControl1.HalconWindow.SetPart(0, 0, -2, -2);
            }
        }
      
    }
}
