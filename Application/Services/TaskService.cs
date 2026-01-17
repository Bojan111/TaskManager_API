using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain;

namespace Application.Services
{
    public class TaskService : ITaskService
    {
        private int _nextId = 1;
        private readonly IUserService _userService;

        public TaskService(IUserService userService)
        {
            _userService = userService;
        }

        public List<TaskEntity> GetTasksForUser(string username)
        {
            var user = _userService.GetByUsername(username);
            return user?.Tasks ?? new List<TaskEntity>();
        }

        public TaskEntity CreateTask(string username, TaskEntity task)
        {
            var user = _userService.GetByUsername(username);
            if (user == null) throw new Exception("User not found");

            task.Id = _nextId++;
            user.Tasks.Add(task);
            return task;
        }

        public TaskEntity? UpdateTask(string username, int id, TaskEntity task)
        {
            var user = _userService.GetByUsername(username);
            var existing = user?.Tasks.FirstOrDefault(t => t.Id == id);
            if (existing == null) return null;

            existing.Title = task.Title;
            existing.Description = task.Description;
            existing.Status = task.Status;
            return existing;
        }

        public bool DeleteTask(string username, int id)
        {
            var user = _userService.GetByUsername(username);
            var existing = user?.Tasks.FirstOrDefault(t => t.Id == id);
            if (existing == null) return false;

            user.Tasks.Remove(existing);
            return true;
        }
    }
}
