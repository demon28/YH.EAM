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
    public class AssetBorrowController:TopControllerBase
    {        
        [Right(PowerName = "借用列表")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
