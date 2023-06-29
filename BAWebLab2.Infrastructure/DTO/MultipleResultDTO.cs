using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAWebLab2.DTO.DTO
{
    /// <summary>class để nhận list trả về từ store theo kiểu đối tượng T1</summary>
    /// <typeparam name="T1">Kiểu đối tượng trả về list</typeparam>
    public class MultipleResultDTO<T1>
    {
        public List<T1> ListPrimary { get; set; }
        
    }
}
