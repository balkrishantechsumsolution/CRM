using BugTrackingSys.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace BugTrackingSys.Areas.Developer.Models
{
    public class UsersRolesViewModel
    {

        public int CurrentPage;
        public int pageSize;
        public double TotalPages;
        public int sortBy;
        public bool isAsc;
        public string Search;
        public int isLastRecord;
        public int Count;
        public ProjectViewModel project { get; set; }
        public RoleModel role { get; set; }
        public UsersModel user { get; set; }
        public Tasks tasks { get; set; }
        public List<User_RolesModel> User_RolesModelList { get; set; }
        public List<UsersModel> UsersList { get; set; }
        public List<RoleModel> RoleList { get; set; }
        public List<ProjectViewModel> ProjectList { get; set; }

        public CalendarEvent cal { get; set; }

       
        public List<IFormFile> files { get; set; }



        public List<Tasks> CalendarEventList { get; set; }

        [NotMapped]
        public IList<SelectListItem> RoleMainList { get; set; }

        [NotMapped]
        public IList<SelectListItem> UserMainList { get; set; }

        [NotMapped]
        public IList<SelectListItem> OwnerMainList { get; set; }

        [NotMapped]
        public IList<SelectListItem> AssigneeMainList { get; set; }

        [NotMapped]
        public List<SelectListItem> TaskList { get; set; }

        [NotMapped]
        public List<SelectListItem> PriorityList { get; set; }

        


        public UsersRolesViewModel()
        {
            RoleList = new List<RoleModel>();
            User_RolesModelList = new List<User_RolesModel>();
            UsersList = new List<UsersModel>();
            ProjectList = new List<ProjectViewModel>();
        }
    }
    public class ProjectViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string CreatedBy { get; set; }

        public string CreatedOn { get; set; }


        public string Isactive { get; set; }
    }

    public class CalendarEvent
    {
        public string id { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string text { get; set; }
        public string? color { get; set; }
    }

}
