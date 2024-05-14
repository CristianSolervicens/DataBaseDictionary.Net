using ModelLibrary.DBObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary.workers
{
    internal class ScalarFuncWorker
    {
        private string connectionString;
        internal string Tag { get; set; }
        internal string Database { get; set; }
        public ScalarFuncWorker(string connectionString, string database, string tag)
        {
            this.connectionString = connectionString;
            this.Tag = tag;
            this.Database = database;
        }


        internal async Task< IList<ScalarFunction> > Worker()
        {
            MySql hsql = new();
            hsql.ConnectionString = connectionString;
            hsql.ConnectToDB();
            hsql.UseDatabase(Database);

            string comando = $@"
            SELECT [schema] = OBJECT_SCHEMA_NAME(so.object_id)
                  ,so.name
                  ,comment = ISNULL((SELECT Value
                              FROM ::fn_listextendedproperty ('{this.Tag}', 'Schema', OBJECT_SCHEMA_NAME(so.object_id), 'FUNCTION', so.name, Null, Null)
                             ), '')
            FROM sys.objects  so
            WHERE so.type = 'FN'
            ORDER BY OBJECT_SCHEMA_NAME(so.object_id), so.name
            ";
            List<ScalarFunction> scalarFunctions = new List<ScalarFunction>();
            hsql.ExecuteSqlData(comando);
            if (hsql.ErrorExiste)
                return scalarFunctions;

            while(hsql.Data.Read())
            {
                ScalarFunction f = new ScalarFunction();
                f.Schema = hsql.Data[0].ToString();
                f.Name = hsql.Data[1].ToString();
                f.Comment = hsql.Data[2].ToString();
                scalarFunctions.Add(f);
            }
            return scalarFunctions;
        }
    }
}
