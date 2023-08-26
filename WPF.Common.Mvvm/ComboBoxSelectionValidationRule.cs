using System.Globalization;
using System.Windows.Controls;

namespace WPF.Common.Mvvm
{
    /// <summary>
    /// Validation rule for combobox selection.
    /// </summary>
    public class ComboBoxSelectionValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is string selectedGender)
            {
                // Check if the selected gender is not the default "Select" value
                if (selectedGender != "Select")
                {
                    // Add your additional validation logic here if needed
                    // For example, you can check if the selected gender is valid
                    return ValidationResult.ValidResult;
                }
             
                return new ValidationResult(false, "Please select a gender.");
            }

            return new ValidationResult(false, "Please select a gender.");
        }
    }
}
