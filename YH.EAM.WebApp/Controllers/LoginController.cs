using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Victory.Core.Encrypt;
using Victory.Core.Controller;
using YH.EAM.Entity.CodeGenerator;
using System.Security.Claims;

namespace YH.EAM.WebApp.Controllers
{
    public class LoginController : TopControllerBase
    {



        public IActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 工号登录
        /// </summary>
        /// <param name="workid"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> LoginIn(string workid, string password)
        {
            //这里只是简单处理了
            if (string.IsNullOrEmpty(workid) || string.IsNullOrEmpty(password))
            {
                return FailMessage("请输入账号密码登录！");
            }

            Facade.LoginFacade loginFacade = new Facade.LoginFacade();

            password = Md5.Encrypt32(password);

     
            Team_User userModel = new Team_User();

            if (!loginFacade.Login(workid, password, ref userModel))
            {
                return FailMessage(loginFacade.Message);
            }

            var claims = new List<Claim>(){
                   new Claim("userId",userModel.Id.ToString()),
                    new Claim("userName", userModel.Name),
                    new Claim("workId", userModel.Workid),
                    new Claim("sex", userModel.Sex.ToString())
               };

            //创建身份信息
            var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Customer"));

            //core 自带的登录验证框架
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal, new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddMinutes(120),
                IsPersistent = false,
                AllowRefresh = true

            });

            return SuccessMessage("登录成功！");

        }

            /// <summary>
            /// 退出登录
            /// </summary>
            /// <returns></returns>
        public async Task<IActionResult> LoginOut()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("/Login/Index");

        }
    }
}
