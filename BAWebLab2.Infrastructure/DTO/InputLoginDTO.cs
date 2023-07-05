namespace BAWebLab2.DTO
{
	/// <summary>đối tượng nhận đầu vào các api login</summary>
	public class InputLoginDTO
    {
        public string? Token { get; set; }
        public string? MenuId { get; set; }
        public string? UserId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? PasswordOld { get; set; }
        public bool? IsRemember { get; set; }

    }
}
