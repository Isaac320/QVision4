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
    public partial class TrayIDFrm : Form
    {
        public TrayIDFrm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Global.TrayID = textBox1.Text;
                DialogResult = DialogResult.OK;
            }
            catch (Exception eee)
            {
                MessageBox.Show(eee.ToString());
            }
        }
    }
}
