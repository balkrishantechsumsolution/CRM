using Microsoft.AspNetCore.Mvc;
using SqlHelper;
using System.Data.SqlClient;
using System.Data;
using BugTrackingSys.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Principal;
using BugTrackingSys.Areas.Developer.Models;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;


namespace BugTrackingSys.Areas.Developer.Controllers
{
    [Area("Developer")]
    public class DeveloperController : Controller
    {
        private readonly IConfiguration configuration;

        public string _configuration = "";
        data sqlhelper = new data();

        public DeveloperController(IConfiguration configuration)
        {

            _configuration = configuration.GetConnectionString("TrackBugsContext");
            sqlhelper = new data(configuration);
        }


        [Route("Developer/Index")]
        public IActionResult Index()
        {
            UsersRolesViewModel ur = new UsersRolesViewModel();
            ur = GetSessionUser();


            return View(ur);
        }

        [Route("Developer/MainHTML")]
        public IActionResult MainHTML(string CreatedOn)
        {
            UsersRolesViewModel ur = new UsersRolesViewModel();
            ViewBag.CreatedOn = CreatedOn;
            ur = GetSessionUser();
            return View(ur);
        }

        [HttpPost]
        [Route("Developer/Index")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Index(UsersRolesViewModel customer)
        {
            return View(customer);
        }
        public UsersRolesViewModel GetSessionUser()
        {

            var UserType = HttpContext.Session.GetString("UserType");
            var LoginID = HttpContext.Session.GetString("LoginID");
            var RoleName = HttpContext.Session.GetString("RoleName");

            UsersRolesViewModel loginModel = new UsersRolesViewModel();

            User_RolesModel loginUserRoleModel = new User_RolesModel();
            UsersModel usersModel = new UsersModel();
            RoleModel roleModel = new RoleModel();

            usersModel.Name = LoginID;
            roleModel.name = RoleName;

            loginUserRoleModel.user = usersModel;
            loginUserRoleModel.role = roleModel;



            loginModel.user = usersModel;
            loginModel.role = roleModel;

            var selectList = new List<SelectListItem>();

            selectList.Add(
                    new SelectListItem
                    {
                        Value = "",
                        Text = "Select",

                    });

            DataTable dtAll = sqlhelper.ExecuteDataTable("SP_RoleType");

            if (dtAll.Rows.Count > 0)
            {
                for (int i = 0; i < dtAll.Rows.Count; i++)
                {
                    selectList.Add(
                    new SelectListItem
                    {
                        Value = dtAll.Rows[i]["Id"].ToString(),
                        Text = dtAll.Rows[i]["Name"].ToString(),

                    });
                }
            }

            loginModel.RoleMainList = selectList;

            var selectUserList = new List<SelectListItem>();

            selectUserList.Add(
                    new SelectListItem
                    {
                        Value = "",
                        Text = "Select",

                    });

            DataTable dtUserAll = sqlhelper.ExecuteDataTable("SP_GetUserlst");

            if (dtUserAll.Rows.Count > 0)
            {
                for (int i = 0; i < dtUserAll.Rows.Count; i++)
                {
                    selectUserList.Add(
                    new SelectListItem
                    {
                        Value = dtUserAll.Rows[i]["Id"].ToString(),
                        Text = dtUserAll.Rows[i]["Name"].ToString(),

                    });
                }
            }

            loginModel.UserMainList = selectUserList;

            return loginModel;

        }

        [HttpGet]
        [Route("Developer/events")]
        public JsonResult events()
        {
            UsersRolesViewModel urVM = new UsersRolesViewModel();
            var userId = HttpContext.Session.GetString("LoginID");

            SqlParameter[] parameter = {
                          new SqlParameter("@TaskAssignee", userId)
                };

            DataTable dtAll = sqlhelper.ExecuteDataTable("usp_GetAllTaskMasterSP", parameter);
            List<CalendarEvent> lstTask = new List<CalendarEvent>();

            if (dtAll.Rows.Count > 0)
            {
                for (int i = 0; i < dtAll.Rows.Count; i++)
                {
                    CalendarEvent t = new CalendarEvent();

                    t.id =dtAll.Rows[i]["TaskId"].ToString();
                    t.start = dtAll.Rows[i]["Startdate"].ToString();
                    t.end = dtAll.Rows[i]["Startdate"].ToString();
                    t.text = dtAll.Rows[i]["Cnt"].ToString();
                    t.color= dtAll.Rows[i]["color"].ToString();
                    lstTask.Add(t);
              
                }
            }

            return Json(lstTask);
        }

        [Route("Developer/LoadTasklst")]
        public IActionResult LoadTasklst(string CreatedOn)
        {

            try
            {
                var draw = HttpContext.Request.Query["draw"].FirstOrDefault();
                // Skiping number of Rows count  
                var start = Request.Query["start"].FirstOrDefault();
                // Paging Length 10,20  
                var length = Request.Query["length"].FirstOrDefault();
                // Sort Column Name  
                var sortColumn = Request.Query["columns[" + Request.Query["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                // Sort Column Direction ( asc ,desc)  
                var sortColumnDirection = Request.Query["order[0][dir]"].FirstOrDefault();
                // Search Value from (Search box)  
                var searchValue = Request.Query["search[value]"].FirstOrDefault();

                //Paging Size (10,20,50,100)  
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                // Getting all Customer data  

                var userId = HttpContext.Session.GetString("LoginID");

                SqlParameter[] parameter = {
                          new SqlParameter("@TaskAssignee", userId),
                          new SqlParameter("@CreatedOn", CreatedOn),
                         
                };

                DataTable dtAll = sqlhelper.ExecuteDataTable("usp_TaskMasterSP", parameter);

                List<Tasks> selectList = new List<Tasks>();

                if (dtAll.Rows.Count > 0)
                {


                    for (int i = 0; i < dtAll.Rows.Count; i++)
                    {
                        Tasks rm = new Tasks();


                        rm.TaskId = Convert.ToInt32(dtAll.Rows[i]["TaskId"].ToString());
                        rm.TaskName = dtAll.Rows[i]["TaskName"].ToString();
                        rm.ProjectID = dtAll.Rows[i]["ProjectID"].ToString();
                        rm.TaskDescrpition = dtAll.Rows[i]["TaskDescrpition"].ToString();
                        rm.TaskAssignee = dtAll.Rows[i]["TaskAssignee"].ToString();
                        rm.TaskOwner = dtAll.Rows[i]["TaskOwner"].ToString();                       
                        rm.CreatedBy = dtAll.Rows[i]["CreatedBy"].ToString();
                        rm.CreatedOn = Convert.ToDateTime(dtAll.Rows[i]["CreatedOn"].ToString());
                        rm.Startdate = Convert.ToDateTime(dtAll.Rows[i]["Startdate"].ToString());
                        rm.Enddate = Convert.ToDateTime(dtAll.Rows[i]["Enddate"].ToString());
                        rm.IsActive = Convert.ToBoolean(dtAll.Rows[i]["Isactive"].ToString());
                        rm.PrioritySet = dtAll.Rows[i]["PrioritySet"].ToString();
                        rm.TaskStatus = dtAll.Rows[i]["TaskStatus"].ToString();
                        rm.CurrentStatus = dtAll.Rows[i]["CurrentStatus"].ToString();
                        rm.TaskAssigneeType = dtAll.Rows[i]["TaskAssigneeType"].ToString();
                        rm.OwnerType = dtAll.Rows[i]["OwnerType"].ToString();

                        rm.Stage = Convert.ToInt32(dtAll.Rows[i]["Stage"].ToString());

                        selectList.Add(rm);
                    }
                }

                var customerData = selectList;

                //Sorting  
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    if (sortColumn.ToLower() == "TaskName")
                    {
                        customerData = customerData.OrderBy(t => t.TaskName).ToList();
                    }

                }
                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    customerData = (List<Tasks>)customerData.Where(m => m.TaskName == searchValue).ToList();
                }

                //total number of rows count   
                recordsTotal = customerData.Count();
                //Paging   
                var data = customerData.Skip(skip).Take(pageSize).ToList();




                //Returning Json Data  
                return Json(new { draw = "1", recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = customerData });

            }
            catch (Exception)
            {
                throw;
            }

        }

        [Route("Developer/TaskIndex")]
        public IActionResult TaskIndex()
        {
            UsersRolesViewModel ur = new UsersRolesViewModel();
            ur = GetSessionUser();

            var selectList = new List<SelectListItem>();

            selectList.Add(
                    new SelectListItem
                    {
                        Value = "",
                        Text = "Select",

                    });

            DataTable dtAll = sqlhelper.ExecuteDataTable("usp_Taskstatus");

            if (dtAll.Rows.Count > 0)
            {
                for (int i = 0; i < dtAll.Rows.Count; i++)
                {
                    selectList.Add(
                    new SelectListItem
                    {
                        Value = dtAll.Rows[i]["Name"].ToString(),
                        Text = dtAll.Rows[i]["Name"].ToString(),

                    });
                }
            }

            ur.TaskList = selectList;

            var selectOwnerList = new List<SelectListItem>();

            selectOwnerList.Add(
                    new SelectListItem
                    {
                        Value = "",
                        Text = "Select",

                    });

            DataTable dtUserAll = sqlhelper.ExecuteDataTable("SP_GetUserlst");

            if (dtUserAll.Rows.Count > 0)
            {
                for (int i = 0; i < dtUserAll.Rows.Count; i++)
                {
                    selectOwnerList.Add(
                    new SelectListItem
                    {
                        Value = dtUserAll.Rows[i]["Id"].ToString(),
                        Text = dtUserAll.Rows[i]["Name"].ToString(),

                    });
                }
            }

            ur.OwnerMainList = selectOwnerList;

            var selectAssigneeList = new List<SelectListItem>();

            selectAssigneeList.Add(
                    new SelectListItem
                    {
                        Value = "",
                        Text = "Select",

                    });


            DataTable dtAssigneeAll = sqlhelper.ExecuteDataTable("SP_GetUserlst");

            if (dtAssigneeAll.Rows.Count > 0)
            {
                for (int i = 0; i < dtAssigneeAll.Rows.Count; i++)
                {
                    selectAssigneeList.Add(
                    new SelectListItem
                    {
                        Value = dtAssigneeAll.Rows[i]["Id"].ToString(),
                        Text = dtAssigneeAll.Rows[i]["Name"].ToString(),

                    });
                }
            }

            ur.AssigneeMainList = selectAssigneeList;

            var selectPriorityList = new List<SelectListItem>();

            selectPriorityList.Add(
                    new SelectListItem
                    {
                        Value = "",
                        Text = "Select",

                    });


            DataTable dtPriorityAll = sqlhelper.ExecuteDataTable("usp_TaskPriority");

            if (dtPriorityAll.Rows.Count > 0)
            {
                for (int i = 0; i < dtPriorityAll.Rows.Count; i++)
                {
                    selectPriorityList.Add(
                    new SelectListItem
                    {
                        Value = dtPriorityAll.Rows[i]["Id"].ToString(),
                        Text = dtPriorityAll.Rows[i]["Name"].ToString(),

                    });
                }
            }

            ur.PriorityList = selectPriorityList;

            return View(ur);
        }


    }
}
