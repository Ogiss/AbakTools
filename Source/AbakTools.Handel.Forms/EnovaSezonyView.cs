using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbakTools.Handel.Forms
{
    public class EnovaSezonyView : BAL.Business.View
    {
        #region Fields

        private List<string> sezony;

        #endregion

        #region Properties

        public override System.Collections.IList Rows
        {
            get
            {
                if (sezony == null)
                    initSezony();
                return sezony;
            }
        }

        #endregion

        #region Methods

        private void initSezony()
        {
            sezony = new List<string>();
            var service = Enova.API.EnovaService.Instance;
            using (var session = service.CreateSession())
            {
                var bm = session.GetModule<Enova.API.Business.BusinessModule>();
                foreach (var s in bm.Dictionary["F.SEZON"].OrderBy(r => r.Value))
                {
                    sezony.Add(s.Value);
                }
            }
        }

        public override Type GetDataType()
        {
            return typeof(string);
        }

        #endregion
    }
}
