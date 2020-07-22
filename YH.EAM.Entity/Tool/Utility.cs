using System;
using System.Collections.Generic;
using System.Text;

namespace YH.EAM.Entity.Tool
{
    public static   class Utility
    {
        public static int PageTotal(int TotalPage,int PageSize)
        {

            return TotalPage % PageSize == 0 ? TotalPage / PageSize : TotalPage / PageSize + 1;
        }


    }
}
