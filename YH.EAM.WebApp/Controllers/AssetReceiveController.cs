using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Victory.Core.Controller;
using YH.EAM.WebApp.Attribute;

namespace YH.EAM.WebApp.Controllers
{
    [Authorize]
    public class AssetReceiveController:TopControllerBase
    {
        [Right(PowerName = "领用界面")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
