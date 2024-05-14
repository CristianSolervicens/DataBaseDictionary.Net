using ModelLibrary.DBObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary.workers
{
    internal class TableFuncWorker
    {
        private string ConnectionString;
        internal string Tag { get; set; }
        internal string Database { get; set; }
        public TableFuncWorker(string connectionString, string database, string tag)
        {
            this.ConnectionString = connectionString;
            this.Tag = tag;
            this.Database = database;
        }
        public async Task< IList<TableFunction> > worker()
        {
            MySql hsql = new MySql();
            hsql.ConnectionString = ConnectionString;
            hsql.ConnectToDB();
            hsql.UseDatabase(Database);

            string comando = $@"
            SELECT [schema] = OBJECT_SCHEMA_NAME(so.object_id)
                    ,so.name
                    ,comment = ISNULL((SELECT Value
                                FROM ::fn_listextendedproperty ('{this.Tag}', 'Schema', OBJECT_SCHEMA_NAME(so.object_id), 'FUNCTION', so.name, Null, Null)
                                ), '')
            FROM sys.objects so
            WHERE so.type = 'TF'
            ORDER BY OBJECT_SCHEMA_NAME(so.object_id), so.name
            ";
            IList<TableFunction> functions = new List<TableFunction>();
            hsql.ExecuteSqlData(comando);
            if (hsql.ErrorExiste)
                return functions;
            while(hsql.Data.Read())
            {
                var f = new TableFunction();
                f.Name = hsql.Data[0].ToString();
                f.Name = hsql.Data[1].ToString();
                f.Comment = hsql.Data[2].ToString();
                functions.Add(f);
            }
            return functions;
        }
    }
}
