using DigitalDoggy.BusinessLogic.Pagination;
using DigitalDoggy.BusinessLogic.Sorting;
using MediatR;
using System.ComponentModel;

namespace DigitalDoggy.BusinessLogic.ApiQueries
{
    public record GetDogsQuery : IRequest<GetDogsResponse>, ISortable, IPaginable
    {
        [DefaultValue("taiLength")]
        public string PropertyName { get; init; }
        
        [DefaultValue("desc")]
        public string Order { get; init; }

        [DefaultValue(0)]
        public int PageNumber { get; init; }
        
        [DefaultValue(10)]
        public int PageSize { get; init; }
    }
}