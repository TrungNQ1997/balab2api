﻿namespace BAWebLab2.Entity
{
    /// <summary>đối tượng map quyền với bảng sql</summary>
    public class UserRole
    {
        public int role_id { get; set; }
        public int user_id { get; set; }
        public int menu_id { get; set; }
        public string action { get; set;}
    }
}
