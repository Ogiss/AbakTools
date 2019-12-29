using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Configuration;
using System.Diagnostics;



namespace EnovaToolsStart
{
    public partial class MainForm : Form
    {
        Version version = null;
        Thread processThread = null;
        bool updated = false;
        Process[] enovaToolsProcesses = null;

    
        
        public MainForm()
        {
            InitializeComponent();
            this.Visible = false;
            
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Hide();
            getEnovaToolsProcess();
            getVersion();
            processThread = new Thread(new ThreadStart(process));
            processThread.Start();
        }

        private void getEnovaToolsProcess()
        {
            enovaToolsProcesses = Process.GetProcesses().Where(p => p.ProcessName == "EnovaToolsExplorer").ToArray();
        }

        private void getVersion()
        {
            version = Assembly.GetExecutingAssembly().GetName().Version;
            nameLabel.Text = "Aktualna wersja: " + version.ToString();
        }

        private void checkUpdate()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration("EnovaToolsExplorer.exe");
            
            if (config != null)
            {
                KeyValueConfigurationElement el = config.AppSettings.Settings["UpdatePath"];
                if (el != null)
                {
                    if (Directory.Exists(el.Value))
                    {
                        string[] dirs = Directory.GetDirectories(el.Value);
                        if (dirs.Count() > 0)
                        {
                            Version ver = new Version(dirs.Max().Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries).Last());
                            if (ver > version)
                            {
                                DialogResult result = MessageBox.Show("Istnieje nowsza wersja EnovaTools. Czy chcesz zaktualizować do nowej wersji?",
                                    "AbakTools", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                                if (result == DialogResult.Yes)
                                {
                                    setInfo("Nowa wersja: " + ver.ToString());

                                    var files = Directory.GetFiles(el.Value + "\\" + ver.ToString());

                                    setMaximum(files.Count());
                                    showMainForm();

                                    if (enovaToolsProcesses.Count() > 0)
                                    {
                                        foreach (var p in enovaToolsProcesses)
                                            p.Kill();

                                        enovaToolsProcesses = null;
                                    }


                                    string cdir = Directory.GetCurrentDirectory();

                                    foreach (var f in files)
                                    {
                                        string file = Path.GetFileName(f);
                                        string ext = Path.GetExtension(f);
                                        if (file != "users.xml" && (file.Length <= 15 || file.Substring(0, 15) != "EnovaToolsStart") && !file.EndsWith(".Config"))
                                        {
                                            File.Copy(f, cdir + "\\" + file, true);
                                        }
                                        performStep();
                                    }

                                    /*
                                    el = config.AppSettings.Settings["Version"];
                                    if (el == null)
                                    {
                                        el = new KeyValueConfigurationElement("Version", ver.ToString());
                                        config.AppSettings.Settings.Add(el);
                                    }
                                    else
                                    {
                                        el.Value = ver.ToString();
                                    }
                                    //config.Save(ConfigurationSaveMode.Modified);
                                    */

                                    updated = true;

                                }
                            }
                        }
                    }
                }
            }
        }

        private delegate void setMaximumHandler(int maximum);
        private void setMaximum(int maximum)
        {
            if (progressBar.InvokeRequired)
            {
                var d = new setMaximumHandler(setMaximum);
                progressBar.Invoke(d, new object[] { maximum });
            }
            else
            {
                progressBar.Minimum = 0;
                progressBar.Maximum = maximum;
                progressBar.Value = 0;
                progressBar.Step = 1;
            }
        }

        private object performStepObj = new object();
        private delegate void performStepHandler();
        private void performStep()
        {
            if (progressBar.InvokeRequired)
            {
                var d = new performStepHandler(performStep);
                progressBar.Invoke(d);
            }
            else
            {
                lock (performStepObj)
                {
                    if (progressBar.Value < progressBar.Maximum)
                        progressBar.PerformStep();
                }
            }
        }

        private delegate void setInfoHandler(string info);
        private void setInfo(string info)
        {
            if (infoTextBox.InvokeRequired)
            {
                var d = new setInfoHandler(setInfo);
                infoTextBox.Invoke(d, new object[] { info });
            }
            else
            {
                infoTextBox.Text = info;
            }
        }

        private delegate void showMainFormDelegate();
        private void showMainForm()
        {
            if (Program.MainForm.InvokeRequired)
            {
                var d = new showMainFormDelegate(showMainForm);
                Program.MainForm.Invoke(d);
            }
            else
            {
                Program.MainForm.Show();
                Program.MainForm.StartPosition = FormStartPosition.CenterScreen;
                Program.MainForm.WindowState = FormWindowState.Normal;
                Program.MainForm.Refresh();
            }
        }

        private void startEnovaTools()
        {
            if (enovaToolsProcesses!=null && enovaToolsProcesses.Count() > 0)
            {
                foreach (var p in enovaToolsProcesses)
                    p.Kill();

                enovaToolsProcesses = null;
            }

            string path = Directory.GetCurrentDirectory() + "\\EnovaToolsExplorer.exe";
            if (File.Exists(path))
            {
                var startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.FileName = "EnovaToolsExplorer.exe";
                if (updated)
                    startInfo.Arguments = "update";
                startInfo.WorkingDirectory = Directory.GetCurrentDirectory();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized;
                startInfo.ErrorDialog = true;

                var proc = new System.Diagnostics.Process();
                proc.StartInfo = startInfo;
                proc.Start();
            }
            else
            {
                MessageBox.Show("Nie mogę odnaleść pliku EnovaToolsExplorer.exe");
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
        }

        private void process()
        {
            checkUpdate();
            startEnovaTools();
            //System.Threading.Thread.Sleep(1000);
            closeMainForm();
        }

        private delegate void closeMainFormHandler();
        private void closeMainForm()
        {
            if (Program.MainForm.InvokeRequired)
            {
                var d = new closeMainFormHandler(closeMainForm);
                Program.MainForm.Invoke(d, null);
            }
            else
            {
                Program.MainForm.Close();
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

    }
}
