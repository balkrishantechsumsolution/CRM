using LeedManagement.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using FluentValidation.TestHelper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace LeedManagement.Areas.Sales.Models
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

        public TasksRemark tasksRemark { get; set; }


        [NotMapped]
        public FileViewModel fileViewModel { get; set; }
        [NotMapped]
        public List<FileViewModel> fileAttach { get; set; }
        [NotMapped]
        public List<User_RolesModel> User_RolesModelList { get; set; }
        [NotMapped]
        public List<UsersModel> UsersList { get; set; }
        [NotMapped]
        public List<RoleModel> RoleList { get; set; }

        [NotMapped]
        public List<Tasks> taskList { get; set; }

        [NotMapped]
        public List<TasksRemark> taskRemarkList { get; set; }

        [NotMapped]
        public List<SelectListItem> ProjectList { get; set; }

        [NotMapped]
        public CalendarEvent cal { get; set; }

        [NotMapped]
        public List<IFormFile> files { get; set; }

        [NotMapped]
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



        }

        //    public UsersRolesViewModel()
        //{
        //    RoleList = new List<RoleModel>();
        //    User_RolesModelList = new List<User_RolesModel>();
        //    UsersList = new List<UsersModel>();
        //    ProjectList = new List<ProjectViewModel>();

        //}
    }
    public class ProjectViewModel
    {
        public string id { get; set; }
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

    public class FileViewModel
    {
        public int FileID { get; set; }
        public string TaskID { get; set; }
        public string FileName { get; set; }
        public string FileData { get; set; }
        public string FileType { get; set; }
        public string IsActive { get; set; }
    }

    public class TasksValidator : AbstractValidator<UsersRolesViewModel>
    {


        public TasksValidator()
        {
            RuleFor(usersRolesViewModel => usersRolesViewModel.tasks.TaskName)
           .NotEmpty().NotNull()
            .WithMessage("You should select a Task Name");
            RuleFor(usersRolesViewModel => usersRolesViewModel.tasks.TaskName).Must(Task_Length).WithMessage("Task name Length must be equal   or greater than 3");

            RuleFor(usersRolesViewModel => usersRolesViewModel.tasks.Startdate)
           .NotEmpty()
            .WithMessage("You should select a start date");
            RuleFor(usersRolesViewModel => usersRolesViewModel.tasks.Enddate)
          .NotEmpty()
           .WithMessage("You should select a end date");
            RuleFor(usersRolesViewModel => usersRolesViewModel.tasks.Startdate).LessThanOrEqualTo(usersRolesViewModel => usersRolesViewModel.tasks.Enddate).WithMessage("Start Date must be greater than End Date");

            RuleFor(usersRolesViewModel => usersRolesViewModel.tasks.TaskStatus)
            .NotEmpty()
            .WithMessage("You should select a Task Status");
            RuleForEach(usersRolesViewModel => usersRolesViewModel.files).SetValidator(new FileValidator());


        }
        private bool Task_Length(string task_)
        {

            if (task_ != null)
            {
                if (task_.Length < 3)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }


    }

    public class FileValidator : AbstractValidator<IFormFile>
    {
        public int FileSize { get; set; } = 1 * 1024 * 1024 * 1024;
        public FileValidator()
        {
            RuleFor(x => x.Length)
            .LessThanOrEqualTo(FileSize)
            .WithMessage("Maximum allowed file size is 5 MB");

        }

    }
}
