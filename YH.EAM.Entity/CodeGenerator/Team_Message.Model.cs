//----------------
//DA  v1.1
//2020-7-31
//Near
//---------------


using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using NPOI.SS.UserModel;
using WeihanLi.Npoi.Attributes;


namespace YH.EAM.DataAccess.CodeGenerator
{
    /// <summary>
    ///  资产列表
    ///</summary>
    public class   Team_Message
    {
       public Team_Message()
       {
      
       }

        ///<summary>
        ///描述：主键
        ///</summary>
        [FreeSql.DataAnnotations.Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }
        ///<summary>
        ///描述：使用人
        ///</summary>
        [Column("使用人")]
        public string User { get; set; }
        ///<summary>
        ///描述：工号
        ///</summary>
        [Column( "工号")]
        public string Workerid { get; set; }
        ///<summary>
        ///描述：一级部门
        ///</summary>
        [Column("一级部门")] 
        public string Dep1 { get; set; }
        ///<summary>
        ///描述：使用部门
        ///</summary>
        [Column("使用部门")]
        public string Dep2 { get; set; }
        ///<summary>
        ///描述：设备编号
        ///</summary>
        [Column("编号")]
        public string Equipment_Numbers { get; set; }
        ///<summary>
        ///描述：名称
        ///</summary>
        [Column("名称")]
        public string Name { get; set; }
        ///<summary>
        ///描述：状态
        ///</summary>
        [Column("状态")]
        public string Status { get; set; }
        ///<summary>
        ///描述：规格型号
        ///</summary>
        [Column("规格型号")]
        public string Size_Model { get; set; }
        ///<summary>
        ///描述：资产类型
        ///</summary>
        [Column("资产组")]
        public string Type { get; set; }
        ///<summary>
        ///描述：入库日期
        ///</summary>
        [Column("购置日期")]
        public DateTime Inbound_Date { get; set; }
        ///<summary>
        ///描述：财务编号
        ///</summary>
        [Column("财务编号")]
        public string Financial_Numbers { get; set; }
        ///<summary>
        ///描述：计算机名
        ///</summary>
        [Column("计算机名")]
        public string Computer_Name { get; set; }
        ///<summary>
        ///描述：物理地址
        ///</summary>
        [Column("物理地址")]
        public string Physical_Address { get; set; }
        ///<summary>
        ///描述：备注
        ///</summary>
        [Column("备注")]
        public string Remarks { get; set; }
        ///<summary>
        ///描述：位置
        ///</summary>
        [Column("位置")]
        public string Location { get; set; }
        ///<summary>
        ///描述：是否加域
        ///</summary>
        [Column("是否加域")]
        public string Add_Domain { get; set; }
        ///<summary>
        ///描述：外网权限
        ///</summary>
        [Column("外网权限")]
        public string Outside_Network { get; set; }
        ///<summary>
        ///描述：设备详情
        ///</summary>
        [Column("设备详情")]
        public string Equipment_Detailed { get; set; }       
        ///<summary>
        ///描述：创建时间
        ///</summary>
        [Column("创建时间")]
        public DateTime Createtime { get; set; }
    }
}








