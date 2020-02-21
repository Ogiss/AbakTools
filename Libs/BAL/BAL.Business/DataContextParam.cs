using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Business
{
    public class DataContextParam
    {
        #region Fields

        private string name;
        private string label;
        private BAL.Types.PropertyPath propertyPath;
        private object defaultValue;
        private Type controlType;

        #endregion

        #region Properties

        public string Name
        {
            get { return this.name; }
        }

        public string Label
        {
            get { return this.label; }
        }

        public BAL.Types.PropertyPath PropertyPath
        {
            get { return this.propertyPath; }
        }

        public object DefaulValue
        {
            get { return this.defaultValue; }
        }

        public Type ControlType
        {
            get { return this.controlType; }
            set { this.controlType = value; }
        }

        #endregion

        #region Methods

        public DataContextParam(string name, string label, BAL.Types.PropertyPath propertyPath, object defaultValue = null)
        {
            this.name = name;
            this.label = label;
            this.propertyPath = propertyPath;
            this.defaultValue = defaultValue;
        }

        #endregion
    }
}
