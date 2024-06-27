using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MotorRentApp.Core.Model
{
    [Index(nameof(LicensePlate), IsUnique = true)]
    public class Vehicle
    {
        public int Id { get; set; }        
        public string LicensePlate { get; set; } = String.Empty;
        public int Year { get; set; }   
        public string Model {  get; set; } = String.Empty;
    }
}
