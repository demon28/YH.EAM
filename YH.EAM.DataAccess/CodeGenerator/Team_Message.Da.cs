//DA  v1.1
//2020-7-31
//Near


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
    ///   资产列表
    ///</summary>
    public class Team_Message_Da : FreeSql.BaseRepository<Team_Message>
    {

        public Team_Message_Da() : base(DataAccess.DbContext.Db, null, null)
        {

        }


        public List<Team_Message> ListByWhere(string keyword, ref PageModel page) {

            var data =this.Select;

            if(!string.IsNullOrEmpty(keyword))
            {
                data= data.Where(s => s.User.Contains(keyword)||s.Workerid.Contains(keyword)||s.Dep1.Contains(keyword)||s.Dep2.Contains(keyword)||s.Equipment_Numbers.Contains(keyword)|| s.Status.Contains(keyword)||s.Name.Contains(keyword)||s.Computer_Name.Contains(keyword));
            }

            page.TotalCount = data.Count().ToInt();
          

            var list = data.Page(page.PageIndex, page.PageSize)
                .OrderBy(s => s.Createtime)
                .ToList();

            return list;
        }


    }

}

