using System;
using System.Collections.Generic;
using System.Text;

namespace YH.EAM.Entity.Tool
{
    public class AppConfig
    {


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


    }
}
