using BAWebLab2.Infrastructure.Repository.IRepository;
using BAWebLab2.DTO;
using BAWebLab2.Entity;
using Dapper;

using System.Data;
using Microsoft.Data.SqlClient;
using BAWebLab2.Infrastructure.DTO;

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

                //var paramInput = new List<SqlParameter>();
                //var param = new SqlParameter();
                //param.ParameterName = "@username";
                //param.Value = input.TextSearch;
                //paramInput.Add(param);
                //var param1 = new SqlParameter();
                //param1.ParameterName = "@pass";
                //param1.Value = LibCommon.LibCommon.HashMD5(input.UserId);
                //paramInput.Add(param1);
                //var param2 = new SqlParameter();
                //param2.ParameterName = "@isRemember";
                //param2.Value = input.PageNumber==0 ? false:true;
                //paramInput.Add(param2);
                //var param3 = new SqlParameter();
                //param3.ParameterName = "@ret";
                //param3.Value = 0;
                //param3.Direction = ParameterDirection.Output;
                //paramInput.Add(param3);
                //var paramOutput = new List<SqlParameter>();



                //var resultStore = _userRepository.GetListUserProcedure("BAWeb_User_Login",ref paramInput, ref  paramOutput);


                parameters.Add("userId", int.Parse(input.UserId));
                parameters.Add("pageNumber", input.PageNumber);
                parameters.Add("pageSize", input.PageSize);
                parameters.Add("textSearch", input.TextSearch);
                parameters.Add("BirthdayFrom", input.BirthdayFrom);
                parameters.Add("birthdayTo", input.BirthdayTo);
                parameters.Add("gioiTinhSearch", input.GioiTinhSearch);
                parameters.Add("count", 0, DbType.Int64, ParameterDirection.Output);

                var resultStore = _userRepository.CallStoredProcedure<UserDTO>("BAWeb_User_GetUserInfo", ref parameters);

                var u = resultStore.ListPrimary.Where(m => m.FullName.Contains("Trung")).ToList();
                var list = new List<UserDTO>();
                //list.Add(u);
                result.List = u;
                var count = parameters.Get<Int64>("count");

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
                parameters.Add("username", (input.Username));
                parameters.Add("pass", (LibCommon.LibCommon.HashMD5(input.Password)));
                parameters.Add("isRemember", (input.IsRemember  ));
                parameters.Add("ret", 0, DbType.Int64, ParameterDirection.Output);

              var  resultStore = _userRepository.CallStoredProcedure<UserDTO>("BAWeb_User_Login", ref parameters);

                result.List = resultStore.ListPrimary;
                            var ret = parameters.Get<Int64>("ret");
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
                parameters.Add("token", (input.Token));

                parameters.Add("menuId", (int.Parse(input.MenuId  )));
                parameters.Add("ret", 0, DbType.Int64, ParameterDirection.Output);
                parameters.Add("isAdmin", false, DbType.Boolean, ParameterDirection.Output);

               var resultStore = _userRepository.CallStoredProcedure<UserRole>("BAWeb_User_CheckTokenLoginGetRole", ref parameters);
                 
                var isAdmin = parameters.Get<Boolean>("isAdmin");
                var ret = parameters.Get<Int64>("ret");
                result.List = resultStore.ListPrimary;
                result.Success = ret == 0 ? false : true;
                result.Admin = isAdmin;
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
                parameters.Add("userId", (input.UserId  ));

                parameters.Add("menuId", (int.Parse(input.MenuId  )));
                parameters.Add("isAdmin", 0, DbType.Boolean, ParameterDirection.Output);

              var  resultStore = _userRepository.CallStoredProcedure<UserRole>("BAWeb_User_GetRole", ref parameters);

                 var isAdmin = parameters.Get<Boolean>("isAdmin");
                 
                    result.Admin = isAdmin;
               
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
                parameters.Add("password", (LibCommon.LibCommon.HashMD5(input.Password)));
                parameters.Add("passwordOld", (LibCommon.LibCommon.HashMD5(input.PasswordOld)));
                parameters.Add("userId", (int.Parse(input.UserId  )));
                parameters.Add("username", (input.Username));

                parameters.Add("ret", 0, DbType.Int64, ParameterDirection.Output);

              var  resultStore = _userRepository.CallStoredProcedure<int>("BAWeb_User_UpdatePassUserInfo", ref parameters);
                var ret = parameters.Get<Int64>("ret");
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
