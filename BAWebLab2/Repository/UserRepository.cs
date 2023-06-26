using BAWebLab2.LibCommon;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using BAWebLab2.Entity;
using BAWebLab2.DTO;

public class UserRepository
{
    private readonly string _connectionString;

    public UserRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public StoreResult<User> GetAllUsers(DynamicParameters param)
    {
        //dynamic myObject = new ExpandoObject();
        var result = new StoreResult<User>();
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                 
                var resultsExcute = connection.QueryMultiple("BAWebUserGetSysUserInfo", param, commandType: CommandType.StoredProcedure);
                
                var list = resultsExcute.Read<User>().ToList();

                var count = param.Get<Int64>("pCount");

                
                result.count = (int)count;
                result.list = list;
                result.is_success = true;
                result.is_error = false;
                //myObject.count = count;
                //myObject.list = list;
                 
            }
        }
        catch (Exception ex)
        {
            result.is_error = true;
            result.is_success = false;
            result.message = ex.Message;
            LibCommon.WriteLog(ex.ToString());
        }
        return result;

    }

    public StoreResult<User> Login(DynamicParameters param)
    {
        var result = new StoreResult<User>();
        //dynamic myObject = new ExpandoObject();
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                 
                var userInfo = connection.Query<User>("BAWebUserLoginSysUserInfo", param, commandType: CommandType.StoredProcedure).AsList();
                //var ret = param.Get<Int64>("pret");

                result.list = userInfo;
                var ret = param.Get<Int64>("pret");
                result.count = (int)ret;
                result.is_error = false;
                //myObject.result = result;
                //myObject.userInfo = userInfo;
                  
            }
        }
        catch (Exception ex)
        {
            result.is_error = true;
            result.message = ex.Message;
            LibCommon.WriteLog(ex.ToString());
        }
        return result;

    }

    public StoreResult<UserRole> CheckLoginAndRole(DynamicParameters param)
    {
        var result = new StoreResult<UserRole>();
        //dynamic myObject = new ExpandoObject();
        try
        {
            using (var connection = new SqlConnection(_connectionString))
        {
                 
            var tables = connection.QueryMultiple("BAWebUserCheckTokenLoginAndGetRoleSysUserInfo", param, commandType: CommandType.StoredProcedure);
            
            var role = tables.Read<UserRole>().ToList();
            //var result = param.Get<Int64>("pret");
                var is_admin = param.Get<Boolean>("pis_admin");
                var ret = param.Get<Int64>("pret");
                result.list = role;
                result.is_success = ret == 0? false: true;
                result.is_admin = is_admin;
                result.is_error = false;
            //    myObject.is_login = result;
            //myObject.is_admin = isAdmin;
            //myObject.role = role;
             
            }
        }
        catch (Exception ex)
        {
            result.is_error = true;
            //result.message = 99;
            result.message = ex.Message;
            LibCommon.WriteLog(ex.ToString());
        }
        return result;

    }

    public StoreResult<UserRole> GetRole(DynamicParameters param)
    {
        var result = new StoreResult<UserRole>();
        //dynamic myObject = new ExpandoObject();
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                 
                var resultSQL = connection.QueryMultiple("BAWebUserGetRoleSysUserInfo", param, commandType: CommandType.StoredProcedure);
                
                var role = resultSQL.Read<UserRole>().ToList();
                var is_admin = param.Get<Boolean>("pis_admin");

                result.is_admin= is_admin;
                result.list= role;
                result.is_error = false;

                //var isAdmin = result.Read<Object>().ToList();
                //myObject.is_admin = isAdmin;
                //myObject.role = role;
                //return myObject;

            }
        }
        catch (Exception ex)
        {
            result.is_error = true;
            result.message = ex.Message;
            LibCommon.WriteLog(ex.ToString());
        }
        return result;

    }
     
    public StoreResult<int> AddUser(DynamicParameters param)
    {
        var result = new StoreResult<int>();
        //dynamic myObject = new ExpandoObject();
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                 
                var resultSQL = connection.Query("BAWebUserInsertSysUserInfo", param, commandType: CommandType.StoredProcedure);
                var isError = param.Get<Int64>("pret");
                 if(isError == 0)
                    result.is_success = true;
                 else 
                    result.is_success= false;
                 result.is_error = false;   
                //myObject.result = result;
                 
            }
        } catch (Exception ex)
        {
              result.is_error = true;
            result.is_success = false;
            result.message = ex.Message;
            LibCommon.WriteLog(ex.ToString());
        }
        return result;

    }

    public StoreResult<int> EditUser(DynamicParameters param)
    {

        var myObject = new StoreResult<int>();
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                 
                var table = connection.Query("BAWebUserUpdateSysUserInfo", param, commandType: CommandType.StoredProcedure);
                var result = param.Get<Int64>("pret");
                if (result == 0)
                    myObject.is_success = true;
                else
                    myObject.is_success = false;
                myObject.is_error = false;
               
            }
        }
        catch (Exception ex)
        {
            myObject.is_success = false;
            myObject.is_error = true;
            myObject.message = ex.Message;
            LibCommon.WriteLog(ex.ToString());
        }
        return myObject;

    }

    public StoreResult<int> ChangePass(DynamicParameters param)
    {
        var myObject = new StoreResult<int>();
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                 
                var table = connection.Query("BAWebUserUpdatePassSysUserInfo", param, commandType: CommandType.StoredProcedure);
                var result = param.Get<Int64>("pret");
                if (result == 0)
                    myObject.is_success = true;
                else
                    myObject.is_success = false;
                myObject.is_error = false;


            }
        }
        catch (Exception ex)
        {
            myObject.is_success = false;
            myObject.is_error = true;
            myObject.message = ex.Message;
            LibCommon.WriteLog(ex.ToString());
        }
        return myObject;

    }


    public StoreResult<int> DeleteUser(DynamicParameters param)
    {
        var myObject = new StoreResult<int>();
        try
        {
            using (var connection = new SqlConnection(_connectionString))
        {
                 
            var table = connection.Query("BAWebUserDeleteSysUserInfo", param, commandType: CommandType.StoredProcedure);
            var result = param.Get<Int64>("pret");
                myObject.list.Add((int)result);
                if (result == 0)
                    myObject.is_success = true;
                else
                    myObject.is_success = false;
                myObject.is_error = false;

            }
        }
        catch (Exception ex)
        {
            myObject.is_error = true;
            myObject.message = ex.Message;
            LibCommon.WriteLog(ex.ToString());
        }
        return myObject;
         
    }

}
