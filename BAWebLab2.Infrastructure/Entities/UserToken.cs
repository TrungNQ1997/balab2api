using System.ComponentModel.DataAnnotations.Schema;

namespace BAWebLab2.Infrastructure.Entities
{
    /// <summary>class chứa thông tin token</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 8/15/2023 created
    /// </Modified>
    [NotMapped]
    public class UserToken
    {
        public int UserID { get; set; }

        public string Token { get; set; }

        public int CompanyID { get; set; }

        public DateTime ExpiredDate { get; set; }

        public UserToken() { }
    }
}
