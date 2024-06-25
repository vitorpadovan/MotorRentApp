using MotorRentApp.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRentApp.Core.Business
{
    public interface IBackgroundTaskBusiness
    {
        public Task<BackgroundTask> RegisterTask<T>(T task);
    }
}
