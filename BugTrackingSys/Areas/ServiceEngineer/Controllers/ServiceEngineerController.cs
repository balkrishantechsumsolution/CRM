using Microsoft.AspNetCore.Mvc;
using SqlHelper;
using System.Data.SqlClient;
using System.Data;
using LeedManagement.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Principal;
using LeedManagement.Areas.ServiceEngineer.Models;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.Hosting.Internal;
using System.Diagnostics;
using System.Threading.Tasks;


namespace LeedManagement.Areas.ServiceEngineer.Controllers
{
    [Area("ServiceEngineer")]
    public class ServiceEngineerController : Controller
    {
        private readonly IConfiguration configuration;

        public string _configuration = "";
        data sqlhelper = new data();

        public ServiceEngineerController(IConfiguration configuration)
        {

            _configuration = configuration.GetConnectionString("TrackBugsContext");
            sqlhelper = new data(configuration);
        }


        [Route("ServiceEngineer/Index")]
        public IActionResult Index()
        {
            UsersRolesViewModel ur = new UsersRolesViewModel();
            ur = GetSessionUser();


            return View(ur);
        }

        [Route("ServiceEngineer/MainHTML")]
        public IActionResult MainHTML(string CreatedOn)
        {
            UsersRolesViewModel ur = new UsersRolesViewModel();
            ViewBag.CreatedOn = CreatedOn;
            ur = GetSessionUser();
            return View(ur);
        }

        [HttpPost]
        [Route("ServiceEngineer/Index")]
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
            Tasks tasks = new Tasks();
            ProjectViewModel projectViewModel = new ProjectViewModel();




            usersModel.Id = "0";
            usersModel.Password = "";

            usersModel.Name = LoginID;
            roleModel.name = RoleName;
            roleModel.Id = "0";



            projectViewModel.id = "0";

            loginUserRoleModel.user = usersModel;
            loginUserRoleModel.role = roleModel;



            loginModel.user = usersModel;
            loginModel.role = roleModel;

            loginModel.project = projectViewModel;


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

            //List<FileViewModel> lstfileAttach = new List<FileViewModel>();
            //FileViewModel fileAttach = new FileViewModel();
            //lstfileAttach.Add(fileAttach);

            //loginModel.fileViewModel = fileAttach;

            //loginModel.fileAttach = lstfileAttach;

            //Tasks task = new Tasks();
            //loginModel.tasks = task;

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

            loginModel.AssigneeMainList = selectAssigneeList;

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

            loginModel.PriorityList = selectPriorityList;

            var selectProjectList = new List<SelectListItem>();

            selectProjectList.Add(
                    new SelectListItem
                    {
                        Value = "",
                        Text = "Select",

                    });


            DataTable dtProjectAll = sqlhelper.ExecuteDataTable("usp_Project");

            if (dtProjectAll.Rows.Count > 0)
            {
                for (int i = 0; i < dtProjectAll.Rows.Count; i++)
                {
                    selectProjectList.Add(
                    new SelectListItem
                    {
                        Value = dtProjectAll.Rows[i]["Id"].ToString(),
                        Text = dtProjectAll.Rows[i]["Name"].ToString(),

                    });
                }
            }

            loginModel.ProjectList = selectProjectList;





            var selectTaskstatusList = new List<SelectListItem>();

            selectTaskstatusList.Add(
                    new SelectListItem
                    {
                        Value = "",
                        Text = "Select",

                    });

            DataTable dtTaskstatusAll = sqlhelper.ExecuteDataTable("usp_Taskstatus");

            if (dtTaskstatusAll.Rows.Count > 0)
            {
                for (int i = 0; i < dtTaskstatusAll.Rows.Count; i++)
                {
                    selectTaskstatusList.Add(
                    new SelectListItem
                    {
                        Value = dtTaskstatusAll.Rows[i]["Name"].ToString(),
                        Text = dtTaskstatusAll.Rows[i]["Name"].ToString(),

                    });
                }
            }

            loginModel.TaskList = selectTaskstatusList;


            return loginModel;

        }

        [HttpGet]
        [Route("ServiceEngineer/events")]
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

        [Route("ServiceEngineer/LoadTasklst")]
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

        [Route("ServiceEngineer/TaskIndex")]
        public IActionResult TaskIndex(int? taskId)
        {
            UsersRolesViewModel ur = new UsersRolesViewModel();
            ur = GetSessionUser();


            if (taskId != null)
            {
                List<FileViewModel> lstfileAttach = new List<FileViewModel>();

                SqlParameter[] paraTask = {
                          new SqlParameter("@TaskId", taskId),
               };

                DataSet dtTaskDtl = sqlhelper.ExecuteDataSet("usp_TaskByIDSP", paraTask);

                DataTable dtTask = dtTaskDtl.Tables[0];

                if (dtTask.Rows.Count > 0)
                {
                    Tasks rm = new Tasks();

                    ProjectViewModel project = new ProjectViewModel();


                    rm.TaskId = Convert.ToInt32(dtTask.Rows[0]["TaskId"].ToString());
                    rm.TaskName = dtTask.Rows[0]["TaskName"].ToString();
                    rm.ProjectID = dtTask.Rows[0]["ProjectID"].ToString();
                    rm.TaskDescrpition = dtTask.Rows[0]["TaskDescrpition"].ToString();
                    rm.TaskAssignee = dtTask.Rows[0]["TaskAssignee"].ToString();
                    rm.TaskOwner = dtTask.Rows[0]["TaskOwner"].ToString();
                    rm.CreatedBy = dtTask.Rows[0]["CreatedBy"].ToString();
                    rm.CreatedOn = Convert.ToDateTime(dtTask.Rows[0]["CreatedOn"].ToString());
                    rm.Startdate = Convert.ToDateTime(dtTask.Rows[0]["Startdate"].ToString());
                    rm.Enddate = Convert.ToDateTime(dtTask.Rows[0]["Enddate"].ToString());
                    rm.IsActive = Convert.ToBoolean(dtTask.Rows[0]["Isactive"].ToString());
                    rm.PrioritySet = dtTask.Rows[0]["PrioritySet"].ToString();
                    rm.TaskStatus = dtTask.Rows[0]["TaskStatus"].ToString();
                    rm.CurrentStatus = dtTask.Rows[0]["CurrentStatus"].ToString();
                    rm.TaskAssigneeType = dtTask.Rows[0]["TaskAssigneeType"].ToString();
                    rm.OwnerType = dtTask.Rows[0]["OwnerType"].ToString();
                    rm.Stage = Convert.ToInt32(dtTask.Rows[0]["Stage"].ToString());

                    project.id = dtTask.Rows[0]["ProjectID"].ToString();

                    ur.tasks = rm;
                    ur.project = project;

                }



                DataTable dtfileAttach = dtTaskDtl.Tables[1];



                for (int i = 0; i < dtfileAttach.Rows.Count; i++)
                {
                    FileViewModel fileAttach = new FileViewModel();

                    fileAttach.FileID = Convert.ToInt32(dtfileAttach.Rows[i]["FileID"].ToString());
                    fileAttach.FileName = dtfileAttach.Rows[i]["FileName"].ToString();
                    fileAttach.FileData = dtfileAttach.Rows[i]["FileData"].ToString();
                    fileAttach.FileType = dtfileAttach.Rows[i]["FileType"].ToString();
                    fileAttach.IsActive = dtfileAttach.Rows[i]["IsActive"].ToString();

                    //String path = HostingEnvironment.ApplicationPhysicalPath + "Image\\Capture.PNG";
                    //string fname = Path.GetFileName(path);
                    byte[] fileBytes = System.Convert.FromBase64String(fileAttach.FileData);
                    //string fileName = fname;
                    //return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileAttach.FileName);

                    lstfileAttach.Add(fileAttach);
                }

                ur.fileAttach = lstfileAttach;


                List<TasksRemark> lstTaskRemark = new List<TasksRemark>();

                DataTable dtTaskRemark = dtTaskDtl.Tables[2];

                for (int i = 0; i < dtTaskRemark.Rows.Count; i++)
                {
                    TasksRemark tasksRemark = new TasksRemark();


                    tasksRemark.id = Convert.ToInt32(dtTaskRemark.Rows[i]["id"].ToString());
                    tasksRemark.TaskId = Convert.ToInt32(dtTaskRemark.Rows[i]["TaskId"].ToString());
                    tasksRemark.Remarks = dtTaskRemark.Rows[i]["Remarks"].ToString();
                    tasksRemark.RemarksOn = Convert.ToDateTime(dtTaskRemark.Rows[i]["RemarksOn"].ToString());
                    tasksRemark.CurrentStatus = dtTaskRemark.Rows[i]["CurrentStatus"].ToString();
                    tasksRemark.EndDate = Convert.ToDateTime(dtTaskRemark.Rows[i]["EndDate"].ToString());
                    tasksRemark.RemarksBy = dtTaskRemark.Rows[i]["RemarksBy"].ToString();


                    lstTaskRemark.Add(tasksRemark);
                }
                ur.taskRemarkList = lstTaskRemark;
            }
            else
            {
                Tasks rm = new Tasks();

                string dtStartDate = DateTime.Now.ToString("dd/MM/yyyy");
                string dtEndDate = DateTime.Now.ToString("dd/MM/yyyy");

                rm.TaskId = 0;
                rm.id = 0;
                rm.Startdate = Convert.ToDateTime(dtStartDate);
                rm.Enddate = Convert.ToDateTime(dtEndDate);

                ur.tasks = rm;
            }

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


            var selectProjectList = new List<SelectListItem>();

            selectProjectList.Add(
                    new SelectListItem
                    {
                        Value = "",
                        Text = "Select",

                    });


            DataTable dtProjectAll = sqlhelper.ExecuteDataTable("usp_Project");

            if (dtProjectAll.Rows.Count > 0)
            {
                for (int i = 0; i < dtProjectAll.Rows.Count; i++)
                {
                    selectProjectList.Add(
                    new SelectListItem
                    {
                        Value = dtProjectAll.Rows[i]["Id"].ToString(),
                        Text = dtProjectAll.Rows[i]["Name"].ToString(),

                    });
                }
            }

            ur.ProjectList = selectProjectList;






            //List<FileViewModel> lstfileAttach = new List<FileViewModel>();
            //FileViewModel fileAttach = new FileViewModel();
            //lstfileAttach.Add(fileAttach);
            //ur.fileViewModel= fileAttach;

            //ur.fileAttach = lstfileAttach;




            return View("TaskIndex", ur);
        }
        [HttpPost]
        [Route("ServiceEngineer/TaskIndex")]
        [AutoValidateAntiforgeryToken]
        public IActionResult TaskIndex(UsersRolesViewModel loginModel)
        {

            var validator = new TasksValidator();
            var valResult = validator.Validate(loginModel);

            if (valResult.Errors.Count != 0)
            {
                valResult.Errors.ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));
                UsersRolesViewModel rvS = new UsersRolesViewModel();
                rvS = GetSessionUser();
                return View("TaskIndex", rvS);
            }

            UsersRolesViewModel rv = new UsersRolesViewModel();

            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("FileName");
            dt.Columns.Add("FileData");
            dt.Columns.Add("FileType");



            UsersRolesViewModel ur = new UsersRolesViewModel();
            var strBase64 = "";
            var contentType = "";
            var path = "";
            try
            {

                if (loginModel.files == null || loginModel.files.Count == 0)
                {

                }
                else
                {

                    var filePaths = new List<string>();
                    foreach (var formFile in loginModel.files)
                    {
                        if (formFile.Length > 0)
                        {
                            // full path to file in temp location
                            path = Path.Combine(
                      Directory.GetCurrentDirectory(), "wwwroot//Attachment//",
                      formFile.FileName);


                            filePaths.Add(path);
                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                DataRow _row = dt.NewRow();
                                formFile.CopyToAsync(stream);
                                strBase64 = ConvertToBase64(stream);
                                contentType = formFile.ContentType;

                                _row["FileName"] = "wwwroot//Attachment//" + formFile.FileName;
                                _row["FileData"] = strBase64;
                                _row["FileType"] = contentType.ToString();
                                dt.Rows.Add(_row);
                            }
                        }
                    }
                }

                var userId = HttpContext.Session.GetString("LoginID");
                var UserType = HttpContext.Session.GetString("UserType");

                SqlParameter[] parameter = {
                          new SqlParameter("@TaskId", loginModel.tasks.TaskId),
                          new SqlParameter("@TaskName", loginModel.tasks.TaskName),
                          new SqlParameter("@ProjectID", loginModel.project.id),
                          new SqlParameter("@TaskDescrpition", loginModel.tasks.TaskDescrpition),
                          new SqlParameter("@TaskAssignee", loginModel.tasks.TaskAssignee),
                          new SqlParameter("@TaskOwner", userId),
                          new SqlParameter("@CreatedBy", userId),
                          new SqlParameter("@CreatedOn", loginModel.tasks.Startdate),
                          new SqlParameter("@Startdate", loginModel.tasks.Startdate),
                          new SqlParameter("@Enddate", loginModel.tasks.Enddate),
                          new SqlParameter("@PrioritySet", loginModel.tasks.PrioritySet),
                          new SqlParameter("@TaskStatus", loginModel.tasks.TaskStatus),
                          new SqlParameter("@IsActive", "1"),
                          new SqlParameter("@CurrentStatus", loginModel.tasks.TaskStatus),
                          new SqlParameter("@Stage","1"),
                          new SqlParameter("@TaskAssigneeType", UserType),
                          new SqlParameter("@OwnerType", UserType),
                          new SqlParameter("@KeyField", ""),
                          new SqlParameter("@taskFileDetailsData",dt),


                };

                DataTable dtAll = sqlhelper.ExecuteDataTableNon("insert_TaskMasterSP", parameter);

                var m_TaskID = dtAll.Rows[0]["TaskID"].ToString();

                if (dtAll.Rows.Count > 0)
                {

                    ViewBag.Message = "Task Created.";

                }
                else
                {
                    ViewBag.Message = "Unable to created Task.";
                }




                rv = GetSessionUser();



                List<FileViewModel> lstfileAttach = new List<FileViewModel>();

                SqlParameter[] paraTask = {
                          new SqlParameter("@TaskId", m_TaskID),
               };

                DataTable dtfileAttach = sqlhelper.ExecuteDataTable("usp_TaskFileAttachSP", paraTask);
                for (int i = 0; i < dtfileAttach.Rows.Count; i++)
                {
                    FileViewModel fileAttach = new FileViewModel();

                    fileAttach.FileID = Convert.ToInt32(dtfileAttach.Rows[i]["FileID"].ToString());
                    fileAttach.FileName = dtfileAttach.Rows[i]["FileName"].ToString();
                    fileAttach.FileData = dtfileAttach.Rows[i]["FileData"].ToString();
                    fileAttach.FileType = dtfileAttach.Rows[i]["FileType"].ToString();
                    fileAttach.IsActive = dtfileAttach.Rows[i]["IsActive"].ToString();

                    //String path = HostingEnvironment.ApplicationPhysicalPath + "Image\\Capture.PNG";
                    //string fname = Path.GetFileName(path);
                    byte[] fileBytes = System.Convert.FromBase64String(fileAttach.FileData);
                    //string fileName = fname;
                    //return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileAttach.FileName);

                    lstfileAttach.Add(fileAttach);
                }
                rv.fileAttach = lstfileAttach;



                List<TasksRemark> lstTaskRemark = new List<TasksRemark>();

                SqlParameter[] paraTaskRemark = {
                          new SqlParameter("@TaskId", m_TaskID),
               };

                DataTable dtTaskRemark = sqlhelper.ExecuteDataTable("usp_TaskRemarkSP", paraTaskRemark);
                for (int i = 0; i < dtTaskRemark.Rows.Count; i++)
                {
                    TasksRemark tasksRemark = new TasksRemark();


                    tasksRemark.id = Convert.ToInt32(dtTaskRemark.Rows[i]["id"].ToString());
                    tasksRemark.TaskId = Convert.ToInt32(dtTaskRemark.Rows[i]["TaskId"].ToString());
                    tasksRemark.Remarks = dtTaskRemark.Rows[i]["Remarks"].ToString();
                    tasksRemark.RemarksOn = Convert.ToDateTime(dtTaskRemark.Rows[i]["RemarksOn"].ToString());
                    tasksRemark.CurrentStatus = dtTaskRemark.Rows[i]["CurrentStatus"].ToString();
                    tasksRemark.EndDate = Convert.ToDateTime(dtTaskRemark.Rows[i]["EndDate"].ToString());
                    tasksRemark.RemarksBy = dtTaskRemark.Rows[i]["RemarksBy"].ToString();


                    lstTaskRemark.Add(tasksRemark);
                }
                rv.taskRemarkList = lstTaskRemark;

                Tasks rm = new Tasks();
                rm.TaskId = 0;
                rm.id = 0;

                rv.tasks = rm;

            }

            catch (Exception ex)
            {

                //ViewBag.Message = "Error while creating Task";
                return View("/Home/Error");

            }

            return View("TaskIndex", rv);

        }


        [Route("ServiceEngineer/DownloadFile")]
        [AutoValidateAntiforgeryToken]
        public FileResult DownloadFile(int? fileid)
        {
            byte[] bytes = new byte[800];
            string fileName = "", contentType = "";


            SqlParameter[] paraTask = {
                          new SqlParameter("@ID", fileid),
               };

            DataTable dtfileAttach = sqlhelper.ExecuteDataTable("usp_FileAttachByIDSP", paraTask);

            if (dtfileAttach.Rows.Count > 0)
            {
                fileName = dtfileAttach.Rows[0]["FileName"].ToString();
                bytes = Convert.FromBase64String(dtfileAttach.Rows[0]["FileData"].ToString());
                contentType = dtfileAttach.Rows[0]["FileType"].ToString();
            }


            return File(bytes, contentType, fileName);
        }


        public string ConvertToBase64(Stream stream)
        {
            if (stream is MemoryStream memoryStream)
            {
                return Convert.ToBase64String(memoryStream.ToArray());
            }

            var bytes = new Byte[(int)stream.Length];

            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(bytes, 0, (int)stream.Length);

            return Convert.ToBase64String(bytes);
        }


    }

}