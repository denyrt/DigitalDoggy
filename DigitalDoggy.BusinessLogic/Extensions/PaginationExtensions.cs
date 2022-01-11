using DigitalDoggy.BusinessLogic.Pagination;
using System.Linq;

namespace DigitalDoggy.BusinessLogic.Extensions
{
    public static class PaginationExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, IPaginable paginable)
        {
            if (paginable == null)
            {
                return query;
            }

            if (paginable.PageNumber < 0)
            {
                return query;
            }
            
            if (paginable.PageSize < 1)
            {
                return query;
            }
            
            try
            {
                _ = checked(paginable.PageNumber * paginable.PageSize);
            }
            catch (System.OverflowException)
            {
                return query;
            }

            return query.Skip(paginable.PageNumber * paginable.PageSize).Take(paginable.PageSize);
        }
    }
}
