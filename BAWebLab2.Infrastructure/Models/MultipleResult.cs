namespace BAWebLab2.Model
{

    /// <summary>class để nhận nhiều list trả về từ 1 store theo kiểu đối tượng T1</summary>
    /// <typeparam name="T1">Kiểu đối tượng trả về list</typeparam>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/12/2023 created
    /// </Modified>
    public class MultipleResult<T1>
    {
        /// <summary>list từ store trả về</summary>
        /// <value>The list primary.</value>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 8/9/2023 created
        /// </Modified>
        public List<T1> ListPrimary { get; set; }

        /// <summary>ienumerable trả về từ store</summary>
        /// <value>The ienumerable primary.</value>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 8/9/2023 created
        /// </Modified>
        public IEnumerable<T1> IEnumerablePrimary { get; set; }

    }
}
