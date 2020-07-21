using System;
using System.Collections.Generic;
using System.Text;
using Victory.Core.Extensions;
using YH.EAM.Entity.Model;

namespace YH.EAM.Entity.Tool
{
    public class AppConfig
    {

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        /// <returns></returns>
        public static string ConnectionString() {
            Victory.Core.Helpers.ConfigHelper configHelper = new Victory.Core.Helpers.ConfigHelper("appsettings.json");

            var dbmodel = configHelper.Get<Model.DbModel>("ConnectionStrings");

            if (dbmodel.DB== "ProductDatabase")
            {
                return dbmodel.ProductDatabase;
            }

            if (dbmodel.DB == "EnvironDatabase")
            {
                return dbmodel.EnvironDatabase;
            }

            if (dbmodel.DB == "DevelopDatabase")
            {
                return dbmodel.DevelopDatabase;
            }

            return dbmodel.DevelopDatabase;
        }



        public static JwtModel Jwt
        {

            get
            {
                Victory.Core.Helpers.ConfigHelper configHelper = new Victory.Core.Helpers.ConfigHelper("appsettings.json");

                JwtModel model = configHelper.Get<Model.JwtModel>("Jwt");

                return model;

            }


        }





        /// <summary>
        /// 是否忽略权限验证
        /// </summary>
        public static bool IgnoreAuthRight
        {
            get
            {
                Victory.Core.Helpers.ConfigHelper configHelper = new Victory.Core.Helpers.ConfigHelper("appsettings.json");

                var key = configHelper.GetSingle("IgnoreAuthRight");

                if (string.IsNullOrEmpty(key))
                {
                    return false;
                }

                return key.ToBool();
            }

        }

        /// <summary>
        /// 是否忽略Api签名验证
        /// </summary>
        public static bool IgnoreApiFilter
        {
            get
            {
                Victory.Core.Helpers.ConfigHelper configHelper = new Victory.Core.Helpers.ConfigHelper("appsettings.json");

                var key = configHelper.GetSingle("IgnoreApiFilter");

                if (string.IsNullOrEmpty(key))
                {
                    return false;
                }

                return key.ToBool();
            }

        }


    }
}
