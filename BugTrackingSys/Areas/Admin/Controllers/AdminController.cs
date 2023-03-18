using Microsoft.AspNetCore.Mvc;
using SqlHelper;
using System.Data.SqlClient;
using System.Data;
using BugTrackingSys.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Principal;
using BugTrackingSys.Areas.Admin.Models;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;


namespace BugTrackingSys.Areas.Department.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly IConfiguration configuration;

        public string _configuration = "";
        data sqlhelper = new data();

        public AdminController(IConfiguration configuration)
        {

            _configuration = configuration.GetConnectionString("TrackBugsContext");
            sqlhelper = new data(configuration);
        }


        [Route("Admin/Index")]
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


        [Route("Admin/AddUser")]
        public IActionResult AddUser()
        {
            UsersRolesViewModel ur = new UsersRolesViewModel();
            ur = GetSessionUser();
            return View(ur);
        }

        [HttpPost]
        [Route("Admin/AddUser")]
        [AutoValidateAntiforgeryToken]
        public IActionResult AddUser(UsersRolesViewModel loginModel)
        {
            UsersRolesViewModel ur = new UsersRolesViewModel();

            try
            {
                SqlParameter[] parameter = {
                          new SqlParameter("@uname", loginModel.user.Id),
                          new SqlParameter("@pass", loginModel.user.Password),
                          new SqlParameter("@roleid", loginModel.role.Id)
                };

                DataTable dtAll = sqlhelper.ExecuteDataTable("insert_Loginsp", parameter);

                if (dtAll.Rows.Count > 0)
                {

                    ViewBag.Message = "User account Created.";

                }
                else
                {
                    ViewBag.Message = "Unable to created User account.Wrong user name or password.";
                }
                ur = GetSessionUser();
            }

            catch (Exception ex)

            {

                ViewBag.Message = "Error while creating user account";

            }

            return View("AddUser", ur);

        }


        [Route("Admin/AddRole")]
        public IActionResult AddRole()
        {
            UsersRolesViewModel ur = new UsersRolesViewModel();
            ur = GetSessionUser();
            return View(ur);
        }


        [HttpPost]
        [Route("Admin/AddRole")]
        [AutoValidateAntiforgeryToken]
        public IActionResult AddRole(UsersRolesViewModel loginModel)
        {
            UsersRolesViewModel ur = new UsersRolesViewModel();
            try
            {
                SqlParameter[] parameter = {
                          new SqlParameter("@rolename", loginModel.role.name),
                         
                };

                DataTable dtAll = sqlhelper.ExecuteDataTable("insert_Rolesp", parameter);

                if (dtAll.Rows.Count > 0)
                {

                    ViewBag.Message = "Role account Created.";

                }
                else
                {
                    ViewBag.Message = "Unable to created Role account.";
                }

                ur = GetSessionUser();
            }

            catch (Exception ex)

            {

                ViewBag.Message = "Error while creating Role";

            }

            return View("AddRole", ur);

        }


        [Route("Admin/GetRolelst")]
        public IActionResult GetRolelst()
        {
            UsersRolesViewModel ur = new UsersRolesViewModel();
            ur = GetSessionUser();
            return View(ur);

        }

        [Route("Admin/LoadRolelst")]
        public IActionResult LoadRolelst()
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

                DataTable dtAll = sqlhelper.ExecuteDataTable("SP_GetRolelst");

                List<RoleModel> selectList = new List<RoleModel>();

                if (dtAll.Rows.Count > 0)
                {


                    for (int i = 0; i < dtAll.Rows.Count; i++)
                    {
                        RoleModel rm = new RoleModel();

                        rm.Id = dtAll.Rows[i]["ID"].ToString();
                        rm.name = dtAll.Rows[i]["Name"].ToString();


                        selectList.Add(rm);
                    }
                }

                var customerData = selectList;

                //Sorting  
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    if (sortColumn.ToLower() == "name")
                    {
                        customerData = customerData.OrderBy(t => t.name).ToList();
                    }
                   
                }
                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    customerData = (List<RoleModel>)customerData.Where(m => m.name == searchValue).ToList();
                }

                //total number of rows count   
                recordsTotal = customerData.Count();
                //Paging   
                var data = customerData.Skip(skip).Take(pageSize).ToList();

               
               

                //Returning Json Data  
                return Json(new {  draw = "1", recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = customerData });

            }
            catch (Exception)
            {
                throw;
            }

        }

        [Route("Admin/GetUserlst")]
        public IActionResult GetUserlst()
        {
            UsersRolesViewModel ur = new UsersRolesViewModel();
            ur = GetSessionUser();
            return View(ur);
        }
        [Route("Admin/LoadUserlst")]
        public IActionResult LoadUserlst()
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

                DataTable dtAll = sqlhelper.ExecuteDataTable("SP_GetUserlst");

                List<User_RolesModel> selectList = new List<User_RolesModel>();

                if (dtAll.Rows.Count > 0)
                {


                    for (int i = 0; i < dtAll.Rows.Count; i++)
                    {
                        User_RolesModel rm = new User_RolesModel();
                        UsersModel usersModel = new UsersModel();
                        RoleModel roleModel = new RoleModel();

                        usersModel.Id = dtAll.Rows[i]["ID"].ToString();
                        usersModel.Name = dtAll.Rows[i]["Name"].ToString();
                        roleModel.name = dtAll.Rows[i]["RoleName"].ToString();

                        rm.user = usersModel;
                        rm.role = roleModel;


                      
                     


                        selectList.Add(rm);
                    }
                }

                var customerData = selectList;

                //Sorting  
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    if (sortColumn.ToLower() == "name")
                    {
                        customerData = customerData.OrderBy(t => t.user.Name).ToList();
                    }

                }
                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    customerData = (List<User_RolesModel>)customerData.Where(m => m.user.Name == searchValue).ToList();
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

    }
}
