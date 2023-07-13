namespace BAWebLab2.Model
{

    /// <summary>đối tượng để lấy danh sách user</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/12/2023 created
    /// </Modified>
    public class UserModel  
    {
        public int? Id { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

        public string? FullName { get; set; }

        public int? SexId { get; set; }

        public string? Phone { get; set; }

        public DateTime? Birthday { get; set; }

        public string? Email { get; set; }

        public bool? IsDelete { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsAdmin { get; set; }

        public int? Creator { get; set; }

        public int? Editor { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateEdited { get; set; }
         
        public DateTime? ExpiredDate { get; set; }
         
        public string? GioiTinhText { get; set; }

        public string? Token { get; set; }

        public int? Stt { get; set; }

        public int? UserId { get; set; }
        
    }
}
