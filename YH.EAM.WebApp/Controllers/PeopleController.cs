using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Victory.Core.Controller;
using Victory.Core.Extensions;
using Victory.Core.Models;
using YH.EAM.DataAccess.CodeGenerator;
using YH.EAM.Entity.Tool;
using YH.EAM.WebApp.Attribute;

namespace YH.EAM.WebApp.Controllers
{
    [Authorize]
    public class PeopleController : TopControllerBase
    {
        [Right(PowerName = "人员信息")]
        public IActionResult Index()
        {
            return View();
        }


        [Right(PowerName = "查询")]
        [HttpPost]
        public IActionResult List(string keyword,int pageIndex,int pageSize)
        {
            PageModel page = new PageModel();
            page.PageIndex = pageIndex;
            page.PageSize = pageSize;


            Team_User_Da da = new Team_User_Da();
            var list = da.ListByWhere(keyword, ref page);


            return SuccessResultList(list,page);
        }

    }
}
