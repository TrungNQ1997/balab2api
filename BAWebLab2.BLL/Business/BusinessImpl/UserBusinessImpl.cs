using BAWebLab2.DAL.Repository.IRepository;
using BAWebLab2.DTO;

using BAWebLab2.Entity;
using Dapper;
//using Newtonsoft.Json.Linq;
using System.Data;
using System.Text.Json;

namespace BAWebLab2.Business
{
    /// <summary>class để xử lí parce dữ liệu từ json</summary>
    public class UserBusinessImpl : IUserBusiness
    {
        private readonly IUserRepository _userRepository;


        public UserBusinessImpl(IUserRepository userRepository)
        {

            _userRepository = userRepository;
        }


        /// <summary>parce json lấy tham số truyền vào store lấy danh sách user</summary>
        /// <param name="json">json chứa tham số truyền vào store</param>
        /// <returns>length của list và list select theo offset thỏa mãn điều kiện filter</returns>
        public StoreResultDTO<User> GetAllUser(JsonDocument json)
        {

            var result = new StoreResultDTO<User>();

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
                parameters.Add("pCount", 0,DbType.Int64,ParameterDirection.Output);
                // result = _userRepository.GetAllUsers1(parameters);
                var l1 = _userRepository.GetAllUsers1(parameters);
                result.list = l1;
                var count = parameters.Get<Int64>("pCount");

                result.count = (int)count;

                result.is_success = true;
                result.is_error = false;
            }
            catch (Exception ex)
            {
             //   result = ex.Message;
                LibCommon.LibCommon.WriteLog(ex.ToString());
            }

            return result;
        }



        ///// <summary>api kiểm tra username, pass có hợp lệ không</summary>
        ///// <param name="json">json chứa username, pass</param>
        ///// <returns>có đăng nhập hợp lệ không, thông tin user đăng nhập</returns>
        //public StoreResultDTO<UserDTO> Login(JsonDocument json)
        //{

        //    var result = new StoreResultDTO<UserDTO>();

        //    DynamicParameters parameters = new DynamicParameters();

        //    try
        //    {
        //        parameters.Add("pUsername", (json.RootElement.GetProperty("username").ToString()));
        //        parameters.Add("pPass", (LibCommon.LibCommon.HashMD5(json.RootElement.GetProperty("pass").ToString())));
        //        parameters.Add("pis_remember", (Boolean.Parse(json.RootElement.GetProperty("is_remember").ToString())));
        //        parameters.Add("pret", 0, DbType.Int64, ParameterDirection.Output);

        //        result = _userRepository.Login(parameters);
        //    }
        //    catch (Exception ex)
        //    {
        //        result.message = ex.Message;
        //        LibCommon.LibCommon.WriteLog(ex.ToString());
        //    }
        //    return result;
        //}

        ///// <summary>api kiểm tra đăng nhập theo token và get quyền theo menu_id</summary>
        ///// <param name="json">json chứa token và menu_id</param>
        ///// <returns>đăng nhập có hợp lệ không, user có phải admin không, danh sách quyền</returns>
        //public StoreResultDTO<UserRole> CheckLoginAndRole(JsonDocument json)
        //{

        //    var result = new StoreResultDTO<UserRole>();

        //    DynamicParameters parameters = new DynamicParameters();
        //    try
        //    {
        //        parameters.Add("pToken", (json.RootElement.GetProperty("token").ToString()));

        //        parameters.Add("pMenu_id", (int.Parse(json.RootElement.GetProperty("menu_id").ToString())));
        //        parameters.Add("pret", 0, DbType.Int64, ParameterDirection.Output);
        //        parameters.Add("pis_admin", 0, DbType.Boolean, ParameterDirection.Output);

        //        result = _userRepository.CheckLoginAndRole(parameters);
        //    }
        //    catch (Exception ex)
        //    {
        //        result.message = ex.Message;
        //        result.is_error = true;
        //        LibCommon.LibCommon.WriteLog(ex.ToString());
        //    }
        //    return result;
        //}

        ///// <summary>lấy danh sách quyền theo user_id và menu_id</summary>
        ///// <param name="json">json chứa user_id và menu_id</param>
        ///// <returns>danh sách quyền</returns>
        //public StoreResultDTO<UserRole> GetRole(JsonDocument json)
        //{

        //    var result = new StoreResultDTO<UserRole>();

        //    DynamicParameters parameters = new DynamicParameters();
        //    try
        //    {
        //        parameters.Add("puser_id", (json.RootElement.GetProperty("user_id").ToString()));

        //        parameters.Add("pMenu_id", (int.Parse(json.RootElement.GetProperty("menu_id").ToString())));
        //        parameters.Add("pis_admin", 0, DbType.Boolean, ParameterDirection.Output);

        //        result = _userRepository.GetRole(parameters);
        //    }
        //    catch (Exception ex)
        //    {
        //        result.is_error = true;
        //        result.message = ex.Message;
        //        LibCommon.LibCommon.WriteLog(ex.ToString());
        //    }
        //    return result;
        //}


        ///// <summary>sửa user</summary>
        ///// <param name="json">json chứa thông tin user sửa</param>
        ///// <returns>trạng thái sửa thành công không</returns>
        //public StoreResultDTO<int> EditUser(JsonDocument json)
        //{

        //    var result = new StoreResultDTO<int>();

        //    DynamicParameters parameters = new DynamicParameters();
        //    try
        //    {
        //        parameters.Add("pid", (json.RootElement.GetProperty("id").ToString().Trim()));
        //        parameters.Add("pusername", (json.RootElement.GetProperty("username").ToString().Trim()));

        //        parameters.Add("pho_ten", (json.RootElement.GetProperty("ho_ten").ToString()));
        //        parameters.Add("pgioi_tinh", (json.RootElement.GetProperty("gioi_tinh").ToString()));
        //        parameters.Add("psdt", (json.RootElement.GetProperty("sdt").ToString()));
        //        parameters.Add("pngay_sinh", (json.RootElement.GetProperty("ngay_sinh").ToString()));
        //        parameters.Add("pemail", (json.RootElement.GetProperty("email").ToString()));
        //        parameters.Add("pis_delete", (false));
        //        parameters.Add("pis_active", (json.RootElement.GetProperty("is_active").ToString()));
        //        parameters.Add("pis_admin", (json.RootElement.GetProperty("is_admin").ToString()));
        //        parameters.Add("puser_id", (json.RootElement.GetProperty("user_id").ToString()));

        //        parameters.Add("pret", 0, DbType.Int64, ParameterDirection.Output);

        //        result = _userRepository.EditUser(parameters);
        //    }
        //    catch (Exception ex)
        //    {
        //        result.is_error = true;
        //        result.message = ex.Message;
        //        LibCommon.LibCommon.WriteLog(ex.ToString());
        //    }
        //    return result;
        //}

        ///// <summary>đổi mật khẩu user đang đăng nhập</summary>
        ///// <param name="json">json chứa pass cũ và mới</param>
        ///// <returns>trạng thái đổi mật khẩu thành công không</returns>
        //public StoreResultDTO<int> ChangePass(JsonDocument json)
        //{

        //    var result = new StoreResultDTO<int>();

        //    DynamicParameters parameters = new DynamicParameters();
        //    try
        //    {
        //        parameters.Add("ppassword", (LibCommon.LibCommon.HashMD5(json.RootElement.GetProperty("password").ToString())));
        //        parameters.Add("ppassword_old", (LibCommon.LibCommon.HashMD5(json.RootElement.GetProperty("password_old").ToString())));
        //        parameters.Add("puser_id", (int.Parse(json.RootElement.GetProperty("user_id").ToString())));
        //        parameters.Add("pusername", (json.RootElement.GetProperty("username").ToString()));

        //        parameters.Add("pret", 0, DbType.Int64, ParameterDirection.Output);

        //        result = _userRepository.ChangePass(parameters);
        //    }
        //    catch (Exception ex)
        //    {
        //        result.is_error = true;
        //        result.message = ex.Message;
        //        LibCommon.LibCommon.WriteLog(ex.ToString());
        //    }
        //    return result;
        //}


        ///// <summary>xóa nhiều user</summary>
        ///// <param name="json">json chứa list id user cần xóa và id user gọi api xóa</param>
        ///// <returns>kết quả thực thi store xóa user</returns>
        //public StoreResultDTO<int> DeleteUser(JsonDocument json)
        //{

        //    var jobject = new JObject();
        //    string user_id = "";
        //    var result = new StoreResultDTO<int>();
        //    try
        //    {
        //        jobject = JObject.Parse(json.RootElement.ToString());
        //        user_id = jobject["user_id"].ToString();
        //        var listDelete = jobject["listDelete"].ToArray();

        //        foreach (var item in listDelete)
        //        {
        //            DynamicParameters parameters = new DynamicParameters();
        //            parameters.Add("pid", (item["id"].ToString()));
        //            parameters.Add("pusername", (item["username"].ToString()));

        //            parameters.Add("puser_id", (user_id));

        //            parameters.Add("pret", 0, DbType.Int64, ParameterDirection.Output);

        //            result = _userRepository.DeleteUser(parameters);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.is_error = true;
        //        result.message = ex.Message;
        //        LibCommon.LibCommon.WriteLog(ex.ToString());
        //    }



        //    return result;
        //}



        ///// <summary>thêm user</summary>
        ///// <param name="json">json chứa thông tin user cần thêm</param>
        ///// <returns>trạng thái thêm thành công hay không</returns>
        public StoreResultDTO<int> AddUser(JsonDocument json)
        {

            var result = new StoreResultDTO<int>();

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


                var t = new User();
                t.username = json.RootElement.GetProperty("username").ToString();
                t.password = LibCommon.LibCommon.HashMD5(json.RootElement.GetProperty("password").ToString());
                t.ho_ten = json.RootElement.GetProperty("ho_ten").ToString();


               _userRepository.Add(t);
            }
            catch (Exception ex)
            {
                result.is_error = true;
                result.message = ex.Message;
                LibCommon.LibCommon.WriteLog(ex.ToString());
            }
            return result;
        }



    }
}
