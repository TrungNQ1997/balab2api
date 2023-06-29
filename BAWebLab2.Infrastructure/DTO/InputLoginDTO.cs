using BAWebLab2.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAWebLab2.DTO.DTO
{
    /// <summary>đối tượng nhận đầu vào các api login</summary>
    public class InputLoginDTO
    {
        public string? token { get; set; }
        public string? menu_id { get; set; }
        public string? user_id { get; set; }
        public string? username { get; set; }
        public string? password { get; set; }
        public string? password_old { get; set; }
        public bool? is_remember { get; set; }

    }
}
