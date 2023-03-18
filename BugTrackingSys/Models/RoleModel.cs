namespace BugTrackingSys.Models
{
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
