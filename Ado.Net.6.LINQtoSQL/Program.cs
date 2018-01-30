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
            Exmpl08();



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
                foreach (var item2 in item)
                {
                    Console.WriteLine(item2.intUserId + " - " + item2.StrTabName);
                }
            }
            Console.WriteLine("------------------------------");
            db.Log = Console.Out;
            Console.WriteLine("------------------------------");

            MetaModel mm = db.Mapping;



            foreach (var user in users)
            {
                //пересылка данных туда и обратно
                foreach (var tab in user.AccessTabs)
                {
                    Console.WriteLine(user.intUserId + " - " + tab.StrTabName);
                }
            }

        }

        static void Exmpl04()
        {
            Console.WriteLine("connection: {0}", db.Connection);
            Console.WriteLine("connection: {0}", db.Connection.ConnectionString);
            Console.WriteLine("connection: {0}", db.Connection.ConnectionTimeout);
            Console.WriteLine("connection: {0}", db.Connection.Database);
            Console.WriteLine("connection: {0}", db.Connection.DataSource);
        }

        static void Exmpl05(MCSModel dataContext)
        {
            Table<AccessTab> tabs = dataContext.GetTable<AccessTab>();
            Table<AccessUser> users = dataContext.GetTable<AccessUser>();
            db.Refresh(RefreshMode.OverwriteCurrentValues);
            AccessTab a = tabs.OrderBy(o => o.StrTabName).First(f => f.intTabId == 56);
        }


        static void Exmpl08()
        {
            Table<AccessTab> accessTabs = db.GetTable<AccessTab>();
            AccessTab aTab = accessTabs.FirstOrDefault(f => f.intTabId == 1);
            aTab.StrDescription = "Test 005";

            Table<AccessUser> accessUsers = db.GetTable<AccessUser>();
            AccessUser aUser = accessUsers.FirstOrDefault(f => f.intAccessId == 6822);
            aUser.dCreated = DateTime.Now;
            aUser.intTabId = 108;

            try
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    db.SubmitChanges();
                    scope.Complete();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                db.Refresh(RefreshMode.OverwriteCurrentValues, accessTabs);
                Console.WriteLine("StrDescription: {0}", aTab.StrDescription);

                db.Refresh(RefreshMode.OverwriteCurrentValues, accessUsers);
                Console.WriteLine("dCreated: {0}", aUser.dCreated);
            }
        }
    }
}
