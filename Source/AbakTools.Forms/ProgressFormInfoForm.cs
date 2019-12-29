using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AbakTools.Forms
{
    public partial class ProgressInfoForm : Form, IInfo, IProgressForm, Enova.API.IProgressForm, Enova.API.IInfo
    {
        #region Fields

        private bool secondProgressEnable;
        private ProgressProcessDelegate process;
        private Action completedProcess;
        private static ProgressInfoForm progressForm;

        #endregion

        #region Properties

        public bool SecondProgressEnable
        {
            get { return this.secondProgressEnable; }
            set
            {
                this.setSecondProgressEnable(value);
            }
        }

        public string Text1
        {
            get { return this.textBox1.Text; }
            set
            {
                this.setProgressText(value);
            }
        }

        public string Text2
        {
            get { return this.textBox2.Text; }
            set
            {
                this.setProgressText(value, true);
            }
        }

        #endregion

        #region Methods

        public ProgressInfoForm()
        {
            InitializeComponent();
        }


        public string Info
        {
            get
            {
                return this.infoTextBox.Text;
            }
        }

        private delegate void AddInfoDelegate(string info);
        public void AddInfo(string info)
        {
            if (this.InvokeRequired)
            {
                var d = new AddInfoDelegate(this.AddInfo);
                this.Invoke(d, info);
            }
            else
            {
                if (string.IsNullOrEmpty(this.infoTextBox.Text))
                    this.infoTextBox.Text = info;
                else
                    this.infoTextBox.Text += "\r\n" + info;
            }
        }

        private void setSecondProgressEnable(bool value)
        {
            if (this.InvokeRequired)
            {
                var d = new Action<bool>(this.setSecondProgressEnable);
                this.Invoke(d, value);
            }
            else
            {
                this.secondProgressEnable = value;
                this.textBox2.Visible = value;
                this.progressBar2.Visible = value;
            }
        }

        private void setProgressText(string text, bool secondProgress = false)
        {
            if (this.InvokeRequired)
            {
                var d = new Action<string, bool>(this.setProgressText);
                this.Invoke(d, text, secondProgress);
            }
            else
            {
                TextBox tb = secondProgress ? this.textBox2 : this.textBox1;
                tb.Text = text;
            }
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (this.process != null)
                this.process.Invoke(this, this);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (this.process != null)
                this.backgroundWorker.RunWorkerAsync();
        }

        public void ResetProgress(bool secondProgress = false)
        {
            if (this.InvokeRequired)
            {
                var d = new Action<bool>(this.ResetProgress);
                this.Invoke(d, secondProgress);
            }
            else
            {
                TextBox tb = secondProgress ? this.textBox2 : this.textBox1;
                ProgressBar pb = secondProgress ? this.progressBar2 : this.progressBar1;

                tb.Text = "";
                pb.Maximum = 100;
                pb.Minimum = 0;
                pb.Step = 1;
                pb.Value = 0;
            }
        }

        public void PerformStep(int step, bool secondProgress = false)
        {
            if (this.InvokeRequired)
            {
                var d = new Action<int, bool>(this.PerformStep);
                this.Invoke(d, step, secondProgress);
            }
            else
            {
                ProgressBar bar = secondProgress ? this.progressBar2 : this.progressBar1;
                bar.Value += step;
            }
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
            if (this.completedProcess != null)
                this.completedProcess.Invoke();
        }

        public static void Show(string title, ProgressProcessDelegate process, Action completedProcess)
        {
            progressForm = new ProgressInfoForm();
            progressForm.Text = title;
            progressForm.process = process;
            progressForm.completedProcess = completedProcess;
            progressForm.ShowDialog();
        }

        #endregion

        #region Nested Types

        public delegate void ProgressProcessDelegate(IProgressForm progressForm, IInfo infoControl);

        #endregion

    }
}
