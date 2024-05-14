using ModelLibrary.DBObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary.workers
{
    internal class TableWorker
    {
        private string ConnectionString;
        internal string Tag { get; set; }
        internal string Database { get; set; }
        public TableWorker(string connectionString, string database, string tag)
        {
            this.ConnectionString = connectionString;
            this.Tag = tag;
            this.Database = database;
        }
        internal async Task< IList<Table>> worker()
        {
            MySql hsql = new MySql();
            hsql.ConnectionString = ConnectionString;
            hsql.ConnectToDB();
            hsql.UseDatabase(Database);

            string comando = $@"
                SELECT [schema] = OBJECT_SCHEMA_NAME(so.object_id)
                      ,so.name
                      ,comment = ISNULL((SELECT Value
                                  FROM ::fn_listextendedproperty ('{this.Tag}', 'Schema', OBJECT_SCHEMA_NAME(so.object_id), 'TABLE', so.name, Null, Null)
                                 ), '')
                FROM sys.tables  so
                ORDER BY OBJECT_SCHEMA_NAME(so.object_id), so.name
            ";

            IList<Table> tables = new List<Table>();
            hsql.ExecuteSqlData(comando);
            if (hsql.ErrorExiste)
                return tables;

            while (hsql.Data.Read())
            {
                Table t = new Table();
                t.Schema = hsql.Data[0].ToString();
                t.Name = hsql.Data[1].ToString();
                t.Comment = hsql.Data[2].ToString();
                tables.Add(t);
            }

            return tables;
        }

        internal async Task WorkerDetails(IList<Table> tablas)
        {
            MySql hsql = new MySql();
            hsql.ConnectionString = ConnectionString;
            hsql.ConnectToDB();
            hsql.UseDatabase(Database);

            foreach(var table in tablas)
            {
                string comando = $@"
                select 
                    --[Schema] = schema_name(tab.schema_id),
                    --[Table] = tab.name, 
                    [Column] = col.name, 
                    --t.name as data_type,    
                    [Tipo] =  t.name + 
                        case when t.is_user_defined = 0 then 
                                isnull('(' + 
                                case when t.name in ('binary', 'char', 'nchar', 
                                            'varchar', 'nvarchar', 'varbinary') then
                                            case col.max_length 
                                                when -1 then 'MAX' 
                                                else 
                                                    case when t.name in ('nchar', 
                                                                'nvarchar') then
                                                                cast(col.max_length/2 
                                                                as varchar(4)) 
                                                            else cast(col.max_length 
                                                                as varchar(4)) 
                                                    end
                                            end
                                        when t.name in ('datetime2', 'datetimeoffset', 
                                            'time') then 
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
                                                        else   
                                                                case when t.name in 
                                                                        ('nchar', 
                                                                        'nvarchar') then 
                                                                        cast(c.max_length/2
                                                                        as varchar(4))
                                                                    else cast(c.max_length
                                                                        as varchar(4))
                                                                end
                                                    end
                                                when c_t.name in ('datetime2', 
                                                    'datetimeoffset', 'time') then 
                                                    cast(c.scale as varchar(4))
                                                when c_t.name in ('decimal', 'numeric') then
                                                    cast(c.precision as varchar(4)) + ', ' 
                                                    + cast(c.scale as varchar(4))
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
                    [Default] = case 
                                when def.definition is not null then def.definition 
                                else '' 
                                end,
                    [PK] = case 
                            when pk.column_id is not null then 'PK' 
                            else '' 
                        end, 
                    [FK] = case 
                            when fk.parent_column_id is not null then 'FK' 
                            else ''
                        end, 
                    UniqueKey = case 
                                    when uk.column_id is not null then 'UK' 
                                    else ''
                                end,
                    [Check] = case 
                                when ch.check_const is not null then ch.check_const 
                                else ''
                            end,
                    [Computed] = ISNULL(cc.definition, ''),
                    [Comments] = ISNULL(ep.value, '')
                from sys.tables as tab
                    left join sys.columns as col
                        on tab.object_id = col.object_id
                    left join sys.types as t
                        on col.user_type_id = t.user_type_id
                    left join sys.default_constraints as def
                        on def.object_id = col.default_object_id
                    left join (
                                select index_columns.object_id, 
                                    index_columns.column_id
                                from sys.index_columns
                                    inner join sys.indexes 
                                        on index_columns.object_id = indexes.object_id
                                        and index_columns.index_id = indexes.index_id
                                where indexes.is_primary_key = 1
                                ) as pk 
                        on col.object_id = pk.object_id 
                        and col.column_id = pk.column_id
                    left join (
                                select fc.parent_column_id, 
                                    fc.parent_object_id
                                from sys.foreign_keys as f 
                                    inner join sys.foreign_key_columns as fc 
                                        on f.object_id = fc.constraint_object_id
                                group by fc.parent_column_id, fc.parent_object_id
                                ) as fk
                        on fk.parent_object_id = col.object_id 
                        and fk.parent_column_id = col.column_id    
                    left join (
                                select c.parent_column_id, 
                                    c.parent_object_id, 
                                    'Check' check_const
                                from sys.check_constraints as c
                                group by c.parent_column_id,
                                    c.parent_object_id
                                ) as ch
                        on col.column_id = ch.parent_column_id
                        and col.object_id = ch.parent_object_id
                    left join (
                                select index_columns.object_id, 
                                    index_columns.column_id
                                from sys.index_columns
                                    inner join sys.indexes 
                                        on indexes.index_id = index_columns.index_id
                                        and indexes.object_id = index_columns.object_id
                                where indexes.is_unique_constraint = 1
                                group by index_columns.object_id, 
                                        index_columns.column_id
                                ) as uk
                        on col.column_id = uk.column_id 
                        and col.object_id = uk.object_id
                    left join sys.extended_properties as ep 
                        on tab.object_id = ep.major_id
                        and col.column_id = ep.minor_id
                        and ep.name = '{this.Tag}'
                        and ep.class_desc = 'OBJECT_OR_COLUMN'
                    left join sys.computed_columns as cc
                        on tab.object_id = cc.object_id
                        and col.column_id = cc.column_id
                where tab.schema_id = SCHEMA_ID('{table.Schema}')
                And tab.name = '{table.Name}'
                order by --[Schema],
                        --[Table], 
                        --[Column]
                        col.column_id;";

                hsql.ExecuteSqlData(comando);
                if (hsql.ErrorExiste)
                {
                    hsql.ErrorClear();
                    continue;
                }
                while(hsql.Data.Read())
                {
                    var td = new TableDetails();
                    td.Column = hsql.Data[0].ToString();
                    td.Type = hsql.Data[1].ToString();
                    td.Nullable = hsql.Data[2].ToString();
                    td.Default = hsql.Data[3].ToString();
                    td.PrimaryKey = hsql.Data[4].ToString();
                    td.ForeignKey = hsql.Data[5].ToString();
                    td.UniqueKey = hsql.Data[6].ToString();
                    td.Check = hsql.Data[7].ToString();
                    td.Computed = hsql.Data[8].ToString();
                    td.Comment = hsql.Data[9].ToString();
                    table.TableDetails.Add(td);
                }

            }
        }

    }
}
