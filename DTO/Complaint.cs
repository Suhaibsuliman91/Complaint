using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DTO
{
    public class Complaint : BaseDTO
    {
        public string DescriptionEn {  get; set; }
        public string DescriptionAR { get; set; }
        public List<DTO.FileInfo>? AttachmentInfo { get; set; }
        public string? Attachment { get; set; }
        public int UserID { get; set; }
        public int StatusID { get; set; }
        public User? User { get; set; }
        public Status? status { get; set; }

        public ICollection<Demand>? Demands { get; set; }
    }

    public class ComplaintValidation : AbstractValidator<Complaint>
    {
        public ComplaintValidation() 
        {
            When(e => e.ID == 0 && e != null && e.IsDeleted == false, () =>
            {
                RuleFor(e => e.DescriptionEn).NotNull().WithMessage("DescriptionEn Is Required");
                RuleFor(e => e.DescriptionEn).NotEmpty().WithMessage("DescriptionEn Is Required");
                When(e => e.DescriptionEn != null, () =>
                {
                    RuleFor(e => e.DescriptionEn).Matches(new Regex(@"^[a-zA-Z.\s]*$")).WithMessage("Must be in English");

                });

                RuleFor(e => e.DescriptionAR).NotNull().WithMessage("DescriptionAR Is Required");
                RuleFor(e => e.DescriptionAR).NotEmpty().WithMessage("DescriptionAR Is Required");
                When(e => e.DescriptionAR != null, () =>
                {
                    RuleFor(e => e.DescriptionAR).Matches(new Regex(@"^[ء-ي.\s]*$")).WithMessage("Must be in Arabic");
                });


                //RuleFor(e => e.Attachment).NotNull().WithMessage("User Attachment Is Required");
                //RuleFor(e => e.Attachment).NotEmpty().WithMessage("User Attachment Is Required");

            });
            When(e => e.ID > 0 && e != null && e.IsDeleted == false, () =>
            {
                RuleFor(e => e.DescriptionEn).NotNull().WithMessage("DescriptionEn Is Required");
                RuleFor(e => e.DescriptionEn).NotEmpty().WithMessage("DescriptionEn Is Required");
                When(e => e.DescriptionEn != null, () =>
                {
                    RuleFor(e => e.DescriptionEn).Matches(new Regex(@"^[a-zA-Z.\s]*$")).WithMessage("Must be in English");

                });

                RuleFor(e => e.DescriptionAR).NotNull().WithMessage("DescriptionAR Is Required");
                RuleFor(e => e.DescriptionAR).NotEmpty().WithMessage("DescriptionAR Is Required");
                When(e => e.DescriptionAR != null, () =>
                {
                    RuleFor(e => e.DescriptionAR).Matches(new Regex(@"^[ء-ي.\s]*$")).WithMessage("Must be in Arabic");
                });


            });
            When(e => e.ID > 0 && e != null && e.IsDeleted == true, () =>
            {
                RuleFor(e => e.DescriptionEn).Null();
                RuleFor(e => e.DescriptionEn).Empty();

                RuleFor(e => e.DescriptionAR).Null();
                RuleFor(e => e.DescriptionAR).Empty();


            });
        }
    }

}
