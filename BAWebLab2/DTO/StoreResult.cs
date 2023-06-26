namespace BAWebLab2.DTO
{
    public class StoreResult<T>
    {

        public bool is_success { get; set; }
        public bool is_admin { get; set; }
        public bool is_error { get; set; }
        public int count { get; set; }
        public string message { get; set; }
        public List<T> list { get; set; }
        public StoreResult()
        {
            list = new List<T>();
        }

        

    }
}
