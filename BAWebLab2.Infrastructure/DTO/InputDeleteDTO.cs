using BAWebLab2.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAWebLab2.DTO
{
    /// <summary>đối tượng nhận đầu vào api xóa user</summary>
    public class InputDeleteDTO
    {
        public string user_id { get; set; }
        public List<UserDTO> users { get; set; }

    }
}
