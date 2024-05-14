using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary.DBObjects;
using ModelLibrary;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ModelLibrary.workers
{
    internal class ProcWorker
    {
        private string connectionString;
        internal string Tag {  get; set; }
        internal string Database { get; set; }
        public ProcWorker(string connectionString, string database, string tag) 
        {
            this.connectionString = connectionString;
            this.Tag = tag;
            this.Database = database;
        }
        internal async Task< IList<Proc> > Worker()
        {
            MySql hsql = new()
            {
                ConnectionString = connectionString
            };
            hsql.ConnectToDB();
            hsql.UseDatabase(Database);

            string comando = $@"
            SELECT [schema] = OBJECT_SCHEMA_NAME(so.object_id)
                  ,so.name
                  ,comment = ISNULL((SELECT Value
                              FROM ::fn_listextendedproperty ('{this.Tag}', 'Schema', OBJECT_SCHEMA_NAME(so.object_id), 'PROCEDURE', so.name, Null, Null)
                             ), '')
            FROM sys.procedures so
            ORDER BY OBJECT_SCHEMA_NAME(so.object_id), so.name
            ";
            hsql.ExecuteSqlData(comando);

            IList < Proc > procs = new List < Proc >();
            if (hsql.ErrorExiste)
                return procs;

            while(hsql.Data.Read())
            {
                Proc p = new();
                p.Schema = hsql.Data[0].ToString();
                p.Name = hsql.Data.GetString(1);
                p.Comment = hsql.Data.GetString(2);
                procs.Add (p);
            }
            return procs;
        }
    }
}
