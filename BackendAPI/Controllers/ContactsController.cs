using BackendAPI.Data;
using BackendAPI.Errors;
using BackendAPI.Helpers;
using BackendAPI.Models;
using BackendAPI.Models.ViewModels;
using CsvHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BackendAPI.Controllers
{
    [Authorize]
    //[RoleBaseAuthorize(Enums.ERole.User)]
    [Route("api/contacts")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public ContactsController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
        {
            return await _context.Contacts.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContact(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null)
                return NotFound();

            return contact;
        }

        [HttpPost]
        public async Task<ActionResult<Contact>> PostContact(Contact contact)
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContact", new { id = contact.Id }, contact);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact(int id, Contact contact)
        {
            if (id != contact.Id)
            {
                return BadRequest();
            }

            var userId = User.FindFirstValue(ClaimTypes.Name);
            if (contact.EmployeeId.ToString() != userId)
                return Forbid();

            _context.Entry(contact).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetContact", new { id = contact.Id }, contact);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Contact>> DeleteContact(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.Name);
            if (contact.EmployeeId.ToString() != userId)
                return Forbid();

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return contact;
        }

        private bool ContactExists(int id)
        {
            return _context.Contacts.Any(e => e.Id == id);
        }

        [HttpGet]
        [Route("search/{keyword}")]
        public async Task<ActionResult<IEnumerable<Contact>>> SearchContacts(string keyword)
        {
            string[] searchTerms = keyword.ToString().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var contacts = await _context.Contacts.ToListAsync();
            var results = (from contact in contacts
                           where searchTerms.All(s => contact.FirstName.Contains(s) 
                                                    || contact.LastName.Contains(s)
                                                    || contact.Department.Contains(s)
                                                    || contact.Project.Contains(s)
                                                    || contact.Title.Contains(s))
                           select contact).ToList();

            return results;
        }

        [HttpPost]
        [Route("export")]
        public async Task<IActionResult> ExportCSV()
        {
            var builder = new StringBuilder();
            builder.AppendLine("First Name, Last Name, Title, Department, Project, Employee ID");

            var contacts = await _context.Contacts.ToListAsync();
            foreach (var contact in contacts)
            {
                builder.AppendLine($"{contact.FirstName}, {contact.LastName}, {contact.Title}, {contact.Department}, {contact.Project}, {contact.EmployeeId}");
            }
            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "ListContact.csv");
        }

        [HttpPost]
        [Route("import")]
        public async Task<IActionResult> ImportAsync(IFormFile files)
        {
            string result;
            var file = Request.Form.Files.First();

            if (files == null)
                files = file;

            if (files != null)
            {
                if (!files.FileName.EndsWith(".csv"))
                    result = "File format is not supported!";
                else
                {
                    List<ContactViewModel> contacts = new List<ContactViewModel>();
                    var reader = new StreamReader(files.OpenReadStream());
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        csv.Read();
                        csv.ReadHeader();
                        while (csv.Read())
                        {
                            var contact = csv.GetRecord<ContactViewModel>();
                            contacts.Add(contact);
                        }
                    }
                    foreach (ContactViewModel item in contacts)
                    {
                        Contact contact = new Contact
                        {
                            Id = 0,
                            FirstName = item.FirstName,
                            LastName = item.LastName,
                            Title = item.Title,
                            Department = item.Department,
                            Project = item.Project,
                            Avatar = item.Avatar,
                            EmployeeId = item.EmployeeId
                        };
                        _context.Contacts.Add(contact);
                        await _context.SaveChangesAsync();
                    }

                    result = "Save file success!";
                }
            }
            else
                result = "Empty File!";

            return Ok(new { Value = result });
        }
    }
}
