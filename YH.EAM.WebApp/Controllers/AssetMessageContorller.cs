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
using Microsoft.AspNetCore.Mvc;
using Victory.Core.Controller;
using Victory.Core.Extensions;
using Victory.Core.Models;
using WeihanLi.Npoi;
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

         private List<Team_Message> list = new List<Team_Message>();

        [Right(PowerName = "查询")]
        [HttpPost]
        public IActionResult List(string keyword,int pageIndex,int pageSize)
        {

            PageModel page = new PageModel();
            page.PageIndex = pageIndex;
            page.PageSize = pageSize;


            Team_Message_Da da = new  Team_Message_Da();
            
            list = da.ListByWhere(keyword, ref page);  
            if(page.PageIndex==0)
            { 
                return SuccessResultList(list);
            }
            return SuccessResultList(list,page);
        }




        [Right(PowerName = "添加")]
        [HttpPost]
        public IActionResult Add(Team_Message model)
        {
            model.Createtime=DateTime.Now.ToLocalTime().ToDateTime();
            model.Inbound_Date=DateTime.Now.ToShortDateString().ToDateTime();
            
            var da = new Team_Message_Da();
            da.Insert(model);
            return SuccessMessage("添加成功！");

        }


        [Right(PowerName = "修改")]
        [HttpPost]
        public IActionResult Update(Team_Message model)
        {
            model.Createtime=DateTime.Now.ToLocalTime().ToDateTime();
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
        public IActionResult Input()
        {
            try
            { 
              
                const string excelPath = @"C:\Users\10650\Desktop\资产管理20200810113909.xls";
                var dataTableT = ExcelHelper.ToDataTable<Team_Message>(excelPath);
                //将datatable数据源转化为实体类数据
                var adapter= ConvertToModel(dataTableT);

                //将实体类数据插入数据库；之前要进行查重.将可以导入的数据存为新的实体类
                var data1=new List<Team_Message>();
            
                var da = new Team_Message_Da();
                List(null,0,0); 
            
                foreach(var item in adapter)
                { 
                    data1.Add(item);
                    foreach(var item1 in list)
                    { 
                        if((item.Equipment_Numbers==item1.Equipment_Numbers)||(item.Id==item1.Id)) 
                        { 
                            data1.Remove(item);
                            break;
                        } 
                    }                                                  
                }       
                if(data1.Count!=0)
                { 
                    foreach(var item2 in data1)
                    { 
                        da.Insert(item2);
                    }           
                    return SuccessMessage("成功导入"+data1.Count+"条数据！");
                }
                else
                { 
                    return SuccessMessage("导入0条数据！");
                }
            }
            catch(Exception e)
            { 
                return FailMessage(e.Message);
            }
            
        }

        [Right(PowerName = "导出")]
        [HttpPost]
        public IActionResult Output()
        {
            List(null, 0, 0);
            
            string folder = DateTime.Now.ToString("yyyyMMddhhmmss");  
            var excelPath= System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "\\" +"资产管理"+ folder + ".xls";
            //const string excelPath = @"C:\Users\10650\Desktop\aaa11.xls";
            list.ToExcelFile(excelPath);
            var entityList=ExcelHelper.ToEntityList<Team_Message>(excelPath);
            if (entityList.Count == 0)
            {
                return FailMessage("失败！");
            }
            return SuccessMessage("成功！");
        }   
        
        /// <summary>
        /// 将DataTable数据源转换成实体类
        /// </summary>
        public static List<Team_Message> ConvertToModel(DataTable dt)
        {
            List<Team_Message> ts = new List<Team_Message>();
            foreach (DataRow dr in dt.Rows)
            {
                Team_Message t = new Team_Message();
                PropertyInfo[] propertys = t.GetType().GetProperties();// 获得此模型的公共属性
                foreach (PropertyInfo pi in propertys)
                {
                    if (dt.Columns.Contains(pi.Name))
                    {
                        if (!pi.CanWrite) continue;
                        var value = dr[pi.Name];
                        if (value != DBNull.Value)
                        {
                            switch (pi.PropertyType.FullName)
                            {
                                case "System.Decimal":
                                    pi.SetValue(t, decimal.Parse(value.ToString()), null);
                                    break;
                                case "System.String":
                                    pi.SetValue(t, value.ToString(), null);
                                    break;
                                case "System.Int32":
                                    pi.SetValue(t, int.Parse(value.ToString()), null);
                                    break;
                                default:
                                    pi.SetValue(t, value, null);
                                    break;
                            }
                        }
                    }
                }
                ts.Add(t);
            }
            return ts;
        }
    }
}