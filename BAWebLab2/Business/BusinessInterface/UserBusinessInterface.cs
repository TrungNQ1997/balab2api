using BAWebLab2.DTO;
using BAWebLab2.Entity;
using System.Text.Json;

namespace BAWebLab2.Business
{
    public interface IUserBusiness
    {
       public StoreResult<User> GetAllUser(JsonDocument json);
        public StoreResult<User> Login(JsonDocument json);
        public StoreResult<UserRole> CheckLoginAndRole(JsonDocument json);
        public StoreResult<UserRole> GetRole(JsonDocument json);
        public StoreResult<int> AddUser(JsonDocument json);
        public StoreResult<int> EditUser(JsonDocument json);
        public StoreResult<int> ChangePass(JsonDocument json);
        public StoreResult<int> DeleteUser(JsonDocument json);
    }
}
