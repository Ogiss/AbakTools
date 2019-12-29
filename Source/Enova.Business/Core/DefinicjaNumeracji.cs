using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.Business.Old;

namespace Enova.Old.Core
{
    [Obsolete("Niekompletny kod")]
    public class DefinicjaNumeracji : SubRow, IEnumerable
    {
        #region Fields

        public const int WzorLength = 0x80;

        #endregion

        #region Properties

        public bool PodczasEdycji
        {
            get { return GetFieldValue<bool>("PodczasEdycji"); }
            set { SetFieldValue("PodczasEdycji", value); }
        }
        public string Wzor
        {
            get { return GetFieldValue<string>("Wzor"); }
            set { SetFieldValue("Wzor", value); }
        }

        #endregion

        #region Methods

        public DefinicjaNumeracji() { }
        public DefinicjaNumeracji(IRow parent, string name) : base(parent, name) { }

        public bool IsComponent(string component)
        {
            if (component == "")
            {
                return true;
            }
            component = component.ToUpper();
            foreach (string str in (IEnumerable)this)
            {
                int length = str.IndexOfAny(new char[] { '.', ':' });
                if (length == -1)
                {
                    length = str.Length;
                }
                if (component == str.Substring(0, length).Trim().ToUpper())
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsReadOnlyPodczasZapisu()
        {
//            if (!this.IsReadOnly())
//            {
                return !(base.Parent is INumeracjaPodczasZapisu);
 //           }
//            return true;
        }



        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Wzor.Split(new char[] { '/' }).GetEnumerator();
        }

        #endregion
    }
}
