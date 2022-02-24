using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendAPI.Models.ViewModels
{
    public class ContactViewModel
    {
        [Name("First Name")]
        public string FirstName { get; set; }
        [Name("Last Name")]
        public string LastName { get; set; }
        [Name("Title")]
        public string Title { get; set; }
        [Name("Departmant")]
        public string Department { get; set; }
        [Name("Project")]
        public string Project { get; set; }
        [Name("Avatar")]
        public string Avatar { get; set; }
        [Name("Employee ID")]
        public int EmployeeId { get; set; }
    }
}
