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
        public  JsonResult GetEvents()
        {
            List<CalendarEvent> lst= new List<CalendarEvent>();
            CalendarEvent cls= new CalendarEvent();

            cls.start ="2023-03-26T12:00:00";
            cls.end = "2023-03-28T12:00:00";
            cls.id = "1";
            cls.text = "Bala Event 1";
          
            cls.color = "#cc4125";
            lst.Add(cls);

            cls = new CalendarEvent();

            cls.start = "2023-03-25T00:00:00";
            cls.end = "2023-03-26T00:00:00";
            cls.id = "2";
            cls.text = "Zenual Event 2";
           
            cls.color = "#6aa84f";
            lst.Add(cls);

            return Json(lst);
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
