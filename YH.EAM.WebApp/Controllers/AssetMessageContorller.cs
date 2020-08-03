//控制器模板 v1.1
//2020-7-31
//Near

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

namespace YH.EAM.DataAccess.CodeGenerator
{

    [Authorize]
    public class  AssetMessageController : TopControllerBase
    {
         [Right(PowerName = "资产列表")]
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


             Team_Message_Da da = new  Team_Message_Da();
            var list = da.ListByWhere(keyword, ref page);
            return SuccessResultList(list,page);
        }




        [Right(PowerName = "添加")]
        [HttpPost]
        public IActionResult Add(Team_Message model)
        {
            if(model!=null)
            { 
                model.Inbound_Date=model.Createtime;
            }

            Team_Message_Da da = new Team_Message_Da();
            da.Insert(model);
            return SuccessMessage("添加成功！");

        }


        [Right(PowerName = "修改")]
        [HttpPost]
        public IActionResult Update(Team_Message model)
        {
            Team_Message_Da da = new Team_Message_Da();
            da.Update(model);
            return SuccessMessage("成功！");
        }


        [Right(PowerName = "删除")]
        [HttpPost]
        public IActionResult Del(int id)
        {
            Team_Message_Da da = new Team_Message_Da();

            if (da.Delete(s => s.Id == id) > 0)
            {
                return SuccessMessage("已删除！");

            }
            return FailMessage();
        }



    
    
    }

}