using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTrackingSys.Models
{
    interface User
    {
        public string Id { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }
    public class UsersModel: User
    {
        public string Id { get; set; }
        public string Password { get; set; }

        public string Name { get; set; }
    }
}
