using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary.DBObjects
{
    internal class Table
    {
        public Table() {
            TableDetails = [];
        }

        internal string Schema { get; set; }
        internal string Name { get; set; }
        internal string? Comment { get; set; }

        internal IList<TableDetails> TableDetails { get; set; }
    }
}
