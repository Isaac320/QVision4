using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QVision.Params;
//using QVision.Tools;

namespace QVision.Frm
{
    public partial class InitFrm : Form
    {
        public InitFrm()
        {
            InitializeComponent();
        }

        private void bt_OK_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    //先测试这些值是否满足条件
            //    bool tempFlag = true;
            //    if (textBox1.Text.Trim() == "") tempFlag = false;
            //    if (textBox2.Text.Trim() == "") tempFlag = false;
            //    if (textBox3.Text.Trim() == "") tempFlag = false;
            //    if (textBox4.Text.Trim() == "") tempFlag = false;

            //    if (tempFlag)
            //    {
            //        //测试是否有相应的receipt
            //       // string rpt = D2RManager.QueryReceipt(textBox2.Text);
            //        if (rpt != null)
            //        {
            //            //这边读recipe  以后需要改写 这里默认选了一个测试
            //            string path = @"d:\";
            //            string name = "ss.zl";
            //          //  Global.Dict = RecipeTool.DeSerializeNow(path, name);
                        
            //            //都满足则就运行下面的。
            //            Global.LotNum = textBox1.Text;
            //            Global.Device = textBox2.Text;
            //            Global.OperatorID = textBox3.Text;
            //            Global.TotalFrame = int.Parse(textBox4.Text);
            //            Global.RecipeName = rpt;

            //            ImgProcess.Project.getInstance().InitRecipe();

                        

            //            Global.ready2Go = true;    //准备就绪，开始按钮可以跑

            //            DialogResult = DialogResult.OK;                      
            //        }
            //        else
            //        {
            //            MessageBox.Show("未查询到该Device对应的Receipt");
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("有空的");
            //    }

            //}
            //catch (Exception ee)
            //{
            //    MessageBox.Show(ee.ToString());
            //}
        }

        private void bt_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
