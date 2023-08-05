namespace SportsBettingSystem.Web.CustomAttributes
{
    using System.ComponentModel.DataAnnotations;
    public class CustomDateTimeAtribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                if (DateTime.TryParse(value!.ToString(), out DateTime Value))
                {
                    if (value is DateTime date && date.Date < DateTime.Now.Date)
                    {
                        return new ValidationResult($"Cannot add games with date prior to todays date - {DateTime.Now.Date}.");
                    }
                    else
                        return ValidationResult.Success;
                }
            }
            return new ValidationResult($"There is a problem with the date, make sure you are choosing a valid date and time! You may contact technical support for more information.");
        }
    }
}
