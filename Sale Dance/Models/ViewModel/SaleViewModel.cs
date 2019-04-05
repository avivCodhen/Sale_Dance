using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sale_Dance.Models.ViewModel
{
    public class SaleViewModel :IValidatableObject
    {
        [Required(ErrorMessage = "יש להזין שם")]
        [StringLength(50, ErrorMessage = "יש להזין תוכן בין 4 עד 50 תווים", MinimumLength = 4)]
        public string Name { get; set; }
        public byte[] Image { get; set; }

        [Required(ErrorMessage = "יש להזין מחיר לפני")]
        public double BeforePrice { get; set; }

        [Required(ErrorMessage = "יש להזין מחיר אחרי ההנחה")]
        public double SalePrice { get; set; }

        public int SaleId { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (SalePrice <= 0)
            {
                errors.Add(new ValidationResult("אנא הכנס את מחיר המבצע", new List<string>(){nameof(SalePrice)}));
            }
            if (BeforePrice <= 0)
            {
                errors.Add(new ValidationResult("אנא הכנס את מחיר לפני המבצע", new List<string>() { nameof(BeforePrice) }));
            }

            return errors;
        }

        public string Description { get; set; }
    }
}
