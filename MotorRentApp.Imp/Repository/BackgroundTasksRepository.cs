using Microsoft.EntityFrameworkCore;
using MotorRentApp.Core.Database;
using MotorRentApp.Core.Model;
using MotorRentApp.Core.Repository;
using System.Text.Json;

namespace MotorRentApp.Imp.Repository
{
    public class BackgroundTasksRepository : IBackgroundTasksRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<BackgroundTask> _backgroundTasks;

        public BackgroundTasksRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _backgroundTasks = appDbContext.BackgroundTasks;
        }

        public async Task<BackgroundTask> SaveTask<T>(T task)
        {
            var backGroundTask = new BackgroundTask(){
                Id = Guid.NewGuid(),
                JsonContent = JsonSerializer.Serialize(task)
            };
            _backgroundTasks.Add(backGroundTask);
            await _appDbContext.SaveChangesAsync();
            return backGroundTask;
        }
    }
}
