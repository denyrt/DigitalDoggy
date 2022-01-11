using MediatR;
using System.ComponentModel;

namespace DigitalDoggy.BusinessLogic.ApiCommands
{
    public record CreateDogCommand : IRequest<CreateDogResponse>
    {
        [DefaultValue("Doggy")]
        public string Name { get; init; }
        
        [DefaultValue("red & white")]
        public string Color { get; init; }
        
        [DefaultValue(100)]
        public int TailLength { get; init; }
        
        [DefaultValue(50)]
        public int Weight { get; init; }
    }
}