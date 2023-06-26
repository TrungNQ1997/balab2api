using Dapper;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Text.Json;

namespace BAWebLab2.Business
{
    public class UserBusinessImpl : IUserBusiness
    {
        private readonly UserRepository _userRepository;


        public UserBusinessImpl(UserRepository userRepository)
        {

            _userRepository = userRepository;
        }


        public Object GetAllUser(JsonDocument json)
        {

            var result = new Object();

            DynamicParameters parameters = new DynamicParameters();
            try
            {
                parameters.Add("pUser_id", int.Parse(json.RootElement.GetProperty("user_id").ToString()));
                parameters.Add("pPage_number", int.Parse(json.RootElement.GetProperty("page_number").ToString()));
                parameters.Add("pPage_size", int.Parse(json.RootElement.GetProperty("page_size").ToString()));
                parameters.Add("pText_search", json.RootElement.GetProperty("text_search").ToString());
                parameters.Add("pBirthday_from", json.RootElement.GetProperty("birthday_from").ToString() == "" ? new DateTime(1900, 1, 1) :
                    DateTime.Parse(json.RootElement.GetProperty("birthday_from").ToString()), DbType.DateTime2, ParameterDirection.Input);
                parameters.Add("pBirthday_to", json.RootElement.GetProperty("birthday_to").ToString() == "" ? new DateTime(1900, 1, 1) :
                    DateTime.Parse(json.RootElement.GetProperty("birthday_to").ToString()), DbType.DateTime2, ParameterDirection.Input);
                parameters.Add("pGioi_tinh_search", int.Parse(json.RootElement.GetProperty("gioi_tinh_search").ToString()));
                result = _userRepository.GetAllProducts(parameters);
            }
            catch (Exception ex)
            {
                result = ex.Message;
                LibCommon.LibCommon.WriteLog(ex.ToString());
            }

            return result;
        }



        public Object Login(JsonDocument json)
        {

            var result = new Object();

            DynamicParameters parameters = new DynamicParameters();

            try
            {
                parameters.Add("pUsername", (json.RootElement.GetProperty("username").ToString()));
                parameters.Add("pPass", (LibCommon.LibCommon.HashMD5(json.RootElement.GetProperty("pass").ToString())));
                parameters.Add("pis_remember", (Boolean.Parse(json.RootElement.GetProperty("is_remember").ToString())));
                parameters.Add("pret", 0, DbType.Int64, ParameterDirection.Output);

                result = _userRepository.Login(parameters);
            }
            catch (Exception ex)
            {
                result = ex.Message;
                LibCommon.LibCommon.WriteLog(ex.ToString());
            }
            return result;
        }

        public Object CheckLoginAndRole(JsonDocument json)
        {

            var result = new Object();

            DynamicParameters parameters = new DynamicParameters();
            try
            {
                parameters.Add("pToken", (json.RootElement.GetProperty("token").ToString()));

                parameters.Add("pMenu_id", (int.Parse(json.RootElement.GetProperty("menu_id").ToString())));
                parameters.Add("pret", 0, DbType.Int64, ParameterDirection.Output);

                result = _userRepository.CheckLoginAndRole(parameters);
            }
            catch (Exception ex)
            {
                result = ex.Message;
                LibCommon.LibCommon.WriteLog(ex.ToString());
            }
            return result;
        }

        public Object GetRole(JsonDocument json)
        {

            var result = new Object();

            DynamicParameters parameters = new DynamicParameters();
            try
            {
                parameters.Add("puser_id", (json.RootElement.GetProperty("user_id").ToString()));

                parameters.Add("pMenu_id", (int.Parse(json.RootElement.GetProperty("menu_id").ToString())));


                result = _userRepository.GetRole(parameters);
            }
            catch (Exception ex)
            {
                result = ex.Message;
                LibCommon.LibCommon.WriteLog(ex.ToString());
            }
            return result;
        }


        public Object AddUser(JsonDocument json)
        {

            var result = new Object();

            DynamicParameters parameters = new DynamicParameters();
            try
            {
                parameters.Add("pusername", (json.RootElement.GetProperty("username").ToString()));
                parameters.Add("ppassword", (LibCommon.LibCommon.HashMD5(json.RootElement.GetProperty("password").ToString())));
                parameters.Add("pho_ten", (json.RootElement.GetProperty("ho_ten").ToString()));
                parameters.Add("pgioi_tinh", (json.RootElement.GetProperty("gioi_tinh").ToString()));
                parameters.Add("psdt", (json.RootElement.GetProperty("sdt").ToString()));
                parameters.Add("pngay_sinh", (json.RootElement.GetProperty("ngay_sinh").ToString()));
                parameters.Add("pemail", (json.RootElement.GetProperty("email").ToString()));
                parameters.Add("pis_delete", (false));
                parameters.Add("pis_active", (json.RootElement.GetProperty("is_active").ToString()));
                parameters.Add("pis_admin", (json.RootElement.GetProperty("is_admin").ToString()));
                parameters.Add("puser_id", (json.RootElement.GetProperty("user_id").ToString()));

                parameters.Add("pret", 0, DbType.Int64, ParameterDirection.Output);

                result = _userRepository.AddUser(parameters);
            }
            catch (Exception ex)
            {
                result = ex.Message;
                LibCommon.LibCommon.WriteLog(ex.ToString());
            }
            return result;
        }

        public Object EditUser(JsonDocument json)
        {

            var result = new Object();

            DynamicParameters parameters = new DynamicParameters();
            try
            {
                parameters.Add("pid", (json.RootElement.GetProperty("id").ToString().Trim()));
                parameters.Add("pusername", (json.RootElement.GetProperty("username").ToString().Trim()));
                if (json.RootElement.GetProperty("password").ToString() == "")
                {
                    parameters.Add("ppassword", "");
                }
                else
                {
                    parameters.Add("ppassword", (LibCommon.LibCommon.HashMD5(json.RootElement.GetProperty("password").ToString())));
                }

                parameters.Add("pho_ten", (json.RootElement.GetProperty("ho_ten").ToString()));
                parameters.Add("pgioi_tinh", (json.RootElement.GetProperty("gioi_tinh").ToString()));
                parameters.Add("psdt", (json.RootElement.GetProperty("sdt").ToString()));
                parameters.Add("pngay_sinh", (json.RootElement.GetProperty("ngay_sinh").ToString()));
                parameters.Add("pemail", (json.RootElement.GetProperty("email").ToString()));
                parameters.Add("pis_delete", (false));
                parameters.Add("pis_active", (json.RootElement.GetProperty("is_active").ToString()));
                parameters.Add("pis_admin", (json.RootElement.GetProperty("is_admin").ToString()));
                parameters.Add("puser_id", (json.RootElement.GetProperty("user_id").ToString()));

                parameters.Add("pret", 0, DbType.Int64, ParameterDirection.Output);

                result = _userRepository.EditUser(parameters);
            }
            catch (Exception ex)
            {
                result = ex.Message;
                LibCommon.LibCommon.WriteLog(ex.ToString());
            }
            return result;
        }

        public Object ChangePass(JsonDocument json)
        {

            var result = new Object();

            DynamicParameters parameters = new DynamicParameters();
            try
            {
                parameters.Add("ppassword", (LibCommon.LibCommon.HashMD5(json.RootElement.GetProperty("password").ToString())));
                parameters.Add("ppassword_old", (LibCommon.LibCommon.HashMD5(json.RootElement.GetProperty("password_old").ToString())));
                parameters.Add("puser_id", (int.Parse(json.RootElement.GetProperty("user_id").ToString())));
                parameters.Add("pusername", (json.RootElement.GetProperty("username").ToString()));

                parameters.Add("pret", 0, DbType.Int64, ParameterDirection.Output);

                result = _userRepository.ChangePass(parameters);
            }
            catch (Exception ex)
            {
                result = ex.Message;
                LibCommon.LibCommon.WriteLog(ex.ToString());
            }
            return result;
        }


        /// <summary>Deletes the user.</summary>
        /// <param name="json">đối tượng jsondocument nhận từ api</param>
        /// <returns>kết quả thực thi store xóa user</returns>
        public Object DeleteUser(JsonDocument json)
        {

            var jobject = new JObject();
            string user_id = "";
            var result = new Object();
            try
            {
                jobject = JObject.Parse(json.RootElement.ToString());
                user_id = jobject["user_id"].ToString();
                var listDelete = jobject["listDelete"].ToArray();

                foreach (var item in listDelete)
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("pid", (item["id"].ToString()));
                    parameters.Add("pusername", (item["username"].ToString()));

                    parameters.Add("puser_id", (user_id));

                    parameters.Add("pret", 0, DbType.Int64, ParameterDirection.Output);

                    result = _userRepository.DeleteUser(parameters);
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
                LibCommon.LibCommon.WriteLog(ex.ToString());
            }
            return result;
        }


    }
}
