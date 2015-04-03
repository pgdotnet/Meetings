using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AopWithDp
{
    internal class FormValidator: AbstractValidator<IFormModel>
    {
        public FormValidator()
        {
            RuleFor(o => o.Name)
                .NotEmpty();

            RuleFor(o => o.Phone)
                .NotEmpty();

            RuleFor(o => o.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(o => o.City)
                .NotEmpty();

            RuleFor(o => o.Address1)
                .NotEmpty();
        }
    }
}
