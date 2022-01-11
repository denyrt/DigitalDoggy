using FluentValidation;

namespace DigitalDoggy.BusinessLogic.ApiCommands
{
    public class CreateDogCommandValidator : AbstractValidator<CreateDogCommand>
    {
        public CreateDogCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(255);
            RuleFor(x => x.Color).NotEmpty().MaximumLength(255);
            RuleFor(x => x.TailLength).GreaterThan(0);
            RuleFor(x => x.Weight).GreaterThan(0);
        }
    }
}
