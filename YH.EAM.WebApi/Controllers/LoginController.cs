using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Victory.Core.Controller;
using YH.EAM.Entity.CodeGenerator;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

using YH.EAM.Entity.Tool;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace YH.EAM.WebApi.Controllers
{
    /// <summary>
    /// 登录接口
    /// </summary>
    public class LoginController :ApiControllerBase
    {

        /// <summary>
        /// 工号登录
        /// </summary>
        /// <param name="workid">工号</param>
        /// <param name="pwd">密码（需要客户端md5加密）</param>
        /// <returns>用户对象</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult LoginByWorkId(string workid,string pwd) 
        {

            Facade.LoginFacade loginFacade = new Facade.LoginFacade();
            Team_User userModel = new Team_User();


            if (!loginFacade.Login(workid, pwd,ref userModel))
            {
                return FailMessage("登录失败:"+loginFacade.Message);
            }


            var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                    new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddMinutes(300)).ToUnixTimeSeconds()}"),
                    new Claim("userId",userModel.Id.ToString()),
                    new Claim("userName", userModel.Name),
                    new Claim("workId", userModel.Workid),

                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConfig.Jwt.ApiKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                   issuer: AppConfig.Jwt.Issuer,
                   audience: AppConfig.Jwt.Audience,
                   claims: claims,
                   expires: DateTime.Now.AddMinutes(300),
                   signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);


            return SuccessResult(jwt, "登录成功！");
        }

    }
}
