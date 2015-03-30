using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using AtlanticDX.ERP.Filters;
using AtlanticDX.ERP.Models;
using PrivilegeFramework;
using YuShang.ERP.Entities.Privileges;
using AtlanticDX.ERP.Areas.Configs.Models;

namespace AtlanticDX.ERP.Areas.Configs.Controllers
{
    [ComplexAuthorize(Roles = "系统管理员,Boss,Manager")]
    public class UserMgmtController : PrivilegeFramework.NavigationProps.NavigationLoopSolvedController// Controller
    {
        // GET: /Configs/UserMgmt
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Index(int page = 1, int rows = 10)
        {
            using (AtlanticDXContext context = new AtlanticDXContext())
            {
                IEnumerable<SysUser> users = users = context.Users.Where(u => u.Id > 0)//小于等于0的用户不允许修改，是系统内置的
                    .OrderBy(m => m.CTIME).Skip((page - 1) * rows).Take(rows).ToList();
                IEnumerable<SysUserViewModel> userViewModels = users.Select(m => new SysUserViewModel(m));
                return Json(new { total = context.Users.Count(), rows = userViewModels });
            }
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add(AddSysUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new SysUser
                {
                    UserName = model.UserName,
                    Name = model.Name,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    CreatorUserName = HttpContext.User.Identity.Name,
                    CTIME = DateTime.Now
                };

                List<string> errorMsgs = new List<string>();

                await HandleUserAddWithRole(model, user, errorMsgs);

                foreach (string error in errorMsgs)
                {
                    ModelState.AddModelError("", error);
                }
            }
            var allErrors = ModelState.GetModelStateErrors();
            return Json(allErrors);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(SysUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id < 1)
                {
                    ModelState.AddModelError(string.Empty, "系统内置用户不允许修改。");
                }

                SysUser currentUser = UserManager.FindById(model.Id);
                if (currentUser == null)
                {
                    ModelState.AddModelError("", "用户不存在");
                }
                else if (model.UserName != currentUser.UserName && UserManager.FindByName(model.UserName) != null)
                {
                    ModelState.AddModelError("", "用户名已存在");
                }
                else
                {
                    List<string> errorMsgs = new List<string>();

                    await HandleUserEditWithRole(model, currentUser, errorMsgs);

                    foreach (string error in errorMsgs)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            var allErrors = ModelState.GetModelStateErrors();
            return Json(allErrors);
        }

        private async Task HandleUserAddWithRole(AddSysUserViewModel model, SysUser user, List<string> errorMsgs)
        {
            var result = await UserManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                errorMsgs.AddRange(result.Errors);
                return;
            }
            if (user.Id < 1)
            {
                user = await UserManager.FindByNameAsync(user.UserName);
            }
            if (!UserManager.IsInRole(user.Id, model.RoleName))
            {
                result = IdentityResult.Success;
                result = await UserManager.AddToRoleAsync(user.Id, model.RoleName);
                if (!result.Succeeded)
                {
                    errorMsgs.AddRange(result.Errors);
                }
            }
        }

        private async Task HandleUserEditWithRole(SysUserViewModel model,
            SysUser currentUser, List<string> errorMsgs)
        {
            currentUser.UserName = model.UserName;
            currentUser.Name = model.Name;
            currentUser.Email = model.Email;
            currentUser.PhoneNumber = model.PhoneNumber;
            var result = await UserManager.UpdateAsync(currentUser);
            if (!result.Succeeded)
            {
                errorMsgs.AddRange(result.Errors);
                return;
            }

            result = IdentityResult.Success;
            if (!UserManager.IsInRole(currentUser.Id, model.RoleName))
                result = await UserManager.AddToRoleAsync(currentUser.Id, model.RoleName);
            if (!result.Succeeded)
            {
                errorMsgs.AddRange(result.Errors);
                return;
            }
            var roleList = UserManager.GetRoles(currentUser.Id);
            foreach (var r in roleList)
            {
                if (r.Equals(model.RoleName))
                    continue;
                var res = UserManager.RemoveFromRole(currentUser.Id, r);
                if (!result.Succeeded)
                {
                    errorMsgs.AddRange(result.Errors);
                }
            }
        }

        [HttpPost]
        public async Task<ActionResult> Remove(SysUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                SysUser currentUser = UserManager.FindById(model.Id);
                if (currentUser == null)
                {
                    ModelState.AddModelError("", "用户不存在。");
                }
                if (currentUser != null && currentUser.Id < 1)
                //if (model.UserName == "admin")
                {
                    ModelState.AddModelError("", "系统内置用户不能删除。");
                    //"admin用户不允许删除");
                }
                else
                {
                    var result = await UserManager.DeleteAsync(currentUser);
                    if (!result.Succeeded)
                    {
                        foreach (string error in result.Errors)
                        {
                            ModelState.AddModelError("", error);
                        }
                    }
                }
            }
            var allErrors = ModelState.GetModelStateErrors();
            return Json(allErrors);
        }
    }
}