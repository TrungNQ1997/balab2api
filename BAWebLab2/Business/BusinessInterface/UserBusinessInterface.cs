using Dapper;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace BAWebLab2.Business
{
    public interface IUserBusiness
    {
       public Object getAllUser(JsonDocument json);
        public Object login(JsonDocument json);
    }
}
