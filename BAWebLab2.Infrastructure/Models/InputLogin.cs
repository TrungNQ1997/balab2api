namespace BAWebLab2.Model
{

    /// <summary>đối tượng nhận đầu vào các api login</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/12/2023 created
    /// </Modified>
    public class InputLogin
    {
        /// <summary>Gets or sets the token.</summary>
        /// <value>token của người dùng đang đăng nhập</value>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public string? Token { get; set; }

        /// <summary>Gets or sets the menu id</summary>
        /// <value>id của menuid user mà muốn truy cập</value>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public string? MenuId { get; set; }

        /// <summary>Gets or sets the user id .</summary>
        /// <value>id của user đang đăng nhập</value>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public string? UserId { get; set; }

        /// <summary>Gets or sets the username.</summary>
        /// <value>username của user đang đăng nhập</value>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public string? Username { get; set; }

        /// <summary>Gets or sets the password.</summary>
        /// <value>password của user muốn đăng nhập</value>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public string? Password { get; set; }

        /// <summary>Gets or sets the password old.</summary>
        /// <value>password cũ của user đang đăng nhập</value>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public string? PasswordOld { get; set; }

        /// <summary>Gets or sets the is remember.</summary>
        /// <value>trạng thái ghi nhớ đăng nhập</value>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public bool? IsRemember { get; set; }

    }
}
