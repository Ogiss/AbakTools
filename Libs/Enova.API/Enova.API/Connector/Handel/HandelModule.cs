using System;
using System.Reflection;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

[assembly: Enova.API.Connector.Module("Handel", typeof(Enova.API.Connector.Handel.HandelModule), typeof(Enova.API.Handel.HandelModule))]

namespace Enova.API.Connector.Handel
{
    internal class HandelModule : Business.Module, Enova.API.Handel.HandelModule
    {
        #region Fields

        private API.Handel.DefDokHandlowych defDokHandlowych;
        private API.Handel.DokHandlowe dokHandlowe;
        private PozycjeDokHan tablePozycjeDokHan;
        private DefRelHandlowych tableDefRelHandlowych;

        #endregion

        #region Properties


        public API.Handel.DefDokHandlowych DefDokHandlowych
        {
            get
            {
                if (this.defDokHandlowych == null)
                    this.defDokHandlowych = new DefDokHandlowych() { EnovaObject = GetEnovaTable("DefDokHandlowych"), module = this };
                return this.defDokHandlowych;
            }
        }

        public API.Handel.DokHandlowe DokHandlowe
        {
            get
            {
                if (this.dokHandlowe == null)
                    this.dokHandlowe = new DokHandlowe() { EnovaObject = GetEnovaTable("DokHandlowe"), module = this };
                return this.dokHandlowe;
            }
        }

        public API.Handel.PozycjeDokHan PozycjeDokHan
        {
            get
            {
                if (this.tablePozycjeDokHan == null)
                    this.tablePozycjeDokHan = new PozycjeDokHan() { EnovaObject = GetEnovaTable("PozycjeDokHan"), module = this };
                return this.tablePozycjeDokHan;
            }
        }

        public API.Handel.DefRelHandlowych DefRelHandlowych
        {
            get
            {
                if (this.tableDefRelHandlowych == null)
                    this.tableDefRelHandlowych = new DefRelHandlowych() { EnovaObject = GetEnovaTable("DefRelHandlowych"), module = this };
                return this.tableDefRelHandlowych;
            }
        }

        #endregion

        #region Methods

        public HandelModule(Business.Session session) : base(session, "Handel") { }

        public HandelModule() : this(null) { }

        public static object Execute(object obj, string props)
        {
            foreach (string str in props.Split(new char[] { '.' }))
            {
                if (obj == null)
                {
                    return obj;
                }
                obj = TypeDescriptor.GetProperties(obj).Find(str, true).GetValue(obj);
            }
            return obj;
        }

        public void DrukujDokument(Form form, Guid guid, string template,
            Enova.API.Printer.Destinations destination = Enova.API.Printer.Destinations.Preview,
            string outputFile = null
            )
        {
            using (var fs = new StreamWriter("enova.error.txt"))
            {
                var dh = GetObjValue(GetValue("DokHandlowe"), "Item", new Type[] { typeof(Guid) }, new object[] { guid });
                
                if (dh != null)
                {
                    fs.WriteLine("1: Dokument:"+dh.ToString());
                    Type t = Type.GetType("Soneta.Business.Context, Soneta.Business");
                    var context = CallObjMethod(
                        t.GetField("Empty").GetValue(null),
                        "Clone",
                        new Type[] { Type.GetType("Soneta.Business.Session, Soneta.Business") },
                        new object[] { ((Business.Session)Session).EnovaObject });

                    CallObjMethod(context, "Set", new Type[] { typeof(object) }, new object[] { dh });
                    t = Type.GetType("Soneta.Printer.AspGenerator, Soneta.Printer");
                    var generator = t.GetConstructor(new Type[0]).Invoke(new object[0]);
                    SetObjValue(generator, "TemplateFileName", template);
                    t = Type.GetType("Soneta.Printer.AspGenerator, Soneta.Printer").GetNestedType("Destinations");
                    generator.GetType().GetField("Destination").SetValue(generator, Enum.ToObject(t, (int)destination));
                    if (!string.IsNullOrEmpty(outputFile))
                        generator.GetType().GetField("OutputFileName").SetValue(generator, outputFile);

                    try
                    {
                        //string cdir = Directory.GetCurrentDirectory();
                        //Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

                        CallObjMethod(generator, "Print",
                            new Type[]{
                            typeof(System.Windows.Forms.Control),
                            Type.GetType("Soneta.Business.Context, Soneta.Business"),
                            typeof(bool),
                            typeof(System.Collections.IEnumerable)
                        },
                            new object[]{
                            form != null ? form : Form.ActiveForm,
                            context,
                            destination == Enova.API.Printer.Destinations.Preview,
                            new ArrayList()
                        });
                        //Directory.SetCurrentDirectory(cdir);
                    }
                    catch (Exception ex)
                    {
                        /*
                        using(var fs = new StreamWriter("enova.error.txt"))
                        {
                            fs.WriteLine("Exception:" + ex.GetType().Name);
                            fs.WriteLine(ex.Message);
                        }
                         */
                        throw ex;
                    }


                }
            }
        }


        public void ZmienDateDokumentu(int ID, DateTime data)
        {
            throw new NotImplementedException();
            /*
            using (Soneta.Business.Session session = EnovaTools.EnovaLogin.CreateSession(false, false))
            {
                var dokument = Soneta.Handel.HandelModule.GetInstance(session).DokHandlowe[ID];
                if (dokument != null)
                {
                    using (var t = session.Logout(true))
                    {
                        var save = dokument.Stan;
                        dokument.Stan = Soneta.Handel.StanDokumentuHandlowego.Bufor;
                        dokument.Obcy.DataOtrzymania = new Soneta.Types.Date(data);
                        dokument.Data = new Soneta.Types.Date(data);
                        //dokument.DataOperacji = new Soneta.Types.Date(data);
                        //dokument.Obcy.DataOtrzymania = new Soneta.Types.Date(data);
                        dokument.Stan = save;

                        t.Commit();
                    }
                    session.Save();
                }
            }
             */
        }



        #endregion

    }
}
