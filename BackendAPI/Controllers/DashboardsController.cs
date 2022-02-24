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
    [Route("api/dashboards")]
    [ApiController]
    public class DashboardsController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public DashboardsController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Dashboard>> GetDashboard(int id)
        {
            var dashboard = await _context.Dashboards.Include(a => a.Widgets).Where(a => a.Id == id).FirstOrDefaultAsync();

            if (dashboard == null)
                return NotFound();

            return dashboard;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDashboard(int id, Dashboard dashboard)
        {
            if (id != dashboard.Id)
                return BadRequest();

            var userId = User.FindFirstValue(ClaimTypes.Name);
            if (dashboard.UserId != userId)
                return Forbid();

            _context.Entry(dashboard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                for (int i = 0; i < dashboard.Widgets.Count; i++)
                    _context.Entry(dashboard.Widgets.ElementAt(i)).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DashboardExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDashboard", new { id = dashboard.Id }, dashboard);
        }

        private bool DashboardExists(int id)
        {
            return _context.Dashboards.Any(e => e.Id == id);
        }
    }
}
