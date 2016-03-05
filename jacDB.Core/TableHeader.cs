using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jacDB.Core
{
    class TableHeader
    {
        public string Name { get; set; }

        public long Count { get; set; }

        public long LastRowId { get; set; }

        public long LastRowVersion { get; set; }
    }
}
