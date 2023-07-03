using BAWebLab2.DAL.Repository.IRepository;
using BAWebLab2.DTO;
using BAWebLab2.DTO.DTO;
using BAWebLab2.Entity;
using Dapper;
//using Newtonsoft.Json.Linq;
using System.Data;

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
                parameters.Add("pUser_id", int.Parse(input.UserId));
                parameters.Add("pPage_number", input.PageNumber );
                parameters.Add("pPage_size", input.PageSize  );
                parameters.Add("pText_search", input.TextSearch  );
                parameters.Add("pBirthday_from", input.BirthdayFrom  );
                parameters.Add("pBirthday_to", input.BirthdayTo);
                parameters.Add("pGioi_tinh_search", input.GioiTinhSearch  );
                parameters.Add("pCount", 0,DbType.Int64,ParameterDirection.Output);
                 
                var resultStore = _userRepository.CallStoredProcedure<UserDTO>("BAWebUserGetSysUserInfo",ref parameters);
                result.List = resultStore.ListPrimary;
                var count = parameters.Get<Int64>("pCount");

                result.Count = (int)count;

                result.Success = true;
                result.Error = false;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Error = true;
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
                parameters.Add("pUsername", (input.Username));
                parameters.Add("pPass", (LibCommon.LibCommon.HashMD5(input.Password)));
                parameters.Add("pis_remember", (input.IsRemember  ));
                parameters.Add("pret", 0, DbType.Int64, ParameterDirection.Output);

              var  resultStore = _userRepository.CallStoredProcedure<UserDTO>("BAWebUserLoginSysUserInfo", ref parameters);

                result.List = resultStore.ListPrimary;
                            var ret = parameters.Get<Int64>("pret");
                          result.Count = (int)ret;
                            result.Error = false;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
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
                parameters.Add("pToken", (input.Token));

                parameters.Add("pMenu_id", (int.Parse(input.MenuId  )));
                parameters.Add("pret", 0, DbType.Int64, ParameterDirection.Output);
                parameters.Add("pis_admin", false, DbType.Boolean, ParameterDirection.Output);

               var resultStore = _userRepository.CallStoredProcedure<UserRole>("BAWebUserCheckTokenLoginAndGetRoleSysUserInfo", ref parameters);
                 
                var is_admin = parameters.Get<Boolean>("pis_admin");
                var ret = parameters.Get<Int64>("pret");
                result.List = resultStore.ListPrimary;
                result.Success = ret == 0 ? false : true;
                result.Admin = is_admin;
                result.Error = false;

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Error = true;
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
                parameters.Add("puser_id", (input.UserId  ));

                parameters.Add("pMenu_id", (int.Parse(input.MenuId  )));
                parameters.Add("pis_admin", 0, DbType.Boolean, ParameterDirection.Output);

              var  resultStore = _userRepository.CallStoredProcedure<UserRole>("BAWebUserGetRoleSysUserInfo", ref parameters);

                 var is_admin = parameters.Get<Boolean>("pis_admin");
                 
                    result.Admin = is_admin;
               
                result.List = resultStore.ListPrimary;
                result.Error = false;
            }
            catch (Exception ex)
            {
                result.Error = true;
                result.Message = ex.Message;
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
                  user.Password = LibCommon.LibCommon.HashMD5(user.Password);
                _userRepository.Update(user);
            }
            catch (Exception ex)
            {
                result.Error = true;
                result.Message = ex.Message;
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
                parameters.Add("ppassword", (LibCommon.LibCommon.HashMD5(input.Password)));
                parameters.Add("ppassword_old", (LibCommon.LibCommon.HashMD5(input.PasswordOld)));
                parameters.Add("puser_id", (int.Parse(input.UserId  )));
                parameters.Add("pusername", (input.Username));

                parameters.Add("pret", 0, DbType.Int64, ParameterDirection.Output);

              var  resultStore = _userRepository.CallStoredProcedure<int>("BAWebUserUpdatePassSysUserInfo", ref parameters);
                var ret = parameters.Get<Int64>("pret");
                if (ret == 0)
                    result.Success = true;
                else
                    result.Success = false;
                result.Error = false;
            }
            catch (Exception ex)
            {
                result.Error = true;
                result.Message = ex.Message;
                LibCommon.LibCommon.WriteLog(ex.ToString());
            }
            return result;
        }
         
        public StoreResultDTO<int> DeleteUser(InputDeleteDTO input)
        {
             
            var result = new StoreResultDTO<int>();
             result = _userRepository.DeleteUser(input);
            if (!result.List.Contains(0))
            {
                result.Error = true;
            }
            
            return result;
        }

          public StoreResultDTO<int> AddUser(User user)
        {

            var result = new StoreResultDTO<int>();

            DynamicParameters parameters = new DynamicParameters();
            try
            {
               
                user.Password =LibCommon.LibCommon.HashMD5(user.Password);

               _userRepository.Add(user);
            }
            catch (Exception ex)
            {
                result.Error = true;
                result.Message = ex.Message;
                LibCommon.LibCommon.WriteLog(ex.ToString());
            }
            return result;
        }
         
    }
}
