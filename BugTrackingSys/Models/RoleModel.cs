using System.ComponentModel.DataAnnotations;

namespace BugTrackingSys.Models
{
    //Ayush
    interface Role
    {
        public string Id { get; set; }
        public string name { get; set; }
    }
    public class RoleModel: Role
    {
        public string Id { get; set; }
        public string name { get; set; }
    }
}
