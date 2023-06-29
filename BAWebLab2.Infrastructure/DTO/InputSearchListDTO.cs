using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAWebLab2.DTO
{
    /// <summary>đối tượng nhận đầu vào api tìm kiếm</summary>
    public class InputSearchListDTO
    {
        public int page_number { get; set; }
        public int page_size { get; set; }
        public int gioi_tinh_search { get; set; }
        public DateTime? birthday_to { get; set; }
        public DateTime? birthday_from { get; set; }
        public string? user_id { get; set; }
        public string? text_search { get; set; }

    }
}
