using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAWebLab2.Entities
{
    [Table("BGT.TranportTypes")]
    public class BGTTranportTypes
    {

        public int PK_TransportTypeID { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public short SortOrder { get; set; }

        public bool IsActivated { get; set; }


    }
}
