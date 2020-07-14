using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QVision.Params;

namespace QVision.Frm
{
    public partial class VideoFrm : Form
    {
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
                        //showList();
                    }
                }
            }
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


    }
}
