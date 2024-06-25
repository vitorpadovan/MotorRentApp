using MotorRentApp.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRentApp.Core.Repository
{
    public interface IBackgroundTasksRepository
    {
        public Task<BackgroundTask> SaveTask<T>(T task);
    }
}
