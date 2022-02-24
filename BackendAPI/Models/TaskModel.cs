using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendAPI.Models
{
    public class TaskModel
    {
        [Key]
        public int Id { get; set; }
        public string Task { get; set; }
        public Boolean IsComplete { get; set; }
        public string UserId { get; set; }
    }
}
