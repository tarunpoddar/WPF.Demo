using System.Globalization;
using System.Windows.Controls;

namespace WPF.Common.Mvvm
{
    /// <summary>
    /// Validation rule for alphabetic text in a control.
    /// </summary>
    public class AlphabeticValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object theValue, CultureInfo theCultureInfo)
        {
            if (theValue is string aInput && !string.IsNullOrEmpty(aInput))
            {
                if (aInput.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
                {
                    return ValidationResult.ValidResult;
                }

                return new ValidationResult(false, "Please enter alphabetic characters only.");
            }

            return new ValidationResult(false, "Text is empty.");
        }
    }
}
