using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace YH.EAM.WebApp.Controllers
{
    public class ConsumableMaintainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
