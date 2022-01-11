using System.ComponentModel;

namespace DigitalDoggy.BusinessLogic.Models
{
    public record Dog
    {
        [DefaultValue("Doggy")]
        public string Name { get; init; }
        
        [DefaultValue("white")]
        public string Color { get; init; }
        
        [DefaultValue("100")]
        public int TailLength { get; init; }
        
        [DefaultValue("50")]
        public int Weight { get; init; }
    }
}