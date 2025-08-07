using AutoMapper;
using Azure;
using boilerplate.web.Data;
using boilerplate.web.Models;
using boilerplate.web.Models.Dto;
using boilerplate.web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using System.Reflection;
using System.Text;
using static NuGet.Packaging.PackagingConstants;

namespace boilerplate.web.Controllers
{
    public class AuthController : Controller
    {
        //private readonly MasterDbContext _context;
        private readonly IUserSessionService _userSessionService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthService _authService;

        public AuthController(IUserSessionService userSessionService, IMapper mapper, IHttpContextAccessor httpContextAccessor, IAuthService authService)
        {
            //_context = context;
            _userSessionService = userSessionService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _authService = authService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(MUser mUser)
        {
            LoginResponseDto loginResponseDto = null;
            if (!ModelState.IsValid)
            {
                APIResponseDto? response = await _authService.Login(new LoginRequestDto { Email = mUser.Email, Password = mUser.Password });
                if (response != null && response.IsSuccess)
                {
                    loginResponseDto = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(response.Result));

                    LoggedInUser userSession = new LoggedInUser { Email = loginResponseDto.User.Email, RoleID = loginResponseDto.User.RoleID, UserID = loginResponseDto.User.ID, UserName = loginResponseDto.User.FullName };
                    _userSessionService.SetUserSession(userSession);
                }
                else
                {
                    TempData["error"] = response?.Message;
                    return RedirectToAction(nameof(Login), "Auth");
                }
            }

            if (loginResponseDto != null)
            {
                var loggeInUser = _userSessionService.GetUserSession();
                List<LoggeInUserRolePermission> mpermission = new List<LoggeInUserRolePermission>();
                if (loggeInUser != null)
                {
                    APIResponseDto? response = await _authService.GetPermissonByRoleID(loginResponseDto.User.RoleID);
                    if (response != null && response.IsSuccess)
                    {
                        mpermission = JsonConvert.DeserializeObject<List<LoggeInUserRolePermission>>(Convert.ToString(response.Result));
                        _userSessionService.SetRolePermissionSession(mpermission);
                    }
                    else
                    {
                        TempData["error"] = response?.Message;
                    }
                }
                //ds = ToDataSet(menus);
                //DataTable table = ds.Tables[0];
                //DataRow[] parentMenus = table.Select("IsMenu = 'True'");

                //var sb = new StringBuilder();
                //string menuString = GenerateUL(parentMenus, table, sb);
                //HttpContext.Session.SetString("menuString", menuString);

                return RedirectToAction(nameof(Index), "Home");

                //return Json(new { status = true, message = "Login Successfull!" });
            }
            else
            {
                return Json(new { status = false, message = "Invalid Email!" });
            }
            return View();
        }

        private string GenerateUL(DataRow[] menu, DataTable table, StringBuilder sb)
        {
            if (menu.Length > 0)
            {
                foreach (DataRow dr in menu)
                {
                    string url = dr["Url"].ToString();
                    string menuText = dr["ModuleName"].ToString();
                    string icon = dr["Icon"].ToString();

                    if (url != "#")
                    {
                        string line = String.Format(@"<li><a href=""{0}""><i class=""{2}""></i> <span>{1}</span></a></li>", url, menuText, icon);
                        sb.Append(line);
                    }

                    string pid = dr["Id"].ToString();
                    string parentId = dr["ParentId"].ToString();

                    DataRow[] subMenu = table.Select(String.Format("ParentId = '{0}'", pid));
                    if (subMenu.Length > 0 && !pid.Equals(parentId))
                    {
                        string line = String.Format(@"<li class=""treeview""><a href=""#""><i class=""{0}""></i> <span>{1}</span><span class=""pull-right-container""><i class=""fa fa-angle-left pull-right""></i></span></a><ul class=""treeview-menu"">", icon, menuText);
                        var subMenuBuilder = new StringBuilder();
                        sb.AppendLine(line);
                        sb.Append(GenerateUL(subMenu, table, subMenuBuilder));
                        sb.Append("</ul></li>");
                    }
                }
            }
            return sb.ToString();
        }
        public DataSet ToDataSet<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dataTable);
            return ds;
        }

        public async Task<ActionResult> Logout()
        {
            _httpContextAccessor.HttpContext?.Session.Clear(); // Clear data first
            await _httpContextAccessor.HttpContext?.Session.CommitAsync(); // Ensure changes are saved
            _httpContextAccessor.HttpContext?.Response.Cookies.Delete(".AspNetCore.Session"); // Remove cook

            return RedirectToAction("Login", "Auth");
        }
    }
}
