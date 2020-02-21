using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Data.Entity.Utilities;
using BAL.Types;

namespace BAL.Business
{
    public class Key<T> : SubTable<T>
        where T : Row
    {
        #region Methods

        internal ArrayList fields;
        internal object[] values;

        #endregion

        #region Methods

        public Key(Table<T> table)
            : base(table)
        {
            this.fields = new ArrayList();
        }

        public void ApplyValues(T row)
        {
            if (this.values != null && this.values.Length > 0 && this.fields.Count > 0)
            {
                for (int i = 0; i < this.fields.Count; i++)
                {
                    if (values.Length == i)
                        break;
                    if (values[i] != null)
                    {
                        var pinfo = (PropertyPath)this.fields[i];
                        var value = values[i];
                        pinfo.Last.SetValue(row, value, null);
                    }
                }
            }
        }

        public IQueryable<T> AddWhere(IQueryable<T> query)
        {
            if (values != null && values.Length > 0 && fields.Count > 0)
            {
                for (int i = 0; i < this.fields.Count; i++)
                {
                    if (values.Length == i)
                        break;
                    if (values[i] != null)
                    {
                        var pinfo = (PropertyPath)this.fields[i];
                        var value = values[i];
                        query = query.Where(this.createEqualExpression(pinfo, value));
                        
                    }

                }
            }
            return query;
        }

        public virtual IQueryable<T> AddOrderBy(IQueryable<T> query)
        {
            return query;
        }


        private Expression<Func<T,bool>> createEqualExpression(PropertyPath pinfo, object value)
        {
            ParameterExpression param = Expression.Parameter(typeof(T));
            MemberExpression member = Expression.MakeMemberAccess(param, pinfo.Last);
            ConstantExpression constant = Expression.Constant(value);
            if (typeof(IRow).IsAssignableFrom(pinfo.Last.PropertyType))
            {
                member = Expression.MakeMemberAccess(member, member.Type.GetProperty("ID"));
                var id = value.GetType().GetProperty("ID").GetValue(value, null);
                constant = Expression.Constant(id);
            }
            BinaryExpression equal = Expression.Equal(member, constant);
            Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T,bool>>(equal, param);
            return lambda;
        }

        protected virtual Key<T> ParseInitField(LambdaExpression exp)
        {
            if (exp != null && exp.Body.NodeType == ExpressionType.MemberAccess)
            {
                PropertyInfo pinfo = ((MemberExpression)exp.Body).Member as PropertyInfo;
                if (pinfo != null)
                    this.fields.Add(new Types.PropertyPath(pinfo));
            }

            return this;
        }

        public Key<T> InitField<R>(Expression<Func<T, R>> exp)
        {
            return this.ParseInitField(exp);
        }

        public S CreateSubtable<S>(params object[] data)
            where S : Table<T>, new()
        {
            return (S)new S().SetKey(this, data);
        }


        #endregion
    }
}
