using System.Globalization;
using System.Windows.Controls;

namespace WPF.Common.Mvvm
{
    /// <summary>
    /// Validation rule for integeral count in a control.
    /// </summary>
    public class CountValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object theValue, CultureInfo theCultureInfo)
        {
            if (theValue is string theCount)
            {
                // Check if the selected gender is not the default "Select" value
                if (int.TryParse(theCount, out int aCount) && aCount > 0)
                {
                    // Add your additional validation logic here if needed
                    // For example, you can check if the selected gender is valid
                    return ValidationResult.ValidResult;
                }
             
                return new ValidationResult(false, "Please enter valid quantity.");
            }

            return new ValidationResult(false, "Please enter valid quantity.");
        }
    }
}
