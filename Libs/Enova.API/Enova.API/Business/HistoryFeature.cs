using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Business
{
    /*
    public interface HistoryFeature : IHistoryValue, ICloneable, IComparable
    {
        // Fields
        private readonly FeatureCollection _coll;
        private readonly FeatureDefinition _fd;
        private HistoryItem[] _items;

        // Methods
        private HistoryFeature(FeatureCollection coll, FeatureDefinition fd, HistoryItem[] items);
        public HistoryFeature Clone();
        public int CompareTo(object obj);
        public Date FindDateFrom(Date date);
        public bool GetBool(Date date);
        public Currency GetCurrency(Date date);
        public Date GetDate(Date date);
        public decimal GetDecimal(Date date);
        public double GetDouble(Date date);
        public Fraction GetFraction(Date date);
        public FromTo GetFromTo(Date date);
        public int GetInt(Date date);
        public Type GetItemType();
        public Row GetRow(Date date);
        public string GetString(Date date);
        public Time GetTime(Date date);
        private int IndexOf(Date date);
        public void Remove(Date date);
        public void Set(object value);
        object ICloneable.Clone();
        public override string ToString();

        // Properties
        public Date[] Dates { get; }
        public FeatureDefinition FeatureDefinition { get; }
        public object this[Date date] { get; set; }

        // Nested Types
        internal class Helper : FeatureDefinition.Helper
        {
            // Fields
            private readonly FeatureDefinition _fd;
            private const string DateMarker = "~~";
            internal readonly FeatureDefinition.Helper ItemHelper;

            // Methods
            internal Helper(FeatureDefinition fd, FeatureDefinition.Helper itemHelper);
            internal override void Check(FeatureCollection coll, FeatureDefinition fd, object value);
            internal override object Clone(object v);
            internal override object GetDefaultValue(FeatureCollection coll);
            internal override Type GetFeatureType(FeatureDefinition fd);
            internal override bool IsNull(FeatureCollection coll, object value);
            internal override object Parse(FeatureData t, FeatureCollection coll, FeatureDefinition fd);
            internal override FeatureData ToString(object v, int maxLen);

            // Properties
            internal Type ItemType { get; }
            internal override string TypeDescription { get; }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct HistoryItem
        {
            public Date ActualFrom;
            public object Value;
        }

        public class LastItemException : Exception
        {
            // Methods
            public LastItemException();
        }
    }
     */
}
