using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Dll_MA_IFacturacion.CFDI.CFDFunctions
{
    public class frmPB : Form
    {
        private IContainer components = null;
        private Label label1;
        private Label lblcmd;
        private Label lblTitle;
        private bool mAuto = false;
        private int mLoopAt = 100;
        public ProgressBar pb;

        public frmPB()
        {
            this.InitializeComponent();
        }

        public void Auto()
        {
            this.Auto(100);
        }

        public void Auto(int LoopAt)
        {
            this.mAuto = true;
            this.mLoopAt = LoopAt;
            this.pb.Minimum = 0;
            this.pb.Maximum = LoopAt;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void FinishAuto()
        {
            this.FinishAuto(true);
        }

        public void FinishAuto(bool complete)
        {
            if (complete)
            {
                this.pb.Value = this.pb.Maximum;
            }
            Application.DoEvents();
            base.Close();
        }

        private void frmPB_Load(object sender, EventArgs e)
        {
            Application.DoEvents();
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lblcmd = new Label();
            this.pb = new ProgressBar();
            this.label1 = new Label();
            base.SuspendLayout();
            this.lblTitle.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblTitle.Location = new Point(12, 8);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(0x170, 0x10);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Procesando";
            this.lblcmd.Location = new Point(12, 0x22);
            this.lblcmd.Name = "lblcmd";
            this.lblcmd.Size = new Size(0x170, 0x13);
            this.lblcmd.TabIndex = 1;
            this.lblcmd.Text = "Ejecutando comando...";
            this.pb.Location = new Point(12, 0x38);
            this.pb.Name = "pb";
            this.pb.Size = new Size(0x177, 0x12);
            this.pb.TabIndex = 2;
            this.label1.Location = new Point(0x3b, 0x4e);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x147, 0x13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Este proceso puede tardar varios minutos. Por favor espere...";
            this.label1.TextAlign = ContentAlignment.MiddleRight;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = Color.White;
            base.ClientSize = new Size(0x192, 0x6f);
            base.ControlBox = false;
            base.Controls.Add(this.label1);
            base.Controls.Add(this.pb);
            base.Controls.Add(this.lblcmd);
            base.Controls.Add(this.lblTitle);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            base.Name = "frmPB";
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Progreso";
            base.Load += new EventHandler(this.frmPB_Load);
            base.ResumeLayout(false);
        }

        public void SetProgress()
        {
            this.SetProgress("Ejecutando comando...", 0);
        }

        public void SetProgress(string commadDesc, int mvalue)
        {
            this.lblcmd.Text = commadDesc;
            if (!((mvalue <= 0) || this.mAuto))
            {
                this.pb.Value = mvalue;
            }
            else if (this.mAuto)
            {
                if ((this.pb.Value + 1) > this.mLoopAt)
                {
                    this.pb.Value = 0;
                }
                this.pb.Value++;
            }
            Application.DoEvents();
        }

        public void setTitle(string title)
        {
            this.lblTitle.Text = title;
        }
    }
}

