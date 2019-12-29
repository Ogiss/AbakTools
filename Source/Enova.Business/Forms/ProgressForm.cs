using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Enova.Business.Old.Forms
{
    public partial class ProgressForm : Form
    {
        #region Fields

        IProgressFormHandler progressFormHandler;
        object result = null;

        #endregion


        #region Properties

        public object Result
        {
            get { return result; }
        }

        [Browsable(true)]
        public bool SupportsCancellation
        {
            get { return backgroundWorker.WorkerSupportsCancellation; }
            set
            {
                backgroundWorker.WorkerSupportsCancellation = value;
                cancelButton.Visible = value;
            }
        }

        public bool Text1Visible
        {
            get { return textBox1.Visible; }
            set { textBox1.Visible = true; }
        }

        public bool Text2Visible
        {
            get { return textBox2.Visible; }
            set { textBox2.Visible = value; }
        }

        #endregion

        #region Methods

        public ProgressForm()
        {
            InitializeComponent();
        }

        public ProgressForm(string title, IProgressFormHandler handler)
        {
            InitializeComponent();
            this.Text = title;
            this.progressFormHandler = handler;
        }

        public ProgressForm(IProgressFormHandler handler)
        {
            InitializeComponent();
            this.Text = "AbakTools - Proszę czekać";
            this.progressFormHandler = handler;
        }


        public static ProgressForm StartProgress(string title, IProgressFormHandler handler)
        {
            ProgressForm form = new ProgressForm(title, handler);
            form.ShowDialog();
            return form;
        }

        public static void StartProgress(IProgressFormHandler handler)
        {
            new ProgressForm(handler).ShowDialog();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = (BackgroundWorker)sender;
            if (e.Argument != null)
            {
                IProgressFormHandler handler = (IProgressFormHandler)e.Argument;
                handler.BackgroundWorker = bw;
                handler.StartProcess();
                e.Result = handler.Result;

            }
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressHandlerArgs args = (ProgressHandlerArgs)e.UserState;
            progressBar1.Visible = args.Progress1Visible;
            textBox1.Visible = args.Progress1Visible;
            textBox1.Text = args.TextProgress1;
            progressBar1.Minimum = args.MinProgress1;
            progressBar1.Maximum = args.MaxProgress1;
            progressBar1.Step = args.StepProgress1;
            progressBar1.Value = args.ValueProgress1;

            progressBar2.Visible = args.Progress2Visible;
            textBox2.Visible = args.Progress2Visible;
            textBox2.Text = args.TextProgress2;
            progressBar2.Minimum = args.MinProgress2;
            progressBar2.Maximum = args.MaxProgress2;
            progressBar2.Step = args.StepProgress2;
            progressBar2.Value = args.ValueProgress2;
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.result = e.Result;
            this.Close();
            this.progressFormHandler.FinishProcess();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (backgroundWorker.WorkerSupportsCancellation)
                backgroundWorker.CancelAsync();
        }



        private void ProgressForm_Load(object sender, EventArgs e)
        {
            if (progressFormHandler != null)
                backgroundWorker.RunWorkerAsync(progressFormHandler);
        }
        #endregion
    }

    public class ProgressHandlerArgs
    {
        public bool Progress1Visible { get; set; }
        public int MinProgress1 { get; set; }
        public int MaxProgress1 { get; set; }
        public int StepProgress1 { get; set; }
        public int ValueProgress1 { get; set; }
        public string TextProgress1 { get; set; }

        public bool Progress2Visible { get; set; }
        public int MinProgress2 { get; set; }
        public int MaxProgress2 { get; set; }
        public int StepProgress2 { get; set; }
        public int ValueProgress2 { get; set; }
        public string TextProgress2 { get; set; }
    }

    public interface IProgressFormHandler
    {
        object SyncRoot { get; set; }
        object Result { get; }
        BackgroundWorker BackgroundWorker { get; set; }
        void StartProcess();
        void FinishProcess();
    }

    public class ProgressFormHandler : IProgressFormHandler
    {
        #region Fields
        ProgressHandlerArgs args = new ProgressHandlerArgs()
        {
            Progress1Visible = true,
            Progress2Visible = true,
            MinProgress1 = 0,
            MinProgress2 = 0,
            MaxProgress1 = 100,
            MaxProgress2 = 100,
            StepProgress1 = 1,
            StepProgress2 = 1,
            ValueProgress1 = 0,
            ValueProgress2 = 0,
            TextProgress1 = "",
            TextProgress2 = ""
        };

        protected object result = null;

        #endregion

        #region Properties

        public ProgressHandlerArgs ProgressArgs
        {
            get { return args; }
        }

        public object Result
        {
            get { return result; }
        }

        public object SyncRoot { get; set; }

        public BackgroundWorker BackgroundWorker { get; set; }

        #endregion

        #region Methods

        public virtual void StartProcess() { }

        public void ProgressChanges()
        {
            BackgroundWorker.ReportProgress(0, ProgressArgs);
        }

        public virtual void FinishProcess() { }

        public void PerformStep1()
        {
            args.ValueProgress1++;
            ProgressChanges();
        }

        public void PerformStep1(string text)
        {
            args.TextProgress1 = text;
            args.ValueProgress1++;
            ProgressChanges();
        }

        public void PerformStep2()
        {
            args.ValueProgress2++;
            ProgressChanges();
        }


        #endregion
    }
}
