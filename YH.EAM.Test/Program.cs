using System;
using YH.EAM.Entity.Model;

namespace YH.EAM.Test
{
    class Program
    {
        static void Main(string[] args)
        {

            DataAccess.CodeGenerator.Team_user_Da da = new DataAccess.CodeGenerator.Team_user_Da();

           var model= da.GetByOne(s => s.Name == "文天成");

            Console.WriteLine(model.Phone);
        }
    }
}
