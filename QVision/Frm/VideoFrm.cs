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
using HalconDotNet;

namespace QVision.Frm
{
    public partial class VideoFrm : Form
    {
        delegate void delegateShowMessage(string s);
        public VideoFrm()
        {
            InitializeComponent();
        }
        private void bt_go_Click(object sender, EventArgs e)
        {
            if (!Global.ready2Go)
            {
                using (InitFrm initfrm = new InitFrm())
                {
                    if (initfrm.ShowDialog() == DialogResult.OK)
                    {
                        ShowList();
                    }
                }
            }

            if (Global.ready2Go)
            {
                if (Global.mySwitch1)
                {
                    Global.mySwitch1 = false;
                    listBoxShowMessage("继续工作");
                }

                if (Global.mMState == MachineState.Free)
                {
                    listBoxShowMessage("开始工作");
                    Global.mMState = MachineState.Run;
                }
            }
        }

        public void showImage(HObject obj, int index)
        {
            HOperatorSet.SetDraw(hSmartWindowControl1.HalconWindow, "margin");
            HOperatorSet.SetLineWidth(hSmartWindowControl1.HalconWindow, 2);
            if (index == 1)
            {
                HOperatorSet.SetColor(hSmartWindowControl1.HalconWindow, "red");
            }
            else
            {
                HOperatorSet.SetColor(hSmartWindowControl1.HalconWindow, "red");
            }
            hSmartWindowControl1.HalconWindow.DispObj(obj);
        }

        public void ShowList()
        {
            if (listView1.InvokeRequired)
            {
                listView1.Invoke(new Action(ShowList), new object[] { });
            }
            else
            {
                listView1.Items.Clear();
                this.listView1.BeginUpdate();
                ListViewItem lvi = new ListViewItem();
                lvi.ImageIndex = 0;
                lvi.Text = "Lot Num";
                lvi.SubItems.Add(Global.LotNum);
                this.listView1.Items.Add(lvi);

                lvi = new ListViewItem();
                lvi.ImageIndex = 1;
                lvi.Text = "Device";
                lvi.SubItems.Add(Global.Device);
                this.listView1.Items.Add(lvi);

                lvi = new ListViewItem();
                lvi.ImageIndex = 2;
                lvi.Text = "Operator ID";
                lvi.SubItems.Add(Global.OperatorID);
                this.listView1.Items.Add(lvi);

                lvi = new ListViewItem();
                lvi.ImageIndex = 3;
                lvi.Text = "Total Frame";
                lvi.SubItems.Add(Global.TotalFrame.ToString());
                this.listView1.Items.Add(lvi);
                this.listView1.EndUpdate();

                lvi = new ListViewItem();
                lvi.ImageIndex = 4;
                lvi.Text = "Recipe Name";
                lvi.SubItems.Add(Global.RecipeName.ToString());
                this.listView1.Items.Add(lvi);
                this.listView1.EndUpdate();

            }
        }

        public void listBoxShowMessage(string s)
        {
            if (listBox1.InvokeRequired)
            {
                BeginInvoke(new delegateShowMessage(listBoxShowMessage), new object[] { s });

            }
            else
            {
                string mystring = DateTime.Now.ToString("HH:mm:ss") + " " + s;
                listBox1.Items.Add(mystring);

                //写log
                LogManager.WriteLog(s);

                if (listBox1.Items.Count > 200)
                {
                    for (int i = 80; i > -1; i--)
                    {
                        listBox1.Items.RemoveAt(i);
                    }
                }
                listBox1.TopIndex = listBox1.Items.Count - 1;
            }
        }

        private void bt_pause_Click(object sender, EventArgs e)
        {
            if (Global.ready2Go)
            {
                Global.mySwitch1 = true;
                listBoxShowMessage("暂停");
            }
        }

        private void bt_step_Click(object sender, EventArgs e)
        {
            if (Global.ready2Go)
            {
                Global.mySwitch2 = false;
                listBoxShowMessage("单步");
            }
        }

        private void hSmartWindowControl1_Load(object sender, EventArgs e)
        {
            hSmartWindowControl1.MouseWheel += hSmartWindowControl1.HSmartWindowControl_MouseWheel;
        }
    }
}
