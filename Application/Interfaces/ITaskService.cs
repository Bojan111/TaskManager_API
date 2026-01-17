using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Application.Interfaces
{
    public interface ITaskService
    {
        List<TaskEntity> GetTasksForUser(string username);
        TaskEntity CreateTask(string username, TaskEntity task);
        TaskEntity? UpdateTask(string username, int id, TaskEntity task);
        bool DeleteTask(string username, int id);
    }
}
