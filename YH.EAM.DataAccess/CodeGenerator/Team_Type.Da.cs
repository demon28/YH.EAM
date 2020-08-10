using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Victory.Core.Extensions;
using Victory.Core.Models;
using YH.EAM.DataAccess;
using YH.EAM.Entity.CodeGenerator;
using YH.EAM.Entity.Enums;
using YH.EAM.Entity.Model;
using YH.EAM.Entity.Tool;

namespace  YH.EAM.DataAccess.CodeGenerator
{

    /// <summary>
    ///   资产类型
    ///</summary>
    public class TEAM_Type_Da : FreeSql.BaseRepository<TEAM_Type>
    {

        public TEAM_Type_Da() : base(DataAccess.DbContext.Db, null, null)
        {

        }


        public List<TEAM_Type> ListByWhere(string keyword, ref PageModel page) 
        {
            var data =this.Select;
            List<TEAM_Type> list;
            if(!string.IsNullOrEmpty(keyword))
            {
                data= data.Where(s => s.Type.Contains(keyword) );
            }
            //如果没有分页信息
            if(page.PageIndex==0)
            { 
                list = data.OrderBy(s => s.Createtime)
                .ToList();
            }
            else
            {
                 page.TotalCount = data.Count().ToInt();
                 list = data.Page(page.PageIndex,page.PageSize)
                .OrderBy(s => s.Createtime)
                .ToList();
            }          
            return list;
        }
    }

}

