﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Victory.Core.Controller;
using YH.EAM.DataAccess.CodeGenerator;
using YH.EAM.Entity.CodeGenerator;
using YH.EAM.WebApp.Attribute;

namespace YH.EAM.WebApp.Controllers
{
    [Authorize]
    public class UserRightController : TopControllerBase
    {

        [Right(PowerName = "主页面")]
        public IActionResult Index()
        {
            return View();
        }


        [Right(Ignore = true)]
        public IActionResult NoPermission() {

            return View();

        }

      

        [Right(PowerName = "功能配置")]
        public IActionResult Func()
        {
            return View();
        }

      




        [Right(PowerName = "查询功能")]
        [HttpPost]
        public IActionResult ListFunc()
        {
            DataAccess.CodeGenerator.Tright_Power_Da da = new DataAccess.CodeGenerator.Tright_Power_Da();

            return SuccessResultList(da.ListByOder());

        }


        [Right(PowerName = "添加功能")]
        [HttpPost]
        public IActionResult AddFunc(Tright_Power model)
        {
            if (string.IsNullOrEmpty(model.Powername))
            {
                return FailMessage("权限名不能为空！");
            }

           Tright_Power_Da da = new Tright_Power_Da();

            da.Insert(model);

            return SuccessMessage("成功！");
  
        }


        [Right(PowerName = "修改功能")]
        [HttpPost]
        public IActionResult UpdateFunc(Tright_Power model)
        {

            if (string.IsNullOrEmpty(model.Powername))
            {
                return FailMessage("权限名不能为空！");
            }

           Tright_Power_Da da = new Tright_Power_Da();
           da.Update(model);

           return SuccessMessage("成功！");
           
        }



        [Right(PowerName = "删除功能")]
        [HttpPost]
        public IActionResult DelFunc(int id)
        {
            Tright_Power_Da da = new Tright_Power_Da();

            if (da.Delete(s => s.Id == id) > 0)
            {
                return SuccessMessage();
              
            }
            return FailMessage();

        }






    }
}
