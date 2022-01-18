using FluentValidation;
using Restaurants57Blocks.Common.Constants;
using Restaurants57Blocks.Domain.Request;

namespace Restaurants57Blocks.Domain.FluentValidation
{
    public class EmployeeValidator : AbstractValidator<EmployeeRequest>
    { 
        public EmployeeValidator()
        {
               RuleFor(employee => employee.Identifcation)
               .NotEmpty()
               .NotNull()
               .WithMessage(Message.Not_Empty_Identification);

            RuleFor(employee => employee.FullName)
            .NotEmpty()
            .NotNull()
            .WithMessage(Message.Not_Empty_FullName);

            RuleFor(employee => employee.Email)
               .Matches(RegularExpressions.Email)
               .WithMessage(Message.Format_Invalid_Email)
               .NotEmpty().WithMessage(Message.Not_Empty_Email)
               .NotNull();

            RuleFor(employee => employee.ResidenceAdress)
             .NotEmpty()
             .NotNull()
             .WithMessage(Message.Not_Empty_Address);

            RuleFor(employee => employee.RestaurantId)
             .NotEmpty()
             .NotNull()
             .WithMessage(Message.Not_Empty_Address);
        }
    }
}
