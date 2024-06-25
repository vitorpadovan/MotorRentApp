using MotorRentApp.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRentApp.Core.Business
{
    public interface IVehicleBusiness
    {
        bool Registre(Vehicle registration);
    }
}
