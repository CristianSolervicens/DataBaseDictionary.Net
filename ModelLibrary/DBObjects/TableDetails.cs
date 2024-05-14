using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary.DBObjects
{
    internal class TableDetails
    {
        internal string Column { get; set; }
        internal string Type { get; set; }
        internal string Nullable { get; set; }
        internal string Default { get; set; }
        internal string PrimaryKey { get; set; }
        internal string ForeignKey { get; set; }
        internal string UniqueKey { get; set;}
        internal string Check { get; set;}
        internal string Computed { get; set; }
        internal string Comment { get; set;}

    }
}
