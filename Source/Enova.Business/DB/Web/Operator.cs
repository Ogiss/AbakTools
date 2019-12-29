using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.DB.Web
{
    public partial class Operator
    {
        #region Fields

        public static Operator CurrentOperator;

        #endregion

        #region Properties

        public AbakTools.Business.OperatorPrawaDostepu PrawaDostepu
        {
            get { return (AbakTools.Business.OperatorPrawaDostepu)PrawaDostepuInt; }
            set { PrawaDostepuInt = (int)value; }
        }

        #endregion

        #region Methods

        public Operator GetOperator(WebContext dc)
        {
            return dc.Operatorzy.Where(r => r.ID == this.ID).FirstOrDefault();
        }

        public void SetPrawaDostepu(AbakTools.Business.OperatorPrawaDostepu prawaDostepu)
        {
            if ((PrawaDostepu & prawaDostepu) != prawaDostepu)
                PrawaDostepu |= prawaDostepu;
        }

        public bool CheckPrawaDostepu(AbakTools.Business.OperatorPrawaDostepu prawaDostepu)
        {
            return (PrawaDostepu & prawaDostepu) == prawaDostepu;
        }

        public void SetPassword(string password)
        {
            if (password == null)
                password = string.Empty;

            string str = Nazwa + "|" + password;
            string encrypted = AbakTools.Business.Tools.Encrypt(str);
            if (string.IsNullOrEmpty(this.Haslo) || !this.Haslo.Equals(encrypted))
                this.Haslo = AbakTools.Business.Tools.Encrypt(str);
        }

        public override string ToString()
        {
            return Nazwa;
        }


        #endregion
    }
}
