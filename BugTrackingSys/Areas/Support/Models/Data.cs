using BugTrackingSys.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTrackingSys.Areas.Support.Models
{   

    public class CalendarEvent
    {
        public string id { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string text { get; set; }
        public string? color { get; set; }
    }

}
