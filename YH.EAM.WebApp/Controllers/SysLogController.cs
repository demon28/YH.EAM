using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace YH.EAM.WebApp.Controllers
{
    [Authorize]
    public class SysLogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult List() {

            return View();

        }

    }
}
