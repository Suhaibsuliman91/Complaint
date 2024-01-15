using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Demand : BaseDTO
    {
        public string DescriptionEn { get; set; }
        public string DescriptionAR { get; set; }
        public int ComplaintID { get; set; }
        //public Complaint? Complaint { get; set; }
    }

    public class DemandValidation : AbstractValidator<Demand>
    {
        public DemandValidation()
        {
            When(e => e.ID > 0 && e != null && e.IsDeleted == false, () =>
            {
                RuleFor(e => e.DescriptionEn).NotNull().WithMessage("DescriptionEn Is Required");
                RuleFor(e => e.DescriptionEn).NotEmpty().WithMessage("DescriptionEn Is Required");

                RuleFor(e => e.DescriptionAR).NotNull().WithMessage("DescriptionAR Is Required");
                RuleFor(e => e.DescriptionAR).NotEmpty().WithMessage("DescriptionAR Is Required");
            });
        }
    }

}
