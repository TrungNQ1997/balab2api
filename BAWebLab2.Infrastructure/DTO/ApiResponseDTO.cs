namespace BAWebLab2.DTO
{
	/// <summary>class dùng để trả về kết quả của api</summary>
	public class ApiResponseDTO<T>
    {
        /// <summary>mã trạng thái</summary>
        /// <value>trường
        /// thể hiện trạng thái api có thực hiện thành công không</value>
        public string StatusCode { get; set; }
        /// <summary>thông báo</summary>
        /// <value> hiển thị thông báo rõ hơn của mã trạng thái dựa theo HttpStatusCode</value>
        public string Message { get; set; }
        /// <summary>kết quả trả về của store</summary>
        /// <value> dữ liệu trả về sau khi thực thi store</value>
        public StoreResultDTO<T> Data { get; set; }
    }
}
