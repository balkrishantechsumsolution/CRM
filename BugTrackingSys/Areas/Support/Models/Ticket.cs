using Microsoft.VisualBasic;

namespace BugTrackingSys.Areas.Support.Models
{
    public class Ticket
    {
        public int TaskId { get; set; }
        public string? TaskName { get; set; }
        public string? ProjectId { get; set; }
        public string? TaskDescription { get; set;}
        public string? TaskAssignee { get; set;}
        public string? Taskowner { get; set; }

        public DateTime CreatedDate { get; set; }= DateTime.Now;
       
        public DateAndTime? Enddate { get; set; }
       
        public String? Priority { get; set; }
       
        public String? Status { get; set; }
       
        public byte isactive { get; set; }

    }
}
