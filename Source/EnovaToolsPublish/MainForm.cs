using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.IO;

namespace EnovaToolsPublish
{
    public partial class MainForm : Form
    {
        private string updatePath = null;
        private Version lastVersion = null;
        private Version currentVersion = null;

        private Configuration config;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            config = ConfigurationManager.OpenExeConfiguration("EnovaToolsExplorer.exe");

            KeyValueConfigurationElement el = config.AppSettings.Settings["UpdatePath"];
            if (el != null)
            {
                updatePath = el.Value;
                updatePathTextBox.Text = updatePath;
                getLastVersion();
                getCurrentVersion();
            }
        }

        private void createDirectory(string path)
        {

        }

        private void getLastVersion()
        {
            if (Directory.Exists(updatePath))
            {
                var dirs = Directory.GetDirectories(updatePath);
                if (dirs.Count() > 0)
                {
                    string path = dirs.Max();
                    string[] parts = path.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
                    
                    lastVersion = new Version(parts.Last());
                    lastTextBox.Text = lastVersion.ToString();
                }
                else
                {
                    lastTextBox.Text = "Brak";
                }
            }
            else
            {
                lastTextBox.Text = "Brak";
                DirectoryInfo di = new DirectoryInfo(updatePath);
                di.Create();
            }
                
        }

        private void getCurrentVersion()
        {
            //currentVersion = System.Reflection.Assembly.LoadFile(Directory.GetCurrentDirectory() + "\\EnovaToolsStart.exe").GetName().Version;
            currentVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            if (currentVersion != null)
            {
                currentTextBox.Text = currentVersion.ToString();
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void publishButton_Click(object sender, EventArgs e)
        {
            if (currentVersion != null)
            {
                if (lastVersion == null || lastVersion < currentVersion)
                {
                    Directory.CreateDirectory(updatePath + "\\" + currentVersion.ToString());
                    var files = Directory.GetFiles(Directory.GetCurrentDirectory());
                    this.Enabled = false;
                    this.Cursor = Cursors.WaitCursor;
                    foreach (var f in files)
                    {
                        if (!f.EndsWith(".config") && !f.EndsWith(".Config") && !f.EndsWith(".pdb") && !f.Contains(".vshost.exe"))
                            File.Copy(f, updatePath + "\\" + currentVersion.ToString() + "\\" + Path.GetFileName(f));
                    }
                    this.Cursor = Cursors.Default;
                    this.Enabled = true;
                    lastVersion = currentVersion;
                    lastTextBox.Text = lastVersion.ToString();
                }
                else
                {
                    MessageBox.Show("Wersja opublikowana jest nowsza lub równa obecnej wersji");
                }
            }
        }
    }
}
