using System.ComponentModel.DataAnnotations.Schema;

namespace BAWebLab2.Entity
{
    [Table("SysUserInfo")]
    /// <summary>đối tượng map user với bảng sql</summary>
    public class User
    {
		public int Id { get; set; }
		public string Username { get; set; }
		public string? Password { get; set; }
		public string? FullName { get; set; }
		public int Sex { get; set; }
		public string? Phone { get; set; }
		public DateTime Birthday { get; set; }
		public string? Email { get; set; }
		public bool IsDelete { get; set; }
		public bool IsActive { get; set; }
		public bool IsAdmin { get; set; }
		public int Creator { get; set; }
		public int Editor { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime DateEdited { get; set; }

	}
}
