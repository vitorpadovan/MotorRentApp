using MessageService.Definition;
using MotorRentApp.Core.Business;
using MotorRentApp.Core.Model;

namespace MotorRentApp.Imp.Business
{
    public class VehicleBusiness : IVehicleBusiness
    {
        private readonly IMessageService _messageService;
        private readonly IBackgroundTaskBusiness _backgroundTaskBusiness;

        public VehicleBusiness(IMessageService messageService, IBackgroundTaskBusiness backgroundTaskBusiness)
        {
            _messageService = messageService;
            _backgroundTaskBusiness = backgroundTaskBusiness;
            //_backgroundTaskBusiness = backgroundTaskBusiness;
        }

        public bool Registre(Vehicle vehicle)
        {
            return _messageService.Publish<Vehicle>(vehicle, (x) =>
            {
                var r = _backgroundTaskBusiness.RegisterTask<Vehicle>(vehicle).Result;
                return r.Id;
            });
        }
    }
}
