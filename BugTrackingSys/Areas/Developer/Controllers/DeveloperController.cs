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



    }
}
