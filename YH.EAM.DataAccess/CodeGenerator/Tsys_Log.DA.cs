using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using YH.EAM.DataAccess;
using YH.EAM.Entity.CodeGenerator;


namespace YH.EAM.DataAccess.CodeGenerator
{

    /// <summary>
    ///  系统日志表
    ///</summary>
    public class Tsys_Log_Da : FreeSql.BaseRepository<Tsys_Log>
    {
        public Tsys_Log_Da() : base(DbContext.Db, null, null)
        {


        }


    }

}

