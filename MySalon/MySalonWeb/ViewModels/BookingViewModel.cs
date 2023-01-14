using MySalonWeb.Models;
using System.ComponentModel.DataAnnotations;

namespace MySalonWeb.ViewModels
{
    public class BookingViewModel : IValidatableObject
    {
        public Client Client { get; set; }
        public Order Order { get; set; }
        public ServiceTypes ServiceType { get; set; }
        public string Date { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new();

            if (Date != null)
            {
                var dateNow = DateTime.Now;
                var dateOrder = Convert.ToDateTime(Date);

                if (dateOrder < dateNow)
                {
                    errors.Add(new ValidationResult("Date must be greater than current", new List<string> { "Date" }));
                }                
            }

            return errors;
        }
    }
}
