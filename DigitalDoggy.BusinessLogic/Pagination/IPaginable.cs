namespace DigitalDoggy.BusinessLogic.Pagination
{
    public interface IPaginable
    {
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
    }
}