using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace BackendAPI.Models
{
    public class Widget
    {
        [Key]
        public int Id { get; set; }
        public string WidgetType { get; set; }
        public int MinWidth { get; set; }
        public int MinHeight { get; set; }

        [JsonIgnore]
        public Dashboard Dashboard { get; set; }

        [ForeignKey("DashboardId")]
        [Required]
        public int DashboardId { get; set; }
    }
}
