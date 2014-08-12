using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EntitiesCoreFramework.Utilities;

namespace EntitiesCoreFramework.jqGrid
{
    public static class LinqExtensions
    {
        /// <summary>
        /// Returns a sortable expression based on a column and direction.
        /// </summary>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string sortColumn, string direction)
        {
            string methodName = string.Format("OrderBy{0}", direction.ToLower() == "asc" ? "" : "descending");
            ParameterExpression parameter = Expression.Parameter(query.ElementType, "p");
            MemberExpression memberAccess = null;
            
            foreach (var property in sortColumn.Split('.'))
                memberAccess = MemberExpression.Property(memberAccess ?? (parameter as Expression), property);

            LambdaExpression orderByLambda = Expression.Lambda(memberAccess, parameter);
            MethodCallExpression result = Expression.Call(typeof(Queryable),
                                  methodName,
                                  new[] { query.ElementType, memberAccess.Type },
                                  query.Expression,
                                  Expression.Quote(orderByLambda));

            return query.Provider.CreateQuery<T>(result);
        }

        /// <summary>
        /// Filters an expression based on jqGrid arguments.
        /// </summary>
        public static IQueryable<T> Where<T>(this IQueryable<T> query, Filter where)
        {
            if (where == null)
                return query;

            if (where.groupOp == "AND")
            {
                foreach (var rule in where.rules)
                {
                    query = query.Where(rule.field, rule.data, StringEnum.Parse<WhereOperation>(rule.op));
                }
            }
            else
            {
                var temp = (new List<T>()).AsQueryable();
                foreach (var rule in where.rules)
                {
                    var t = query.Where(rule.field, rule.data, StringEnum.Parse<WhereOperation>(rule.op));
                    temp = temp.Concat(t);
                }

                query = temp.Distinct();
            }

            return query;
        }

        /// <summary>
        /// Filters an expression based on jqGrid arguments.
        /// </summary>
        /// <returns></returns>
        public static IQueryable<T> Where<T>(this IQueryable<T> query, string column, object value, WhereOperation operation)
        {
            if (string.IsNullOrEmpty(column))
                return query;

            ParameterExpression parameter = Expression.Parameter(query.ElementType, "p");

            MemberExpression memberAccess = null;
            foreach (var property in column.Split('.'))
                memberAccess = MemberExpression.Property(memberAccess ?? (parameter as Expression), property);

            //change param value type
            //necessary to getting bool from string
            ConstantExpression filter = Expression.Constant(Convert.ChangeType(value, memberAccess.Type));  

            Expression condition = null;
            LambdaExpression lambda = null;

            switch (operation)
            {
                //equal ==
                case WhereOperation.Equal:
                    condition = Expression.Equal(memberAccess, filter);
                    break;
                //not equal !=
                case WhereOperation.NotEqual:
                    condition = Expression.NotEqual(memberAccess, filter);
                    break;
                //string.Contains()
                case WhereOperation.Contains:
                    condition = Expression.Call(memberAccess, typeof(string).GetMethod("Contains"), Expression.Constant(value));
                    break;
            }

            lambda = Expression.Lambda(condition, parameter);
            MethodCallExpression result = Expression.Call(
                  typeof(Queryable), "Where",
                  new[] { query.ElementType },
                  query.Expression,
                  lambda);

            return query.Provider.CreateQuery<T>(result);
        }
    }
}
