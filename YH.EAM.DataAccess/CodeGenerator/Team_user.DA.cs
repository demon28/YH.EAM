using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Victory.Core.Models;
using YH.EAM.DataAccess;
using YH.EAM.Entity.CodeGenerator;
using YH.EAM.Entity.Enums;
using YH.EAM.Entity.Model;

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




    }

}

