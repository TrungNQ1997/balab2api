namespace BAWebLab2.Model
{
    /// <summary>class dùng để trả về kết quả của api</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/12/2023 created
    /// </Modified>
    public class ApiResponse<T>
    {
        /// <summary>mã trạng thái</summary>
        /// <value>trường
        /// thể hiện trạng thái api có thực hiện thành công không</value>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public string StatusCode { get; set; }

        /// <summary>thông báo</summary>
        /// <value> hiển thị thông báo rõ hơn của mã trạng thái dựa theo HttpStatusCode</value>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public string Message { get; set; }

        /// <summary>kết quả trả về của store</summary>
        /// <value> dữ liệu trả về sau khi thực thi store</value>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public StoreResult<T> Data { get; set; }
    }
}
