using DigitalDoggy.BusinessLogic.Sorting;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace DigitalDoggy.BusinessLogic.Extensions
{
    public static class SortingExtensions
    {
        /// <summary>
        /// Sort element by own property.
        /// </summary>
        /// <typeparam name="T"> The type of the elements of source. </typeparam>
        /// <typeparam name="TKey"> The type of the key returned by the function that is represented by keySelector. </typeparam>
        /// <param name="query"> Source elements. </param>
        /// <param name="sort"> Sorting parameters. </param>
        /// <param name="defaultOrdering"> Default ordering for element when sorting cannot be applied. </param>
        /// <returns></returns>
        public static IQueryable<T> SortBy<T, TKey>(this IQueryable<T> query, ISortable sort, Expression<Func<T, TKey>> defaultOrdering)
        {
            var type = typeof(T);
            var properties = type.GetProperties();

            if (sort == null || !properties.Any(x => x.Name.Equals(sort.PropertyName, StringComparison.OrdinalIgnoreCase)))
            {
                return query.OrderBy(defaultOrdering);
            }

            if (sort.Order == "desc")
            {
                return query.OrderByDescending(sort.PropertyName);
            }

            return query.OrderBy(sort.PropertyName);
        }

        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName)
        {
            return source.OrderBy(ToLambda<T>(propertyName));
        }

        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string propertyName)
        {
            return source.OrderByDescending(ToLambda<T>(propertyName));
        }

        private static Expression<Func<T, object>> ToLambda<T>(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T));
            var property = Expression.Property(parameter, propertyName);
            var propAsObject = Expression.Convert(property, typeof(object));

            return Expression.Lambda<Func<T, object>>(propAsObject, parameter);            
        }
    }
}