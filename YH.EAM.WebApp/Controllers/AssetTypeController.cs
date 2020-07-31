using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Victory.Core.Controller;
using Victory.Core.Models;
using YH.EAM.DataAccess.CodeGenerator;
using YH.EAM.Entity.CodeGenerator;
using YH.EAM.WebApp.Attribute;


namespace YH.EAM.WebApp.Controllers
{

    [Authorize]
    public class AssetTypeController : TopControllerBase
    {
        [Right(PowerName = "资产类型")]
        public IActionResult Index()
        {
            return View();
        }

        [Right(PowerName = "查询")]
        [HttpPost]
        public IActionResult List(string keyword, int pageIndex, int pageSize)
        {

            PageModel page = new PageModel();
            page.PageIndex = pageIndex;
            page.PageSize = pageSize;


            TEAM_Type_Da da = new TEAM_Type_Da();
            var list = da.ListByWhere(keyword, ref page);
            return SuccessResultList(list, page);
        }




        [Right(PowerName = "添加")]
        [HttpPost]
        public IActionResult Add(TEAM_Type model)
        {

            TEAM_Type_Da da = new TEAM_Type_Da();
            da.Insert(model);
            return SuccessMessage("添加成功！");

        }


        [Right(PowerName = "修改")]
        [HttpPost]
        public IActionResult Update(TEAM_Type model)
        {
            TEAM_Type_Da da = new TEAM_Type_Da();
            da.Update(model);
            return SuccessMessage("成功！");
        }


        [Right(PowerName = "删除")]
        [HttpPost]
        public IActionResult Del(int id)
        {
            TEAM_Type_Da da = new TEAM_Type_Da();

            if (da.Delete(s => s.Id == id) > 0)
            {
                return SuccessMessage("已删除！");

            }
            return FailMessage();
        }





    }

}