using Application.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskManager_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        private string Username => User.Identity?.Name ?? "";

        [HttpGet]
        public IActionResult GetAll() => Ok(_taskService.GetTasksForUser(Username));

        [HttpPost]
        public IActionResult Create([FromBody] TaskEntity task)
        {
            if (string.IsNullOrWhiteSpace(task.Title)) return BadRequest();
            return Ok(_taskService.CreateTask(Username, task));
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] TaskEntity task)
        {
            var updated = _taskService.UpdateTask(Username, id, task);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _taskService.DeleteTask(Username, id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
