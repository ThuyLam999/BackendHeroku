using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackendAPI.Models
{
    public class Dashboard
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string LayoutType { get; set; }
   
        public virtual ICollection<Widget> Widgets { get; set; }
    }
}
