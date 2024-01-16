using FluentValidation;
using Repository;
using Service.InterFace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DTO
{
    public class User : BaseDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public int? UserTypeID { get; set; }
        public UserType? UserType { get; set; }
        //public ICollection<Complaint>? Complaints { get; set; }
    }

    public class UserValidation : AbstractValidator<User>
    {
        private readonly IUnitOfWork _UnitOfWork;
        public UserValidation(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
            When(e => e.ID >= 0 && e != null && e.IsDeleted == false, () =>
            {
                RuleFor(e => e.Name).NotNull().WithMessage(" Name Is Required");
                RuleFor(e => e.Name).NotEmpty().WithMessage("Name Is Required");

                RuleFor(e => e.Email ).NotNull().WithMessage(" Email Is Required");
                RuleFor(e => e.Email ).NotEmpty().WithMessage("Email Is Required");

                RuleFor(e => e.Email).Must((Model, value) => IsUnique(Model.ID, Model.Email)).WithMessage("Email Already Exist");
                RuleFor(e => e.Email).Matches(new Regex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")).WithMessage("Invalid Email");


                RuleFor(e => e.Password ).NotNull().WithMessage(" Password Is Required");
                RuleFor(e => e.Password ).NotEmpty().WithMessage("Password Is Required");

                RuleFor(e => e.Mobile).NotNull().WithMessage(" Mobile Is Required");
                RuleFor(e => e.Mobile).NotEmpty().WithMessage("Mobile Is Required");

                //RuleFor(e => e.TypeID).NotNull().WithMessage(" TypeID Is Required");
                //RuleFor(e => e.TypeID).NotEmpty().WithMessage("TypeID Is Required");
                //When(e => e.TypeID != null, () =>
                //{
                //    RuleFor(e => e.TypeID).Must((TypeID) => IsTypeExist(TypeID)).WithMessage("Invalid User Type");
                //});

            });

        }

        private bool IsTypeExist(int TypeID)
        {
            var Result = _UnitOfWork.UserType.GetByID(TypeID) != null;
            return Result;
        }

        private bool IsUnique(int ID, string Email)
        {
            bool Result = false;
            if(ID > 0)
            {
                Result = _UnitOfWork.User.Find(e => e.ID != ID && e.Email == Email).FirstOrDefault() == null;
            }
            else
            {
                Result = _UnitOfWork.User.Find(e =>e.Email == Email).FirstOrDefault() == null;
            }
            return Result;

        }

    }

}
