using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jacDB.Core
{
    class RowHeader
    {
        public bool IsDeleted { get; set; }

        public short Length { get; set; }

        public long RowId { get; set; }

        public long RowVersion { get; set; }

    }
}
