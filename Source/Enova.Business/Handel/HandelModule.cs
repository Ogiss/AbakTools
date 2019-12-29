using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.Business.Old;
using Enova.Business.Old.DB;
using Enova.Old.Core;

namespace Enova.Old.Handel
{
    public partial class HandelModule : EnovaModule
    {
        #region Fields

        private DefDokHandlowych defDokHandlowychTable;
        private RelacjeHandlowe relacjeHandloweTable;
        private DefRelHandlowych defRelHandlowychTable;
        private PozRelHandlowej pozRelacjiHandlowejTable;
        private PozycjeDokHan pozycjeDokHanTable;
        private DokHandlowe dokHandloweTable;

        #endregion

        #region Properties

        public CoreModule Core
        {
            get { return CoreModule.GetInstance(this.DataContext); }
        }

        public DefDokHandlowych DefDokHandlowych
        {
            get { return this.defDokHandlowychTable; }
        }

        public RelacjeHandlowe RelacjeHandlowe
        {
            get { return this.relacjeHandloweTable; }
        }

        public DefRelHandlowych DefRelHandlowych
        {
            get { return this.defRelHandlowychTable; }
        }

        public PozRelHandlowej PozRelHandlowej
        {
            get { return this.pozRelacjiHandlowejTable; }
        }

        public PozycjeDokHan PozycjeDokHan
        {
            get { return this.pozycjeDokHanTable; }
        }

        public DokHandlowe DokHandlowe
        {
            get { return this.dokHandloweTable; }
        }

        #endregion

        #region Methods

        public HandelModule(Session session)
            : base(session, "Handel")
        {
            this.defDokHandlowychTable = new DefDokHandlowych();
            this.relacjeHandloweTable = new RelacjeHandlowe();
            this.defRelHandlowychTable = new DefRelHandlowych();
            this.pozRelacjiHandlowejTable = new PozRelHandlowej();
            this.pozycjeDokHanTable = new PozycjeDokHan();
            this.dokHandloweTable = new DokHandlowe();

            this.AddTable(this.defDokHandlowychTable);
            this.AddTable(this.relacjeHandloweTable);
            this.AddTable(this.defRelHandlowychTable);
            this.AddTable(this.pozRelacjiHandlowejTable);
            this.AddTable(this.pozycjeDokHanTable);
            this.AddTable(this.dokHandloweTable);
        }

        public static HandelModule GetInstance(ISessionable session)
        {
            if (session != null && session.Session != null)
                return (HandelModule)session.Session.Modules[typeof(HandelModule)];
            return null;
        }

        #endregion
    }
}
