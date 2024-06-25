using System.ComponentModel.DataAnnotations;

namespace BFF_MotorRentApp.Controllers.Validations
{
    public class YearValidation : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            try
            {
                int teste = (int)value!;
                if (teste > DateTime.Now.Year)
                    return false;
            }
            catch
            {
                //TODO to implement better
                return false;
            }
            return true;
        }
    }
}
