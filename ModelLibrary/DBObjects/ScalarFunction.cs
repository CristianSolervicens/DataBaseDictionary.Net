using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary.DBObjects
{
    internal class ScalarFunction
    {
        public string Schema { get; internal set; }
        public string Name { get; internal set; }
        public string? Comment { get; internal set; }
    }
}
