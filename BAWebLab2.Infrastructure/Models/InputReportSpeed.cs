using BAWebLab2.Model;

namespace BAWebLab2.Infrastructure.Models
{
    /// <summary>class chứa tham số cần của báo cáo vi phạm tốc độ phương tiện</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/20/2023 created
    /// </Modified>
    public class InputReportSpeed:InputSearchList
    {
        public string? TimeFrom { get; set; }
        public string? TimeTo { get; set; }
    }
}
