using BackendAPI.Data;
using BackendAPI.Errors;
using BackendAPI.Helpers;
using BackendAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BackendAPI.Controllers
{
    [Authorize]
    //[RoleBaseAuthorize(Enums.ERole.User)]
    [Route("api/tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public TasksController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskModel>>> GetTasks()
        {
            return await _context.TaskModels.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskModel>> GetTask(int id)
        {
            var task = await _context.TaskModels.FindAsync(id);

            if (task == null)
                return NotFound();

            return task;
        }

        [HttpPost]
        public async Task<ActionResult<TaskModel>> PostTask(TaskModel task)
        {
            _context.TaskModels.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTask", new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(int id, TaskModel task)
        {
            if (id != task.Id)
                return BadRequest();

            var userId = User.FindFirstValue(ClaimTypes.Name);
            if (task.UserId != userId)
                return Forbid();

            _context.Entry(task).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTask", new { id = task.Id }, task);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TaskModel>> DeleteTask(int id)
        {
            var task = await _context.TaskModels.FindAsync(id);
            if (task == null)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.Name);
            if (task.UserId != userId)
                return Forbid();

            _context.TaskModels.Remove(task);
            await _context.SaveChangesAsync();

            return task;
        }

        private bool TaskExists(int id)
        {
            return _context.TaskModels.Any(e => e.Id == id);
        }

        [HttpGet]
        [Route("search/{keyword}")]
        public async Task<ActionResult<IEnumerable<TaskModel>>> SearchTask(string keyword)
        {
            string[] searchTerms = keyword.ToString().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var tasks = await _context.TaskModels.ToListAsync();
            var results = (from task in tasks
                           where searchTerms.All(s => task.Task.Contains(s))
                           select task).ToList();

            return results;
        }

    }
}
