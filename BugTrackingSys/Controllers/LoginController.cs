using BugTrackingSys.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using SqlHelper;
using System.Data;
using System.Data.SqlClient;
using System.Security.Principal;

namespace BugTrackingSys.Controllers
{
    [Route("LoginController")]
    public class LoginController : Controller
    {
        // requires using Microsoft.Extensions.Configuration;
        private readonly IConfiguration configuration;
        private readonly ILogger _logger;
        public string _configuration = "";
        data sqlhelper = new data();
        public LoginController(ILogger<LoginController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration.GetConnectionString("TrackBugsContext");
            sqlhelper = new data(configuration);
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [Route("LoginController/Login")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Login(User_RolesModel loginModel)
        {
            try
            {
                SqlParameter[] parameter = {
                          new SqlParameter("@uname", loginModel.user.Id),
                          new SqlParameter("@pass", loginModel.user.Password)
                };

                DataTable dtAll = sqlhelper.ExecuteDataTable("usp_Login", parameter);

                if (dtAll.Rows.Count > 0)
                {

                    HttpContext.Session.SetString("UserType", dtAll.Rows[0]["role_id"].ToString());
                    HttpContext.Session.SetString("LoginID", dtAll.Rows[0]["name"].ToString());
                    HttpContext.Session.SetString("RoleName", dtAll.Rows[0]["roleName"].ToString());

                    var UserType = HttpContext.Session.GetString("UserType");

                    if (UserType == "1")
                    {
                        return RedirectToAction(actionName: "Index", controllerName: "Admin", new { area = "Admin" });
                    }
                    else if (UserType == "2")
                    {
                        return RedirectToAction(actionName: "Index", controllerName: "Developer", new { area = "Developer" });
                    }
                    else if (UserType == "3")
                    {
                        return RedirectToAction(actionName: "Index", controllerName: "Support", new { area = "Support" });
                    }

                }
                else
                {
                    ViewBag.Message = "Unable to Login account.Wrong user name or password.";
                }

            }

            catch (Exception ex)

            {

                ViewBag.Message = "Error while creating user account";

            }

            return View(loginModel);

        }
    }
}
