using BFF_MotorRentApp.Controllers.Validations;
using System.ComponentModel.DataAnnotations;

namespace BFF_MotorRentApp.Controllers.Requests.Vehicle
{
    public class VehicleRegistration
    {
        [LicensePlateValidation]
        public string LicensePlate { get; set; } = String.Empty;

        [YearValidation]
        public int Year { get; set; }

        public int GetMax() { return Year; }
    }
}
