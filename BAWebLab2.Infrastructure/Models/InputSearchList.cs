namespace BAWebLab2.Model
{

    /// <summary>đối tượng nhận đầu vào api tìm kiếm</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/12/2023 created
    /// </Modified>
    public class InputSearchList
    {
        /// <summary>Gets or sets số trang muốn lấy</summary>
        /// <value>số trang muốn lấy</value>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public int PageNumber { get; set; }

        /// <summary>Gets or sets số dòng mỗi trang</summary>
        /// <value>số dòng mỗi trang</value>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public int PageSize { get; set; }

        /// <summary>Gets or sets tìm kiếm giới tính</summary>
        /// <value>tìm kiếm giới tính 0-tất cả, 1-nam, 2-nữ, 3-tất cả</value>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public int SexSearch { get; set; }

        /// <summary>Gets or sets ngày  đến</summary>
        /// <value>tìm kiếm  theo ngày  đến</value>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public DateTime? DayTo { get; set; }

        /// <summary>Gets or sets ngày  từ</summary>
        /// <value>tìm kiếm  theo ngày  từ</value>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public DateTime? DayFrom { get; set; }

        /// <summary>Gets or sets the user id.</summary>
        /// <value>id user gọi api</value>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public int UserId { get; set; }

        /// <summary>Gets or sets nôi dung tìm kiếm theo username, sdt, ..</summary>
        /// <value>nôi dung tìm kiếm theo username, sdt, ..</value>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public string TextSearch { get; set; }



    }
}
