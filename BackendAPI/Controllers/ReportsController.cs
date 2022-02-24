using BackendAPI.Data;
using BackendAPI.Errors;
using BackendAPI.Extensions;
using BackendAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BackendAPI.Controllers
{
    [Authorize]
    //[RoleBaseAuthorize(Enums.ERole.User)]
    [ApiController]
    [Route("api/reports")]
    public class ReportsController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public ReportsController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("_countBy/{collection}/{field}")]
        public async Task<ActionResult> CountBy(string collection, string field)
        {
            var type = Assembly.GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.Name == collection);

            var entities = await _context.Set(type).ToListAsync();

            var pl = from r in entities.ToList()
                     orderby r.GetType().GetProperty(field).GetValue(r, null) 
                     group r by r.GetType().GetProperty(field).GetValue(r, null) into grp
                     select new { Member = grp.Key, count = grp.Count() };

            if (entities == null)
                return BadRequest();

            return Ok(pl);
        }
    }
}
