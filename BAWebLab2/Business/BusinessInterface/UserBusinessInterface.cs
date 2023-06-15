using Dapper;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace BAWebLab2.Business
{
    public interface IUserBusiness
    {
       public Object getAllUser(JsonDocument json);
        public Object login(JsonDocument json);
        public Object checkLoginAndRole(JsonDocument json);
        public Object getRole(JsonDocument json);
        public Object addUser(JsonDocument json);
        public Object editUser(JsonDocument json);
        public Object deleteUser(JsonDocument json);
    }
}
