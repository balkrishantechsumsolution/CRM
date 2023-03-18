using BugTrackingSys.Models;
using Microsoft.AspNetCore.Mvc;
using SqlHelper;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using BugTrackingSys.Areas.Admin.Models;

namespace BugTrackingSys.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CommonFunController : Controller
    {
        private readonly IConfiguration configuration;

        public string _configuration = "";
        data sqlhelper = new data();

        public CommonFunController(IConfiguration configuration)
        {

            _configuration = configuration.GetConnectionString("TrackBugsContext");
            sqlhelper = new data(configuration);
        }

        public IActionResult Index()
        {
            return View();
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

        [Route("Admin/AddProject")]
        public IActionResult AddProject()
        {
            UsersRolesViewModel ur = new UsersRolesViewModel();           
            ur = GetSessionUser();
            return View(ur);
        }

        [HttpPost]
        [Route("Admin/AddProject")]
        [AutoValidateAntiforgeryToken]
        public IActionResult AddProject(UsersRolesViewModel loginModel)
        {
            UsersRolesViewModel ur = new UsersRolesViewModel();
            try
            {
                SqlParameter[] parameter = {
                          new SqlParameter("@Name", loginModel.project.Name),
                          new SqlParameter("@Description", loginModel.project.Description),
                          new SqlParameter("@CreatedBy", loginModel.project.CreatedBy)
                };

                DataTable dtAll = sqlhelper.ExecuteDataTable("insert_ProjectSP", parameter);

                if (dtAll.Rows.Count > 0)
                {

                    ViewBag.Message = "Project account Created.";

                }
                else
                {
                    ViewBag.Message = "Unable to created Project account.";
                }


              
                ur = GetSessionUser();

            }

            catch (Exception ex)

            {

                ViewBag.Message = "Error while creating Project";

            }

            return View("AddProject", ur);

        }

        [Route("Admin/GetProjectlst")]
        public IActionResult GetProjectlst()
        {
            UsersRolesViewModel ur = new UsersRolesViewModel();
            ur = GetSessionUser();
            return View(ur);
        }


        [Route("Admin/LoadProjectlst")]
        public IActionResult LoadProjectlst()
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

                DataTable dtAll = sqlhelper.ExecuteDataTable("usp_Project");

                List<ProjectViewModel> selectList = new List<ProjectViewModel>();

                if (dtAll.Rows.Count > 0)
                {


                    for (int i = 0; i < dtAll.Rows.Count; i++)
                    {
                        ProjectViewModel rm = new ProjectViewModel();
                       

                        rm.Id = dtAll.Rows[i]["ID"].ToString();
                        rm.Name = dtAll.Rows[i]["Name"].ToString();
                        rm.Description = dtAll.Rows[i]["Description"].ToString();
                        rm.CreatedOn = dtAll.Rows[i]["CreatedOn"].ToString();
                        rm.CreatedBy = dtAll.Rows[i]["CreatedBy"].ToString();
                        rm.Isactive = dtAll.Rows[i]["Isactive"].ToString();

                        selectList.Add(rm);
                    }
                }

                var customerData = selectList;

                //Sorting  
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    if (sortColumn.ToLower() == "name")
                    {
                        customerData = customerData.OrderBy(t => t.Name).ToList();
                    }

                }
                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    customerData = (List<ProjectViewModel>)customerData.Where(m => m.Name == searchValue).ToList();
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
