﻿using FreeSql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using YH.EAM.Entity.Tool;

namespace YH.EAM.DataAccess
{
    public static class DbContext
    {
        public static Dictionary<string, IFreeSql> ConnectionPool = new Dictionary<string, IFreeSql>();

        public static IFreeSql Db()
        {
            DataType t = DataType.SqlServer;
            return SelectDBType(t);
        }


        private static IFreeSql SelectDBType(DataType enum_dbtype)
        {
            var dbtype = enum_dbtype.ToString();
            if (!ConnectionPool.ContainsKey(dbtype))
            {
                var freesql = new FreeSql.FreeSqlBuilder()
                     .UseConnectionString(enum_dbtype, AppConfig.ConnectionString())
                     .UseAutoSyncStructure(false)   //是否根据实体修改数据库， Code-First
                     .UseMonitorCommand(
                        cmd =>
                        {
                            Trace.WriteLine(DateTime.Now.ToString(""));
                            Trace.WriteLine(cmd.CommandText);
                        }, //监听SQL命令对象，在执行前
                        (cmd, traceLog) =>
                        {
                            Console.WriteLine(traceLog);
                        }) //监听SQL命令对象，在执行后
                    .UseLazyLoading(true)
                    .Build();

              

                ConnectionPool.Add(dbtype, freesql);
            }
            return ConnectionPool[dbtype];
        }

     


    }
}