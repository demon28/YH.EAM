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

namespace YH.EAM.DataAccess.CodeGenerator
{

    /// <summary>
    ///  
    ///</summary>
    public class Team_User_Da : FreeSql.BaseRepository<Team_User>
    {

        public Team_User_Da() : base(DataAccess.DbContext.Db, null, null)
        {

        }


        public List<Team_User> ListByWhere(string keyword, ref PageModel page) 
        {

            var data =this.Select;
            List<Team_User> list;
            if(!string.IsNullOrEmpty(keyword))
            {
                data= data.Where(s => s.Name.Contains(keyword) || s.Workid.Contains(keyword) );
            }
            if (page.PageIndex == 0)
            {
                 list = data.OrderBy(s => s.Comedate)
                .ToList();

            }
            else
            { 
                page.TotalCount = data.Count().ToInt();
                list = data.Page(page.PageIndex, page.PageSize)
                .OrderBy(s => s.Comedate)
                .ToList();
            }
           
            return list;
        }

    }

}

