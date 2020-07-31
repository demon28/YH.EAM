using FreeSql.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;


namespace YH.EAM.DataAccess.CodeGenerator
{
    /// <summary>
    ///  资产类型
    ///</summary>
    public class   TEAM_Type
    {

       public TEAM_Type()
       {
      
       }

        ///<summary>
        ///描述：主键
        ///</summary>
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }
        ///<summary>
        ///描述：资产类型
        ///</summary>
        public string Type { get; set; }
        ///<summary>
        ///描述：备注
        ///</summary>
        public string Remarks { get; set; }
        ///<summary>
        ///描述：创建时间
        ///</summary>
        public DateTime Createtime { get; set; }

    }
 }








