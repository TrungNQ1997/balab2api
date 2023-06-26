using BAWebLab2.Entity;

namespace BAWebLab2.DTO
{
    /// <summary>class dùng để trả về kết quả của api</summary>
    public class ApiResponse<T>
    {
        /// <summary>Gets or sets the status code.</summary>
        /// <value>The status code. trường 
        /// thể hiện trạng thái api có thực hiện thành công không</value>
        public string StatusCode { get; set; }
        /// <summary>Gets or sets the message.</summary>
        /// <value>The message.
        /// hiển thị thông báo rõ hơn của mã lỗi dựa theo HttpStatusCode</value>
        public string Message { get; set; }
        /// <summary>Gets or sets the data.</summary>
        /// <value>The data.
        /// dữ liệu trả về sau khi thực thi store</value>
        public StoreResult<T> Data { get; set; }
    }
}
