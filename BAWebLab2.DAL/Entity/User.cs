﻿using System.ComponentModel.DataAnnotations.Schema;

namespace BAWebLab2.Entity
{
    [Table("SysUserInfo")]
    /// <summary>đối tượng map user với bảng sql</summary>
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string ho_ten { get; set; }
        public int gioi_tinh { get; set; }
        public string sdt { get; set;}
        public DateTime ngay_sinh { get; set; }
        public string email { get; set; }
        public bool is_delete { get; set; }
        public bool is_active { get; set; }
        public bool is_admin { get; set; }
        public int cuser { get; set; }
        public int luser { get; set; }
        public DateTime cdate { get; set; }
        public DateTime ldate { get; set; }
         
    }
}
