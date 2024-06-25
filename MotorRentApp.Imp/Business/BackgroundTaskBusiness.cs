using MotorRentApp.Core.Business;
using MotorRentApp.Core.Model;
using MotorRentApp.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRentApp.Imp.Business
{
    public class BackgroundTaskBusiness : IBackgroundTaskBusiness
    {
        private readonly IBackgroundTasksRepository _repository;

        public BackgroundTaskBusiness(IBackgroundTasksRepository repository)
        {
            _repository = repository;
        }

        public Task<BackgroundTask> RegisterTask<T>(T task)
        {   
            return _repository.SaveTask(task);
        }
    }
}
