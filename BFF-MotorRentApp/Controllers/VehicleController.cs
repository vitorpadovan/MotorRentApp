using BFF_MotorRentApp.Controllers.Requests.Vehicle;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorRentApp.Core.Business;
using MotorRentApp.Core.Enums;

namespace BFF_MotorRentApp.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "vehicle")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleBusiness _vehicleBusiness;

        public VehicleController(IVehicleBusiness vehicleBusiness)
        {
            _vehicleBusiness = vehicleBusiness;
        }

        [HttpPost]
        //[Authorize(Roles = nameof(UserProfiles.COMMONUSER))]
        [Authorize(Roles = nameof(UserProfiles.ADMINISTRATOR))]
        [Route("registration")]
        public IActionResult Registration([FromBody] VehicleRegistration registration)
        {
            _vehicleBusiness.Registre(new(){
                LicensePlate = registration.LicensePlate,
                Year = registration.Year,
            });
            return Ok();
        }
    }
}
