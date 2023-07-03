namespace BAWebLab2.DTO
{
    /// <summary>kết quả trả về của store</summary>
    /// <typeparam name="T">kiểu danh sách đối tượng trả về</typeparam>
    public class StoreResultDTO<T>
    {

        public bool Success { get; set; }
        public bool Admin { get; set; }
        public bool Error { get; set; }
        public int Count { get; set; }
        public string Message { get; set; }
        public List<T> List { get; set; }
        public StoreResultDTO()
        {
            List = new List<T>();
        }

        

    }
}
