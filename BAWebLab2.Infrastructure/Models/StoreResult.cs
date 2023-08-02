namespace BAWebLab2.Model
{

    /// <summary>kết quả trả về của store</summary>
    /// <typeparam name="T">kiểu danh sách đối tượng trả về</typeparam>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/12/2023 created
    /// </Modified>
    public class StoreResult<T>
    {


        /// <summary>Gets or sets kết quả trạng thái là admin output từ procedure</summary>
        /// <value>
        ///   <c>kết quả trạng thái là admin output từ procedure</c>
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public bool Admin { get; set; }

        /// <summary>Gets or sets trạng thái lỗi</summary>
        /// <value>
        ///   <c>trạng thái lỗi, true:có lỗi, false: không có lỗi</c>
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public bool Error { get; set; }

        /// <summary>Gets or sets biến count.</summary>
        /// <value>để gán giá trị output int procedure trả về</value>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public int Count { get; set; }

        /// <summary>Gets or sets thông báo</summary>
        /// <value>thông báo lỗi nếu có cho client</value>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public string Message { get; set; }

        /// <summary>Gets or sets danh sách đối tượng trả về</summary>
        /// <value>danh sách đối tượng trả về client</value>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public List<T> List { get; set; }

        public StoreResult()
        {
            List = new List<T>();
        }



    }
}
