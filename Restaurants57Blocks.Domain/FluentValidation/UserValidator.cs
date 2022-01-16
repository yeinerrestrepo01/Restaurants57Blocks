using FluentValidation;
using Restaurants57Blocks.Common.Constants;
using Restaurants57Blocks.Domain.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurants57Blocks.Domain.FluentValidation
{
    public class UserValidator : AbstractValidator<UserRequest>
    {
        public UserValidator()
        {
            RuleFor(user => user.FullName)
            .NotEmpty()
            .WithMessage(Message.Not_Empty_FullName);

            RuleFor(user => user.Password)
             .Matches(RegularExpressions.Password)
             .WithMessage(Message.Format_Invalid_Password);

            RuleFor(user => user.Email)
                .Matches(RegularExpressions.Email)
                .WithMessage(Message.Format_Invalid_Email)
                .NotEmpty().WithMessage(Message.Not_Empty_Email)
                .NotNull();
        }
    }
}
