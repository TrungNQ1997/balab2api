namespace BAWebLab2.Entity
{
    /// <summary>đối tượng map quyền với bảng sql</summary>
    public class UserRole
    {
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public int MenuId { get; set; }
        public string Action { get; set;}
    }
}
