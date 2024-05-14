using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary.DBObjects
{
    internal class View
    {
        public View(){
            ViewDetails = [];
        }

        internal string Schema { get; set; }
        internal string Name { get; set; }
        internal string? Comment { get; set; }

        internal IList<ViewDetails> ViewDetails { get; set; }
    }
}
