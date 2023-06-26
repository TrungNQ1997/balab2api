using System.Text.Json;

namespace BAWebLab2.Business
{
    public interface IUserBusiness
    {
       public Object GetAllUser(JsonDocument json);
        public Object Login(JsonDocument json);
        public Object CheckLoginAndRole(JsonDocument json);
        public Object GetRole(JsonDocument json);
        public Object AddUser(JsonDocument json);
        public Object EditUser(JsonDocument json);
        public Object ChangePass(JsonDocument json);
        public Object DeleteUser(JsonDocument json);
    }
}
