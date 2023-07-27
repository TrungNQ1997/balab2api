namespace BAWebLab2.Model
{

    /// <summary>class để nhận list trả về từ store theo kiểu đối tượng T1</summary>
    /// <typeparam name="T1">Kiểu đối tượng trả về list</typeparam>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/12/2023 created
    /// </Modified>
    public class MultipleResult<T1>
    {
        public List<T1> ListPrimary { get; set; }

        public IEnumerable<T1> IEnumerablePrimary { get; set; }

    }
}
