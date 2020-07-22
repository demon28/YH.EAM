using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Victory.Core.Controller;

namespace YH.EAM.WebApp.Controllers.Asset
{

    public class AssetClassifyController : TopControllerBase
    {
        // GET: AssetClassifyController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AssetClassifyController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AssetClassifyController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AssetClassifyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AssetClassifyController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AssetClassifyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AssetClassifyController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AssetClassifyController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
