﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
            DialogResult = DialogResult.OK;
        }

        private void bt_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}