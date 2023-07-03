namespace BAWebLab2.DTO
{
	/// <summary>đối tượng nhận đầu vào api tìm kiếm</summary>
	public class InputSearchListDTO
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int GioiTinhSearch { get; set; }
        public DateTime? BirthdayTo { get; set; }
        public DateTime? BirthdayFrom { get; set; }
        public string? UserId { get; set; }
        public string? TextSearch { get; set; }

    }
}
