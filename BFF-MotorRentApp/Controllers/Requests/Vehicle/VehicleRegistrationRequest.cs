using BFF_MotorRentApp.Controllers.Validations;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BFF_MotorRentApp.Controllers.Requests.Vehicle
{
    public class VehicleRegistrationRequest
    {
        [LicensePlateValidation]
        public string LicensePlate { get; set; } = String.Empty;

        [YearValidation]
        public int Year { get; set; }

        [NotNull]
        [Required]
        public string Model { get; set; }
    }
}
