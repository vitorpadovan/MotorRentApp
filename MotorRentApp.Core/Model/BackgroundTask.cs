using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRentApp.Core.Model
{
    public class BackgroundTask
    {
        public Guid Id { get; set; }
        public string JsonContent {  get; set; } = String.Empty;
    }
}
