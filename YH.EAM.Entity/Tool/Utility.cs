using System;
using System.Collections.Generic;
using System.Text;

namespace YH.EAM.Entity.Tool
{
    public static   class Utility
    {
        public static int PageTotal(int TotalCount,int PageSize)
        {

            return TotalCount % PageSize == 0 ? TotalCount / PageSize : TotalCount / PageSize + 1;
        }


    }
}
