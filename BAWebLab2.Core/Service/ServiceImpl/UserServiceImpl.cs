using BAWebLab2.DAL.Repository.IRepository;
using BAWebLab2.DTO;
using BAWebLab2.DTO.DTO;
using BAWebLab2.Entity;
using Dapper;
//using Newtonsoft.Json.Linq;
using System.Data;
using System.Text.Json;

namespace BAWebLab2.Business
{
    /// <summary>class để xử lí  dữ liệu từ client</summary>
    public class UserServiceImpl : IUserService
    {
        private readonly IUserRepository _userRepository;


        public UserServiceImpl(IUserRepository userRepository)
        {

            _userRepository = userRepository;
        }


         public StoreResultDTO<UserDTO> GetListUsersFilter(InputSearchListDTO input)
        {

            var result = new StoreResultDTO<UserDTO>();

            DynamicParameters parameters = new DynamicParameters();
            try
            {
                parameters.Add("pUser_id", int.Parse(input.user_id));
                parameters.Add("pPage_number", input.page_number);
                parameters.Add("pPage_size", input.page_size);
                parameters.Add("pText_search", input.text_search);
                parameters.Add("pBirthday_from", input.birthday_from);
                parameters.Add("pBirthday_to", input.birthday_to);
                parameters.Add("pGioi_tinh_search", input.gioi_tinh_search);
                parameters.Add("pCount", 0,DbType.Int64,ParameterDirection.Output);
                 
                var resultStore = _userRepository.CallStoredProcedure<UserDTO>("BAWebUserGetSysUserInfo",ref parameters);
                result.list = resultStore.ListPrimary;
                var count = parameters.Get<Int64>("pCount");

                result.count = (int)count;

                result.is_success = true;
                result.is_error = false;
            }
            catch (Exception ex)
            {
                result.message = ex.Message;
                result.is_error = true;
                LibCommon.LibCommon.WriteLog(ex.ToString());
            }

            return result;
        }



         public StoreResultDTO<UserDTO> Login(InputLoginDTO input)
        {

            var result = new StoreResultDTO<UserDTO>();

            DynamicParameters parameters = new DynamicParameters();

            try
            {
                parameters.Add("pUsername", (input.username));
                parameters.Add("pPass", (LibCommon.LibCommon.HashMD5(input.password)));
                parameters.Add("pis_remember", (input.is_remember));
                parameters.Add("pret", 0, DbType.Int64, ParameterDirection.Output);

              var  resultStore = _userRepository.CallStoredProcedure<UserDTO>("BAWebUserLoginSysUserInfo", ref parameters);

                result.list = resultStore.ListPrimary;
                            var ret = parameters.Get<Int64>("pret");
                          result.count = (int)ret;
                            result.is_error = false;
            }
            catch (Exception ex)
            {
                result.message = ex.Message;
                LibCommon.LibCommon.WriteLog(ex.ToString());
            }
            return result;
        }

        public StoreResultDTO<UserRole> CheckLoginAndRole(InputLoginDTO input)
        {

            var result = new StoreResultDTO<UserRole>();

            DynamicParameters parameters = new DynamicParameters();
            try
            {
                parameters.Add("pToken", (input.token));

                parameters.Add("pMenu_id", (int.Parse(input.menu_id)));
                parameters.Add("pret", 0, DbType.Int64, ParameterDirection.Output);
                parameters.Add("pis_admin", false, DbType.Boolean, ParameterDirection.Output);

               var resultStore = _userRepository.CallStoredProcedure<UserRole>("BAWebUserCheckTokenLoginAndGetRoleSysUserInfo", ref parameters);
                 
                var is_admin = parameters.Get<Boolean>("pis_admin");
                var ret = parameters.Get<Int64>("pret");
                result.list = resultStore.ListPrimary;
                result.is_success = ret == 0 ? false : true;
                result.is_admin = is_admin;
                result.is_error = false;

            }
            catch (Exception ex)
            {
                result.message = ex.Message;
                result.is_error = true;
                LibCommon.LibCommon.WriteLog(ex.ToString());
            }
            return result;
        }

        public StoreResultDTO<UserRole> GetRole(InputLoginDTO input)
        {

            var result = new StoreResultDTO<UserRole>();

            DynamicParameters parameters = new DynamicParameters();
            try
            {
                parameters.Add("puser_id", (input.user_id));

                parameters.Add("pMenu_id", (int.Parse(input.menu_id)));
                parameters.Add("pis_admin", 0, DbType.Boolean, ParameterDirection.Output);

              var  resultStore = _userRepository.CallStoredProcedure<UserRole>("BAWebUserGetRoleSysUserInfo", ref parameters);

                 var is_admin = parameters.Get<Boolean>("pis_admin");
                 
                    result.is_admin = is_admin;
               
                result.list = resultStore.ListPrimary;
                result.is_error = false;
            }
            catch (Exception ex)
            {
                result.is_error = true;
                result.message = ex.Message;
                LibCommon.LibCommon.WriteLog(ex.ToString());
            }
            return result;
        }


        public StoreResultDTO<int> EditUser(User user)
        {

            var result = new StoreResultDTO<int>();

            DynamicParameters parameters = new DynamicParameters();
            try
            {
                  user.password = LibCommon.LibCommon.HashMD5(user.password);
                _userRepository.Update(user);
            }
            catch (Exception ex)
            {
                result.is_error = true;
                result.message = ex.Message;
                LibCommon.LibCommon.WriteLog(ex.ToString());
            }
            return result;
        }

        public StoreResultDTO<int> ChangePass(InputLoginDTO input)
        {
             
            var result = new StoreResultDTO<int>();

            DynamicParameters parameters = new DynamicParameters();
            try
            {
                parameters.Add("ppassword", (LibCommon.LibCommon.HashMD5(input.password)));
                parameters.Add("ppassword_old", (LibCommon.LibCommon.HashMD5(input.password_old)));
                parameters.Add("puser_id", (int.Parse(input.user_id)));
                parameters.Add("pusername", (input.username));

                parameters.Add("pret", 0, DbType.Int64, ParameterDirection.Output);

              var  resultStore = _userRepository.CallStoredProcedure<int>("BAWebUserUpdatePassSysUserInfo", ref parameters);
                var ret = parameters.Get<Int64>("pret");
                if (ret == 0)
                    result.is_success = true;
                else
                    result.is_success = false;
                result.is_error = false;
            }
            catch (Exception ex)
            {
                result.is_error = true;
                result.message = ex.Message;
                LibCommon.LibCommon.WriteLog(ex.ToString());
            }
            return result;
        }
         
        public StoreResultDTO<int> DeleteUser(InputDeleteDTO input)
        {
             
            var result = new StoreResultDTO<int>();
             result = _userRepository.DeleteUser(input);
            if (!result.list.Contains(0))
            {
                result.is_error = true;
            }
            
            return result;
        }

          public StoreResultDTO<int> AddUser(User user)
        {

            var result = new StoreResultDTO<int>();

            DynamicParameters parameters = new DynamicParameters();
            try
            {
               
                user.password=LibCommon.LibCommon.HashMD5(user.password);

               _userRepository.Add(user);
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
