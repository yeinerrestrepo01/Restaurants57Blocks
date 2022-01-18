using FluentValidation;
using Restaurants57Blocks.Common.Constants;
using Restaurants57Blocks.Domain.Request;

namespace Restaurants57Blocks.Domain.FluentValidation
{
    public class RestaurantValidator : AbstractValidator<RestaurantRequest>
    { 
        public RestaurantValidator()
        {
            RuleFor(restaurant => restaurant.Identifcation)
            .NotEmpty()
            .NotNull()
            .WithMessage(Message.Not_Empty_Identification);

            RuleFor(restaurant => restaurant.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage(Message.Not_Empty_Name);

            RuleFor(restaurant => restaurant.Address)
             .NotEmpty()
             .NotNull()
             .WithMessage(Message.Not_Empty_Address);

        }
    }
}
