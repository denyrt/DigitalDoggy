namespace DigitalDoggy.BusinessLogic.Sorting
{
    public interface ISortable
    {
        public string PropertyName { get; init; }
        public string Order { get; init; }
    }
}