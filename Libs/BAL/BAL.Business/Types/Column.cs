using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Types;

namespace BAL.Types
{
    public class Column : INotifyPropertyChanged, ISupportInitialize, IComparer<Column>, ICloneable
    {
        #region Fields

        private string name;
        private string headerText;
        private int width;
        private TextAlign textAlign;
        private TextAlign headerTextAlign;
        private bool initializing;
        private bool visible;
        private bool? readOnly;
        private int order;
        private string format;
        private PropertyPath propertyPath;
        private PropertyDescriptorPath propertyDescriptorPath;
        
        #endregion

        #region Properties

        public string Name
        {
            get { return this.name; }
            set
            {
                this.name = value;
                this.OnPropertyChanged("Name");
            }
        }

        public string PropertyName
        {
            get
            {
                if (this.propertyDescriptorPath != null)
                    return this.propertyDescriptorPath.Last.Name;
                if (this.propertyPath != null)
                    return this.propertyPath.Last.Name;
                return this.name;
            }
        }

        public string HeaderText
        {
            get { return this.headerText; }
            set
            {
                this.headerText = value;
                this.OnPropertyChanged("HeaderText");
            }
        }

        public TextAlign HeaderTextAlign
        {
            get { return this.headerTextAlign; }
            set
            {
                this.headerTextAlign = value;
                this.OnPropertyChanged("HeaderTextAlign");
            }
        }

        public Type Type
        {
            get
            {
                if (propertyDescriptorPath != null)
                    return this.propertyDescriptorPath.Last.PropertyType;
                if (this.propertyPath != null)
                    return this.propertyPath.Last.PropertyType;
                return typeof(string);
            }
        }
        public int Width
        {
            get { return this.width; }
            set
            {
                this.width = value;
                this.OnPropertyChanged("Width");
            }
        }

        public TextAlign TextAlign
        {
            get { return this.textAlign; }
            set
            {
                this.textAlign = value;
                this.OnPropertyChanged("TextAlign");
            }
        }

        public bool Visible
        {
            get { return this.visible; }
            set
            {
                this.visible = value;
                this.OnPropertyChanged("Visible");
            }
        }

        public bool ReadOnly
        {
            get { return this.readOnly == null ? true : this.readOnly.Value; }
            set
            { 
                this.readOnly = value;
                this.OnPropertyChanged("ReadOnly");
            }
        }

        public int Order
        {
            get { return this.order; }
            set
            {
                this.order = value;
                this.OnPropertyChanged("Order");
            }
        }

        public string Format
        {
            get { return this.format; }
            set
            {
                this.format = value;
                this.OnPropertyChanged("Format");
            }
        }

        public PropertyPath PropertyPath
        {
            get { return this.propertyPath; }
            set
            {
                this.propertyPath = value;
                this.OnPropertyChanged("PropertyPath");

            }
        }

        public PropertyDescriptorPath PropertyDescriptorPath
        {
            get { return this.propertyDescriptorPath; }
            set
            {
                this.propertyDescriptorPath = value;
                this.OnPropertyChanged("PropertyDescryptorPath");
            }
        }

        public string PropertyPathStr
        {
            get
            {
                if (PropertyDescriptorPath != null)
                    return propertyDescriptorPath.ToString();
                else if (PropertyPath != null)
                    return PropertyPath.ToString();
                return null;
            }
        }

        #endregion


        #region Methods

        public Column()
        {
            this.readOnly = true;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (!this.initializing && this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void BeginInit()
        {
            if (!this.initializing)
                this.initializing = true;
        }

        public void EndInit()
        {
            if (this.initializing)
                this.initializing = false;
        }

        public int Compare(Column c1, Column c2)
        {
            return c1.Order.CompareTo(c2.Order);
        }

        public override string ToString()
        {
            return this.Name + " (" + this.Order + ")";
        }

        public Column Clone()
        {
            return new Column()
            {
                name = this.name,
                headerText = this.headerText,
                headerTextAlign = this.headerTextAlign,
                order = this.order,
                propertyPath = this.propertyPath,
                propertyDescriptorPath = this.propertyDescriptorPath,
                textAlign = this.textAlign,
                visible = this.visible,
                width = this.width,
                readOnly = this.readOnly
            };
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion


    }
}
