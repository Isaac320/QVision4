using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QVision.Frm
{
    public partial class SplashFrm : Form
    {
        public static int num = 0;
        public static string info = "loading...";
        public SplashFrm()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (num >= 0 && num < 100)
            {
                progressBar1.Value = num;
                label1.Text = info;
            }
            else
            {
                this.Close();
            }
        }
    }
}
