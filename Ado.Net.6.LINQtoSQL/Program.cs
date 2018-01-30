using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using Ado.Net._6.LINQtoSQL.Model;
using System.Data.Linq.Mapping;

namespace Ado.Net._6.LINQtoSQL
{
    class Program
    {
        static private Model.MCSModel db = new Model.MCSModel();



        static void Main(string[] args)
        {
            Exmpl03();
        }

        static void Exmpl01()
        {
            try
            {
                db.CommandTimeout = 30;
                Table<AccessTab> accessTables = db.GetTable<AccessTab>();
                AccessTab tab = accessTables.FirstOrDefault(f => f.intTabId == 56);
                tab.StrDescription = "some descr";
                db.SubmitChanges(ConflictMode.FailOnFirstConflict);


                //foreach (AccessTab tab in accessTables)
                //{
                //    Console.WriteLine("Tab name: " + tab.StrTabName);
                //}

            }
            catch (ChangeConflictException ex)
            {
                Console.WriteLine(ex.Message);
                foreach (ObjectChangeConflict item in db.ChangeConflicts)
                {
                    MetaTable metatable = db.Mapping.GetTable(item.Object.GetType());
                    Model.AccessTab en = (Model.AccessTab)item.Object;
                    Console.WriteLine("Table name{0}:", metatable.TableName);
                }
            }
        }

        static void Exmpl03()
        {
            Table<AccessUser> users = db.GetTable<AccessUser>();
            var query = from u in users
                        where u.intUserId == 1
                        select
from t in u.AccessTabs
//anonim method
                        select new { u.intUserId, t.StrTabName };
            foreach (var item in query)
            {
                foreach(var item2 in item)
                {
                    Console.WriteLine(item2.intUserId + " - " + item2.StrTabName);
                }
            }

        }
    }
}
