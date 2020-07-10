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
    public partial class VideoFrm : Form
    {
        public VideoFrm()
        {
            InitializeComponent();
        }

        private void bt_go_Click(object sender, EventArgs e)
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
}
