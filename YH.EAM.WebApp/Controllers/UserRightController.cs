using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace YH.EAM.WebApp.Controllers
{
    public class UserRightController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult NoPermission() {

            return View();

        }



       
        public IActionResult Role()
        {
            return View();
        }

        public IActionResult Func()
        {
            return View();
        }
    }
}
