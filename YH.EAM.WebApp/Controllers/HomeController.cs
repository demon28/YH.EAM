using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YH.EAM.Entity.Model;
using YH.EAM.WebApp.Models;

namespace YH.EAM.WebApp.Controllers
{

    [Authorize]
    public class HomeController : Victory.Core.Controller.TopControllerBase
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 获取登录用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetLoginUser()
        {
            SysUserInfoModel usermodel = new SysUserInfoModel();

            var userinfo = (HttpContext.User.Identity as System.Security.Claims.ClaimsIdentity);

            usermodel.userId = int.Parse(userinfo.FindFirst("userId").Value);
            usermodel.userName = userinfo.FindFirst("userName").Value;
            usermodel.workId = userinfo.FindFirst("workId").Value;
            usermodel.sex = int.Parse(userinfo.FindFirst("sex").Value);

            return SuccessResult(usermodel);
        }
    }
}
