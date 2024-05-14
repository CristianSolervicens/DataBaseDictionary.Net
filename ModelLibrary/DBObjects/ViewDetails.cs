using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary.DBObjects
{
    internal class ViewDetails
    {
        internal string Column { get; set; }
        internal string Type { get; set; }
        internal string Nullable { get; set; }
        internal string Comment { get; set; }
    }
}
