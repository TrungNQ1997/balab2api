namespace BAWebLab2.DTO
{
    /// <summary>kết quả trả về của store</summary>
    /// <typeparam name="T">kiểu danh sách đối tượng trả về</typeparam>
    public class StoreResultDTO<T>
    {

        public bool is_success { get; set; }
        public bool is_admin { get; set; }
        public bool is_error { get; set; }
        public int count { get; set; }
        public string message { get; set; }
        public List<T> list { get; set; }
        public StoreResultDTO()
        {
            list = new List<T>();
        }

        

    }
}
