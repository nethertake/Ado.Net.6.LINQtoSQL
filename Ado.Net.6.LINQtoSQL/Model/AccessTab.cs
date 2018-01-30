using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ado.Net._6.LINQtoSQL.Model
{
    [Table(Name="AccessTab")]
   public class AccessTab
    { 
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int intTabId { get; set; }
        [Column(Name="strTabName")]
        public string StrTabName { get; set; }
        [Column(Name="strDescription")]
        public string StrDescription { get; set; }
        [Column(Name="strTabUrl")]
        public string StrTabUrl { get; set; }

        private string StrTabGroupName;
        [Column(Storage="StrTabGroupName")]
        public string strTabGroupName
        {
            get { return StrTabGroupName; }
            set { StrTabGroupName = value; }
        }

        [Association(ThisKey = "intTabId", OtherKey = "intTabId")]
        public EntitySet<AccessUser> AccessUsers { get; set; }
    }
}
