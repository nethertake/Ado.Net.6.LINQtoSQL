using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;

namespace Ado.Net._6.LINQtoSQL.Model
{
    class MCSModel: DataContext
    {
        public MCSModel(): base("Data Source = 192.168.111.107; Initial Catalog=MCS; User ID = sa; Password = Mc123456;")
        {
           
        }
        public Table<AccessTab> AccessTab { get; set; }



    }
}
