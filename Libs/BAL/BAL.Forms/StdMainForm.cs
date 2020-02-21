using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BAL.Forms
{
    public partial class StdMainForm : BAL.Forms.FormWithTabs, IMainForm
    {
        #region Fields

        private Control currentForm;
        private static StdMainForm instance;

        #endregion

        #region Properties

        public virtual string Title
        {
            get { return this.Text; }
            set { this.Text = value; }
        }

        public virtual string StatusLineText { get; set; }


        public static StdMainForm Instance
        {
            get { return instance; }
        }

        #endregion

        public StdMainForm()
        {
            instance = this;
            InitializeComponent();
        }

        public void OpenForm(Type formType, string title)
        {
            if (formType != null)
            {
                if (currentForm != null)
                {
                    this.TabControl.SelectedTab.Controls.Remove(currentForm);
                    currentForm.Dispose();
                    currentForm = null;
                }

                var form = formType.GetConstructor(new Type[0]).Invoke(new object[0]);
                if (typeof(Form).IsAssignableFrom(form.GetType()))
                {
                    Form f = form as Form;
                    f.TopLevel = false;
                    f.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    f.Dock = DockStyle.Fill;
                }
                this.TabControl.SelectedTab.Controls.Add((Control)form);
                this.TabControl.SelectedTab.Text = title;
                ((Control)form).Show();
                currentForm =(Control)form;
                this.TabControl.SelectedTab.Select();
                ((Control)form).Select();
            }
        }


    }
}
