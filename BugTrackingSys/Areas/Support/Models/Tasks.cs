using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BugTrackingSys.Areas.Support.Models
{
    public class Tasks
    {
        public int CurrentPage;
        public int pageSize;
        public double TotalPages;
        public int sortBy;
        public bool isAsc;
        public string Search;
        public int isLastRecord;
        public int Count;

        [Key]
        [Required]
        public int id { get; set; }

        public int TaskId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "TaskName length can't be more than 100.")]
        public string TaskName { get; set; }

        public string ProjectID { get; set; }

        [StringLength(6000, MinimumLength = 3)]
        public string TaskDescrpition { get; set; }

        [Required]
        public string TaskAssignee { get; set; }

        public string TaskOwner { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required(ErrorMessage = "Please enter start date")]
        [DataType(DataType.Date)]
        public DateTime Startdate { get; set; }

        [Required(ErrorMessage = "Please enter end date")]
        [DataType(DataType.Date)]
        public DateTime Enddate { get; set; }

        [Required]
        public string PrioritySet { get; set; }

        [Required]
        public string TaskStatus { get; set; }
        [NotMapped]
        public bool IsActive { get; set; }

        public string CurrentStatus { get; set; }

        public int Stage { get; set; }

        public string TaskAssigneeType { get; set; }

        public string OwnerType { get; set; }

        public string? color { get; set; }

        [NotMapped]
        public IList<SelectListItem> AssigneeMainList { get; set; }

        [NotMapped]
        public IList<SelectListItem> OwnerMainList { get; set; }


    }
    public class TasksRemark
    {
        public int id { get; set; }

        public int TaskId { get; set; }

        public string Remarks { get; set; }

        public string RemarksBy { get; set; }
        public DateTime RemarksOn { get; set; }
        public string PrioritySet { get; set; }
        public string IsActive { get; set; }
        public string CurrentStatus { get; set; }
        public string Stage { get; set; }
        public DateTime EndDate { get; set; }

    }



}
