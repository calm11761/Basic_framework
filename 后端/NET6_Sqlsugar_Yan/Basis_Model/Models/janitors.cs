using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basis_Model.Models
{
    [SugarTable("janitors", "信息表")]
    public class janitors
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnDescription = "主键")]
        public int Id{ get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 50, ColumnDescription = "账号")]
        public string JanAccounts { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 50, ColumnDescription = "权限名称")]
        public string  Competence { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 50, ColumnDescription = "创建时间")]
        public DateTime CreateTime { get; set; }
    }
}
