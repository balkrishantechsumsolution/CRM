using Microsoft.AspNetCore.Mvc;
using BugTrackingSys.Areas.Support.Models;
using Microsoft.VisualBasic;

namespace BugTrackingSys.Areas.Support.Controllers
{
    [Area("Support")]
    public class TicketController : Controller
    {
        public IActionResult TicketForm(int id)
        {
            // Assume you have some code here that retrieves the ticket object from a database or other source based on the ID.
            var ticket = new Ticket
            {
                TaskId = id,
                TaskName = "Example Ticket",
                TaskDescription = "This is an example ticket.",
                ProjectId = "Project123",
                TaskAssignee = "John Doe",
                Taskowner = "Jane Smith",
                Priority = "High",
                Status = "Open",
                isactive = 1
            };

            return View(ticket);
        }
    }
}
