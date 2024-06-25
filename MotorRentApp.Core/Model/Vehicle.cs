using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRentApp.Core.Model
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; } = String.Empty;
        public int Year { get; set; }        
    }
}
