using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Forms.Services
{
    public class View<T> : IEnumerable<T>
        where T : Enova.API.Business.Row
    {
        string filter;
        private Enova.API.Business.View view;

        public View(Enova.API.Business.View view)
        {
            this.view = view;
            this.filter = "";
        }

        public static View<T> Create<T>(Enova.API.Business.View view)
            where T : Enova.API.Business.Row
        {
            return new View<T>(view);
        }

        public View<T> And(string and)
        {
            if (filter != "")
                filter += " AND ";
            filter += and;
            return this;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (filter != "")
                view.Filter = filter;
            return view.Cast<T>().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
