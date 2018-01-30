using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ado.Net._6.LINQtoSQL.Model
{
   [Table(Name = "AccessUser")]
    public class AccessUser
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true )]
        public int intAccessId { get; set; }
        [Column]
        public int intUserId { get; set; }
        [Column]
        public DateTime dCreated { get; set; }
        [Column]
        public int intTabId { get; set; }

        //связь между таблицами
        [Association(ThisKey="intTabId", OtherKey = "intTabId")]
        public EntitySet<AccessTab> AccessTabs { get; set; }
    }
}
