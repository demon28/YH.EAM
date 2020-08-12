//控制器模板 v1.1
//2020-7-31
//Near

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Victory.Core.Controller;
using Victory.Core.Extensions;
using Victory.Core.Models;
using WeihanLi.Npoi;
using YH.EAM.Entity.CodeGenerator;
using YH.EAM.WebApp.Attribute;
using YH.EAM.WebApp.Controllers;

namespace YH.EAM.DataAccess.CodeGenerator
{

    [Authorize]
    public class AssetMessageController : TopControllerBase
    {
        IWebHostEnvironment _webHostEnvironment;

        public AssetMessageController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;

        }

        [Right(PowerName = "资产列表")]
        public IActionResult Index()
        {
            return View();
        }

        private List<Team_Message> list = new List<Team_Message>();

        [Right(PowerName = "查询")]
        [HttpPost]
        public IActionResult List(string keyword, int pageIndex, int pageSize)
        {

            PageModel page = new PageModel();
            page.PageIndex = pageIndex;
            page.PageSize = pageSize;


            Team_Message_Da da = new Team_Message_Da();

            list = da.ListByWhere(keyword, ref page);
            if (page.PageIndex == 0)
            {
                return SuccessResultList(list);
            }
            return SuccessResultList(list, page);
        }




        [Right(PowerName = "添加")]
        [HttpPost]
        public IActionResult Add(Team_Message model)
        {
            model.Createtime = DateTime.Now.ToLocalTime().ToDateTime();
            model.Inbound_Date = DateTime.Now.ToShortDateString().ToDateTime();

            var da = new Team_Message_Da();
            da.Insert(model);
            return SuccessMessage("添加成功！");

        }


        [Right(PowerName = "修改")]
        [HttpPost]
        public IActionResult Update(Team_Message model)
        {
            model.Createtime = DateTime.Now.ToLocalTime().ToDateTime();
            var da = new Team_Message_Da();
            da.Update(model);
            return SuccessMessage("成功！");
        }


        [Right(PowerName = "删除")]
        [HttpPost]
        public IActionResult Del(int id)
        {
            var da = new Team_Message_Da();

            return da.Delete(s => s.Id == id) > 0 ? SuccessMessage("已删除！") : FailMessage();
        }


        [Right(PowerName = "导入")]
        [HttpPost]
        [Obsolete]
        public IActionResult Upload(IFormCollection formCollection)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            //拿到用户id
            var userinfo = (HttpContext.User.Identity as System.Security.Claims.ClaimsIdentity);
            int userId = int.Parse(userinfo.FindFirst("userId").Value);

            //FormCollection转化为FormFileCollection
            var files = (FormFileCollection)formCollection.Files;

            //单文件上传直接获取文件
            //  Request.Form.Files[0]

            if (files.Count <= 0)
            {
                return FailMessage("上传失败，未检测上传的文件信息");
            }

            //确定文件上传路径
            var filePath = "/FileUpload/Exort/" + userId + "/";

            if (!Directory.Exists(webRootPath + filePath))
            {
                Directory.CreateDirectory(webRootPath + filePath);
            }

            foreach (var formFile in files)
            {
                //文件后缀
                var fileExtension = Path.GetExtension(formFile.FileName);//获取文件格式，拓展名

                //保存的文件名称(以名称和保存时间命名)
                var saveName = formFile.FileName.Substring(0, formFile.FileName.LastIndexOf('.')) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + fileExtension;

                //文件保存  TODO:未加密
                using (var fs = System.IO.File.Create(webRootPath + filePath + saveName))
                {
                    formFile.CopyTo(fs);
                    fs.Flush();
                }

                //完整的文件路径
                var completeFilePath = Path.Combine(filePath, saveName);

                if (!Import(webRootPath + completeFilePath))
                {
                    return SuccessMessage("导入失败！");
                }
            }

            return SuccessMessage("导入成功");
        }

        [Right(PowerName = "导出")]
        [HttpPost]
        [Obsolete]
        public IActionResult Output()
        {
            List(null, 0, 0);

            string folder = DateTime.Now.ToString("yyyyMMddhhmmss");
            var excelPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "\\" + "资产管理" + folder + ".xls";

            var setting = ExcelHelper.SettingFor<Team_Message>();           
            setting.HasSheetConfiguration(0, "系统设置列表",1,true);

            setting.HasFilter(0, 1)
                .HasFreezePane(0, 1, 2, 1);
            setting.Property(_ => _.User)
                .HasColumnTitle("使用人")
                .HasColumnIndex(0);

            setting.Property(_ => _.Workerid)
                .HasColumnTitle("工号")
                .HasColumnIndex(1);

            setting.Property(_ => _.Dep1)
                .HasColumnTitle("一级部门")
                .HasColumnIndex(2);

            setting.Property(_ => _.Dep2)
                .HasColumnTitle("使用部门")
                .HasColumnIndex(3);

            setting.Property(_ => _.Equipment_Numbers)
                .HasColumnTitle("编号")
                .HasColumnIndex(4);

            setting.Property(_ => _.Name)
                .HasColumnIndex(5)
                .HasColumnTitle("名称");

            setting.Property(_ => _.Status)
                .HasColumnTitle("状态")
                .HasColumnIndex(6);

            setting.Property(_ => _.Size_Model)
                .HasColumnTitle("规格型号")
                .HasColumnIndex(7);

            setting.Property(_ => _.Type)
                .HasColumnTitle("资产组")
                .HasColumnIndex(8);

            setting.Property(_ => _.Inbound_Date)
                .HasColumnTitle("购置日期")
                .HasColumnIndex(9);

            setting.Property(_ => _.Financial_Numbers)
                .HasColumnTitle("财务编号")
                .HasColumnIndex(10);

            setting.Property(_ => _.Computer_Name)
                .HasColumnIndex(11)
                .HasColumnTitle("计算机名");

            setting.Property(_ => _.Physical_Address)
               .HasColumnTitle("物理地址")
               .HasColumnIndex(12);

            setting.Property(_ => _.Location)
                .HasColumnTitle("位置")
                .HasColumnIndex(14);

            setting.Property(_ => _.Add_Domain)
                .HasColumnTitle("是否加域")
                .HasColumnIndex(15);

            setting.Property(_ => _.Outside_Network)
                .HasColumnTitle("外网权限")
                .HasColumnIndex(16);

            setting.Property(_ => _.Equipment_Detailed)
                .HasColumnTitle("设备详情")
                .HasColumnIndex(17)
                .HasColumnFormatter("yyyy-MM-dd HH:mm:ss");

            setting.Property(_ => _.Remarks)
                .HasColumnIndex(13)
                .HasColumnTitle("备注");

            setting.Property(_ => _.Createtime)
                .HasColumnTitle("更新时间")
                .HasColumnIndex(18)
                .HasColumnFormatter("yyyy-MM-dd HH:mm:ss");
            setting.Property(_ => _.Id).Ignored();
                
            list.ToExcelFile(excelPath);
            var entityList = ExcelHelper.ToEntityList<Team_Message>(excelPath);

            if (entityList.Count == 0)
            {
                return FailMessage("失败！");
            }
            return SuccessMessage("成功！");
        }

        /// <summary>
        /// 排重并存入数据库
        /// </summary>
        /// <param name="excelPath"></param>
        /// <returns></returns>
        [Obsolete]
        private bool Import(string excelPath)
        {
            try
            {               
                var dataTableT = ExcelHelper.ToEntityList<Team_Message>(excelPath);                              

                //将实体类数据插入数据库；之前要进行查重.将可以导入的数据存为新的实体类
                var data1 = new List<Team_Message>();

                var da = new Team_Message_Da();
                List(null, 0, 0);                

                foreach (var item in dataTableT)
                {
                    data1.Add(item);
                    foreach (var item1 in list)
                    {
                        if ((item.Equipment_Numbers == item1.Equipment_Numbers) || (item.Id == item1.Id))
                        {
                            data1.Remove(item);
                            break;
                        }
                    }
                }
                if (data1.Count != 0)
                {                     
                    foreach (var item2 in data1)
                    {                       
                        item2.Createtime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd").ToDateTime();
                        item2.Inbound_Date=item2.Inbound_Date.ToString("yyyy-MM-dd").ToDateTime();
                        da.Insert(item2);
                    }

                    return true;
                }
                else
                {
                  return false;
                }
            }
            catch (Exception e)
            {
                 return false;
            }

        }       
    }
}