using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QVision.Frm;
using QVision.Report;
using System.Threading;

namespace QVision
{
    public partial class MainFrm : Form
    {
       
        public MainFrm()
        {
            InitializeComponent();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (this.ActiveMdiChild == Frames.videoFrm)
                {
                    Cursor.Current = Cursors.Arrow;
                    return;
                }
                Frames.videoFrm.MdiParent = this;
                Frames.videoFrm.Dock = DockStyle.Fill;
                Frames.videoFrm.Show();
                Frames.videoFrm.Activate();
                Cursor.Current = Cursors.Arrow;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (this.ActiveMdiChild == Frames.recipeFrm)
                {
                    Cursor.Current = Cursors.Arrow;
                    return;
                }
                Frames.recipeFrm.MdiParent = this;
                Frames.recipeFrm.Dock = DockStyle.Fill;
                Frames.recipeFrm.Show();
                Frames.recipeFrm.Activate();
                Cursor.Current = Cursors.Arrow;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }

        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            SplashFrm.num = 30;
            Frames.videoFrm = new VideoFrm();
            Frames.recipeFrm = new RecipeFrm();
            Frames.settingFrm = new SettingFrm();
            Frames.reportFrm = new ReportFrm();
            SplashFrm.num = 60;
            Thread.Sleep(1200);
            SplashFrm.num = 100;

            this.WindowState = FormWindowState.Maximized;
            this.Visible = true;
            Frames.videoFrm.MdiParent = this;
            Frames.videoFrm.Dock = DockStyle.Fill;
            Frames.videoFrm.Show();
            Frames.videoFrm.Activate();

            ImgProcess.Project.getInstance().Init();



        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (this.ActiveMdiChild == Frames.settingFrm)
                {
                    Cursor.Current = Cursors.Arrow;
                    return;
                }
                Frames.settingFrm.MdiParent = this;
                Frames.settingFrm.Dock = DockStyle.Fill;
                Frames.settingFrm.Show();
                Frames.settingFrm.Activate();
                Cursor.Current = Cursors.Arrow;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }

        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Frm.AboutFrm aboutFrm = new Frm.AboutFrm())
            {
                aboutFrm.ShowDialog();
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (MessageBox.Show("退出本系统?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Application.Exit();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (this.ActiveMdiChild == Frames.reportFrm)
                {
                    Cursor.Current = Cursors.Arrow;
                    return;
                }
                Frames.reportFrm.MdiParent = this;
                Frames.reportFrm.Dock = DockStyle.Fill;
                Frames.reportFrm.Show();
                Frames.reportFrm.Activate();
                Cursor.Current = Cursors.Arrow;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }
    }
}
