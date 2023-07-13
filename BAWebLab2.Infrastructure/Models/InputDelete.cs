namespace BAWebLab2.Model
{

    /// <summary>đối tượng nhận đầu vào api xóa user</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/12/2023 created
    /// </Modified>
    public class InputDelete
    {
        /// <summary>Gets or sets user id</summary>
        /// <value>id user thực hiện xóa</value>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public string UserId { get; set; }

        /// <summary>Gets or sets the users.</summary>
        /// <value>danh sách user cần xóa</value>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public List<UserModel> Users { get; set; }

    }
}
