using BAWebLab2.DTO;
using BAWebLab2.Entity;
using System.Text.Json;

namespace BAWebLab2.Business
{
    public interface IUserBusiness
    {
       public StoreResultDTO<User> GetAllUser(JsonDocument json);
        //public StoreResultDTO<UserDTO> Login(JsonDocument json);
        //public StoreResultDTO<UserRole> CheckLoginAndRole(JsonDocument json);
        //public StoreResultDTO<UserRole> GetRole(JsonDocument json);
        public StoreResultDTO<int> AddUser(JsonDocument json);
        //public StoreResultDTO<int> EditUser(JsonDocument json);
        //public StoreResultDTO<int> ChangePass(JsonDocument json);
        //public StoreResultDTO<int> DeleteUser(JsonDocument json);
    }
}
