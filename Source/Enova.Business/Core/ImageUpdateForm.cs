using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Enova.Business.Old.DB.Web;

[assembly: BAL.Forms.MenuAction("Admin\\Aktualizacja zdjęć", typeof(Enova.Business.Old.Core.ImageUpdateForm), Priority = 1210, MenuAction = BAL.Forms.MenuActionsType.OpenFormModal)]

namespace Enova.Business.Old.Core
{
    public partial class ImageUpdateForm : Form
    {

        bool continueProgress;
        object synch = new object();

        public ImageUpdateForm()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            continueProgress = true;
            backgroundWorker.RunWorkerAsync();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            continueProgress = false;
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            using (var dc = new WebContext())
            {
                var images = dc.Zdjecia.Where(i=>i.ImageBytes == null).ToList();

                setProgressMaximum(images.Count);

                foreach (var i in images)
                {
                    if (!continueProgress)
                        break;
                    var img = i.GetImage();

                    setText(i.Produkt.Kod + " - " + i.Produkt.Nazwa, i.Legenda + (img != null ? " - (" + img.Width + "x" + img.Height + ")" : ""));
                    performStep();
                }
            }
        }

        private void setProgressMaximum(int maximum)
        {
            if (this.InvokeRequired)
            {
                var d = new Action<int>(setProgressMaximum);
                this.Invoke(d, maximum);
            }
            else
            {
                progressBar.Value = 0;
                progressBar.Maximum = maximum;
                progressBar.Step = 1;
            }
        }

        private void setText(string t1, string t2)
        {
            if (this.InvokeRequired)
            {
                var d = new Action<string, string>(setText);
                this.Invoke(d, t1, t2);
            }
            else
            {
                textBox1.Text = t1;
                textBox2.Text = t2;
            }
        }

        private void performStep()
        {
            if (this.InvokeRequired)
            {
                var d = new Action(performStep);
                this.Invoke(d);
            }
            else
            {
                this.progressBar.PerformStep();
                this.Refresh();
            }
        }
    }
}
