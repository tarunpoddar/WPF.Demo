using System.Globalization;
using System.Windows.Controls;

namespace WPF.Common.Mvvm
{
    /// <summary>
    /// Validation rule for date selection in date picker.
    /// </summary>
    public class DatePickerValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is DateTime selectedDate)
            {
                if (selectedDate <= DateTime.Today)
                {
                    return ValidationResult.ValidResult;
                }

                return new ValidationResult(false, "Cannot bill for a future date.");
            }

            return new ValidationResult(false, "Please select a valid date.");
        }
    }
}
