using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTrackingSys.Models
{
    public class User_RolesModel
    {
        //// Creating objects of Geeks1 and Geeks2 class
        //UsersModel obj1 = new UsersModel();
        //RoleModel obj2 = new RoleModel();

        public string role_id { get; set; }
        public string user_id { get; set; }
        public RoleModel role { get; set; }
        public UsersModel user { get; set; }

        [NotMapped]
        public IList<SelectListItem> RoleList { get; set; }
    }
}
