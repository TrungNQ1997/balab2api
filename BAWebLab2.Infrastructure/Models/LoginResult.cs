using System.Diagnostics.CodeAnalysis;

namespace BAWebLab2.Model
{
    /// <summary>class nhận kết quả trả về api login test</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/12/2023 created
    /// </Modified>
    public class LoginResult
    {
        [AllowNull]
        public string? Username { get; set; }

        [AllowNull]
        public int? UserId { get; set; }

        [AllowNull]
        public string? Token { get; set; }

        [AllowNull]
        public DateTime? ExpiredDate { get; set; }
    }
}
