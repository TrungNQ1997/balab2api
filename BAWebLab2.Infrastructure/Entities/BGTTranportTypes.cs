using System.ComponentModel.DataAnnotations.Schema;

namespace BAWebLab2.Entities
{
    /// <summary>class map đối tượng với bảng BGT.TranportTypes trong database</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/20/2023 created
    /// </Modified>
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
