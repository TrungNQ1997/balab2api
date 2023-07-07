using BAWebLab2.DTO;
using BAWebLab2.Entity;
using BAWebLab2.Infrastructure.DTO;
using Microsoft.Data.SqlClient;

namespace BAWebLab2.Infrastructure.Repository.IRepository
{
	public interface   IUserRepository : IGenericRepository<User>
    {

        /// <summary>xóa danh sách user</summary>
        /// <param name="input">đối tượng chứa danh sách user cần xóa, id user thực hiện xóa</param>
        /// <returns>chuỗi kết quả xóa: 0- thành công, khác 0- thất bại</returns>
        public StoreResultDTO<int> DeleteUser(InputDeleteDTO input);
        public MultipleResultDTO<LoginResultDTO> GetListUserProcedure(string procedure, ref List<SqlParameter> input, ref List<SqlParameter> output);
    }
}
