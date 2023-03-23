using Microsoft.AspNetCore.Mvc;
using SqlHelper;
using System.Data.SqlClient;
using System.Data;
using BugTrackingSys.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Principal;
using BugTrackingSys.Areas.Support.Models;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;


namespace BugTrackingSys.Areas.Support.Controllers
{
    [Area("Support")]
    public class SupportController : Controller
    {
        private readonly IConfiguration configuration;

        public string _configuration = "";
        data sqlhelper = new data();

        public SupportController(IConfiguration configuration)
        {

            _configuration = configuration.GetConnectionString("TrackBugsContext");
            sqlhelper = new data(configuration);
        }


        [HttpGet]
        [Route("Support/events")]
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

                    t.id = dtAll.Rows[i]["TaskId"].ToString();
                    t.start = dtAll.Rows[i]["Startdate"].ToString();
                    t.end = dtAll.Rows[i]["Startdate"].ToString();
                    t.text = dtAll.Rows[i]["Cnt"].ToString();
                    t.color = dtAll.Rows[i]["color"].ToString();
                    lstTask.Add(t);

                }
            }

            return Json(lstTask);
        }


        [Route("Support/Index")]
        public IActionResult Index()
        {
            UsersRolesViewModel ur = new UsersRolesViewModel();
            ur = GetSessionUser();
            return View(ur);
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


       
    }
}
