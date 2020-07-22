using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Victory.Core.Controller;
using Victory.Core.Extensions;
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

            DataAccess.CodeGenerator.Team_User_Da da = new DataAccess.CodeGenerator.Team_User_Da();

            var data = da.Select;

            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.Where(s => s.Name.Contains(keyword));
            }

            Victory.Core.Models.PageModel page = new Victory.Core.Models.PageModel();
            page.PageIndex = pageIndex;
            page.PageSize = pageSize;
            page.TotalCount = data.Count().ToInt();
            page.ToTalPage = Utility.PageTotal(page.TotalCount, page.PageSize);

            var list = data.Page(pageIndex, pageSize)
                .OrderBy(s => s.Comedate)
                .ToList();
                
            return SuccessResultList(list,page);
        }

    }
}
