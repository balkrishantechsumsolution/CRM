using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTrackingSys.Areas.Developer.Models
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

        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string ProjectID { get; set; }
        public string TaskDescrpition { get; set; }
        public string TaskAssignee { get; set; }
        public string TaskOwner { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime Startdate { get; set; }
        public DateTime Enddate { get; set; }
        public string PrioritySet { get; set; }
        public string TaskStatus { get; set; }
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
    public interface IFormFile
    {
        string ContentType { get; }
        string ContentDisposition { get; }
        IHeaderDictionary Headers { get; }
        long Length { get; }
        string Name { get; }
        string FileName { get; }
        Stream OpenReadStream();
        void CopyTo(Stream target);
        public Task CopyToAsync(Stream target);
    }
}
