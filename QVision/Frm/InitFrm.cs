using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QVision.Params;
using QVision.Tools;

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
            try
            {
                //先测试这些值是否满足条件
                bool tempFlag = true;
                if (textBox1.Text.Trim() == "") tempFlag = false;
                if (textBox2.Text.Trim() == "") tempFlag = false;
                if (textBox3.Text.Trim() == "") tempFlag = false;
                if (textBox4.Text.Trim() == "") tempFlag = false;

                if (tempFlag)
                {
                    //测试是否有相应的receipt
                    string rpt = D2RManager.QueryReceipt(textBox2.Text);
                    if (rpt != null)
                    {
                        
                        DialogResult = DialogResult.OK;
                        Global.ready2Go = true;    //准备就绪，开始按钮可以跑
                    }
                    else
                    {
                        MessageBox.Show("未查询到该Device对应的Receipt");
                    }
                }
                else
                {
                    MessageBox.Show("有空的");
                }

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }

        private void bt_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
