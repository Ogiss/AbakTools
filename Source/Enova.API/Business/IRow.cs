using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Business
{
    //[Editor("Soneta.Business.Forms.Design.RowEditor, Soneta.Business.Forms", typeof(UITypeEditor)), ControlEdit(ControlEditKind.Lookup, Filter = ControlEditAttribute.FilterMode.SingleLine), TypeConverter(typeof(RowConverter))]
    public interface IRow : ISessionable, Types.IObjectBase
    {
        // Methods
        //VerifiersException GetVerifiers(bool withWarnings);
        bool IsReadOnly();

        // Properties
        FeatureCollection Features { get; }
        int ID { get; }
        bool IsLive { get; }
        IRow Parent { get; }
        string Prefix { get; }
        Row Root { get; }
        //RowState State { get; }
        Table Table { get; }
        int TableHandle { get; }
    }




}
