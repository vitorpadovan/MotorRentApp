using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BFF_MotorRentApp.Controllers.Validations
{
    public class LicensePlateValidation : ValidationAttribute
    {
        private readonly List<String> validPlates = [
            "[A-Za-z]{3}-\\d{4}",
            "[A-Za-z]{3}-\\d{1}[A-Za-z]\\d{2}"
            ];
        public override bool IsValid(object? value)
        {
            string toString;
            try
            {
                toString = (string)value!;
            }
            catch
            {
                return false;
            }
            foreach(var validPlate in validPlates) {
                Regex oldPlate = new Regex(validPlate, RegexOptions.IgnoreCase);
                if (oldPlate.IsMatch(toString))
                    return true;
            };
            return false;
        }
    }
}
