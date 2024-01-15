using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class UserType : BaseDTO
    {
        public string Name { get; set; }
        //public ICollection<User>? Users { get; set; }
    }

    public class UserTypeValidation : AbstractValidator<UserType>
    {
        public UserTypeValidation()
        {
            When(e => e.ID > 0 && e != null && e.IsDeleted == false, () =>
            {
                RuleFor(e => e.Name).NotNull().WithMessage(" Name Is Required");
                RuleFor(e => e.Name).NotEmpty().WithMessage("Name Is Required");

            });
        }
    }

}
