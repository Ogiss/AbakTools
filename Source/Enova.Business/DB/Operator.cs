using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Security.Cryptography;
using Enova.Old.Tools;

namespace Enova.Business.Old.DB
{
    public partial class Operator : IGuidedRow , IDbContext, ISessionable, ISetSession
    {
        #region Fields

        private static readonly MACTripleDES passwordDES;
        private static readonly byte[] passwordKey;
        private static readonly MACTripleDES trustingDES;
        private static readonly byte[] trustingGuidHash;
        private static readonly byte[] trustingHash;
        private static readonly byte[] trustingHash2;
        private static readonly byte[] trustingKey;

        private Session session;

        #endregion

        #region Properties

        Session ISetSession.Session
        {
            set { this.session = value; }
        }

        public Session Session
        {
            get
            {
                if (this.session != null)
                    return this.session;
                if (this.DataContext != null && this.DataContext is ISessionable)
                    return ((ISessionable)this.DataContext).Session;
                return null;
            }
        }

        public bool IsAccountLocked
        {
            get
            {
                return this.FullName.EndsWith(".LOCK");
            }
            /*
            set
            {
                this.setFullName(this.FullName, this.IsOperatorNet, value);
                this.Extension.ResetInvalidLogin();
            }
             */
        }
 

 


        #endregion

        #region Methods

        static Operator()
        {
            passwordKey = new byte[] { 
                0x36, 0x5e, 0xb9, 0xb8, 0x7b, 100, 0xdf, 0xd5, 0xfd, 0x33, 5, 0x57, 0x1b, 0x40, 0x58, 0x6f, 
                0x43, 0x5e, 0x3f, 0x52, 0xaf, 0x81, 0x86, 0xd3
            };
            trustingKey = new byte[] { 
                0xea, 0x36, 0x3a, 0x7b, 0x62, 0xdf, 0xbd, 0xa3, 190, 11, 0x35, 90, 0x22, 0x52, 0xc4, 0x2b, 
                0, 0xde, 0x4b, 0x22, 250, 0x9c, 0xc6, 0x87
            };
            trustingHash = new byte[] { 0xb8, 0xd5, 0x3f, 0x76, 0x38, 0xb5, 8, 0x1c };
            trustingHash2 = new byte[] { 0x26, 0x1d, 0x19, 0x79, 0x48, 0xd9, 0x2f, 0x8f };
            trustingGuidHash = new byte[] { 0x61, 0x8a, 0x43, 0x9c, 0x9c, 0xf1, 0xb8, 0x6b };
            passwordDES = new MACTripleDES(passwordKey);
            trustingDES = new MACTripleDES(trustingKey);
        }

        internal string GetPasswordHash(string password)
        {
            byte[] buffer = this.Session.DatabaseGuid.ToByteArray();
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] buffer3 = this.Guid.ToByteArray();
            byte[] array = new byte[(buffer.Length + bytes.Length) + buffer3.Length];
            buffer.CopyTo(array, 0);
            bytes.CopyTo(array, buffer.Length);
            buffer3.CopyTo(array, (int)(buffer.Length + bytes.Length));
            return CoreTools.ByteArrayToString(passwordDES.ComputeHash(array));
        }

        internal bool Trusting(string s)
        {
            bool flag = true;
            byte[] buffer = trustingDES.ComputeHash(Encoding.Unicode.GetBytes(this.Session.DatabaseGuid.ToString().ToLower()));
            for (int i = 0; i < trustingGuidHash.Length; i++)
            {
                if (trustingGuidHash[i] != buffer[i])
                {
                    flag = false;
                }
            }
            if (flag)
            {
                return true;
            }
            byte[] buffer2 = trustingDES.ComputeHash(Encoding.Unicode.GetBytes(s));
            for (int j = 0; j < trustingHash2.Length; j++)
            {
                if (trustingHash2[j] != buffer2[j])
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsValidPassword(string password)
        {
            return (this.Password == this.GetPasswordHash(password));
        }

        #endregion

        #region IDataContex

        ObjectContext IDbContext.DbContext { get; set; }
        public EnovaContext DataContext
        {
            get { return (EnovaContext)((IDbContext)this).DbContext; }
        }

        #endregion

        #region IRow Implementation

        IRow IRow.Parent
        {
            get { return null; }
        }
        IRow IRow.Root
        {
            get { return this; }
        }
        string IRow.Prefix
        {
            get { return ""; }
        }

        public Operators Table
        {
            get
            {
                return BusinessModule.GetInstance(this.Session).Operators;
            }
        }

        ITable IRow.Table
        {
            get
            {
                return this.Table;
            }
        }
        public RowState State
        {
            get { return this.GetRowState(); }
        }

        public bool IsLive
        {
            get { return this.GetIsLive(); }
        }

        public bool IsReadOnly()
        {
            return false;
        }

        #endregion

    }
}
