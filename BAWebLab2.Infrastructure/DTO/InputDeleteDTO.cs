namespace BAWebLab2.DTO
{
    /// <summary>đối tượng nhận đầu vào api xóa user</summary>
    public class InputDeleteDTO
    {
        public string UserId { get; set; }
        public List<UserDTO> Users { get; set; }

    }
}
