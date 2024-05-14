using ModelLibrary.DBObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary.workers
{
    internal class ViewWorker
    {
        private string ConnectionString;
        internal string Tag { get; set; }
        internal string Database { get; set; }
        public ViewWorker(string connectionString, string database, string tag)
        {
            this.ConnectionString = connectionString;
            this.Tag = tag;
            this.Database = database;
        }
        public async Task< IList<View> > worker()
        {
            MySql hsql = new MySql();
            hsql.ConnectionString = ConnectionString;
            hsql.ConnectToDB();
            hsql.UseDatabase(Database);

            string comando = $@"
            SELECT [schema] = OBJECT_SCHEMA_NAME(so.object_id)
                  ,so.name
                  ,comment = ISNULL((SELECT Value
                              FROM ::fn_listextendedproperty ('{this.Tag}', 'Schema', OBJECT_SCHEMA_NAME(so.object_id), 'VIEW', so.name, Null, Null)
                             ), '')
            FROM sys.views so
            ORDER BY OBJECT_SCHEMA_NAME(so.object_id), so.name
            ";
            IList<View> views = new List<View>();
            hsql.ExecuteSqlData(comando);
            if (hsql.ErrorExiste)
                return views;
            while(hsql.Data.Read())
            {
                View v = new View();
                v.Schema = hsql.Data[0].ToString();
                v.Name = hsql.Data[1].ToString();
                v.Comment = hsql.Data[2].ToString();
                views.Add(v);
            }
            return views;
        }

        internal async Task WorkerDetails(IList<View> views)
        {
            MySql hsql = new MySql();
            hsql.ConnectionString = ConnectionString;
            hsql.ConnectToDB();
            hsql.UseDatabase(Database);

            foreach (var view in views)
            {
                string comando = $@"
                select 
                   [Columna] = col.name,
                   [Tipo] = t.name + 
                            case when t.is_user_defined = 0 then 
                                        isnull('(' + 
                                        case when t.name in ('binary', 'char', 'nchar',
                                                'varchar', 'nvarchar', 'varbinary') then
                                                case col.max_length 
                                                    when -1 then 'MAX' 
                                                    else 
                                                            case 
                                                                when t.name in ('nchar', 
                                                                    'nvarchar') then
                                                                    cast(col.max_length/2 
                                                                    as varchar(4))
                                                                else cast(col.max_length 
                                                                    as varchar(4))
                                                            end
                                                end
                                            when t.name in ('datetime2', 
                                                'datetimeoffset', 'time') then 
                                                cast(col.scale as varchar(4))
                                            when t.name in ('decimal', 'numeric') then 
                                                cast(col.precision as varchar(4)) + ', ' +
                                                cast(col.scale as varchar(4))
                                        end + ')', '')        
                                else ':' +
                                        (select c_t.name + 
                                                isnull('(' + 
                                                case when c_t.name in ('binary', 'char',
                                                        'nchar', 'varchar', 'nvarchar',
                                                        'varbinary') then
                                                        case c.max_length
                                                            when -1 then 'MAX'
                                                            else case when t.name in
                                                                            ('nchar',
                                                                            'nvarchar')
                                                                        then cast(c.max_length/2
                                                                            as varchar(4))
                                                                        else cast(c.max_length
                                                                            as varchar(4))
                                                                    end
                                                        end
                                                    when c_t.name in ('datetime2', 
                                                        'datetimeoffset', 'time') then
                                                        cast(c.scale as varchar(4))
                                                    when c_t.name in ('decimal', 'numeric') then
                                                        cast(c.precision as varchar(4)) +
                                                        ', ' + cast(c.scale as varchar(4))
                                                end + ')', '')
                                        from sys.columns as c
                                                inner join sys.types as c_t 
                                                    on c.system_type_id = c_t.user_type_id
                                        where c.object_id = col.object_id
                                            and c.column_id = col.column_id
                                            and c.user_type_id = col.user_type_id
                                        ) 
                            end,
                    [Nullable] = case 
                                    when col.is_nullable = 0 then 'N'
                                    else 'Y'
                                 end,
                    [Comment] = ISNULL(ep.value, '')
                from sys.views as v
                    join sys.columns as col
                        on v.object_id = col.object_id
                    left join sys.types as t
                        on col.user_type_id = t.user_type_id
                    left join sys.extended_properties as ep 
                        on v.object_id = ep.major_id
                        and col.column_id = ep.minor_id
                        and ep.name = '{this.Tag}'        
                        and ep.class_desc = 'OBJECT_OR_COLUMN'
                where v.schema_id = SCHEMA_ID('{view.Schema}')
                  And v.name = '{view.Name}'
                order by col.column_id;";

                hsql.ExecuteSqlData(comando);
                if (hsql.ErrorExiste)
                {
                    hsql.ErrorClear();
                    continue;
                }
                while (hsql.Data.Read())
                {
                    var vd = new ViewDetails();
                    vd.Column = hsql.Data[0].ToString();
                    vd.Type = hsql.Data[1].ToString();
                    vd.Nullable = hsql.Data[2].ToString();
                    vd.Comment = hsql.Data[3].ToString();
                    view.ViewDetails.Add(vd);
                }

            }
        }
    }
}
