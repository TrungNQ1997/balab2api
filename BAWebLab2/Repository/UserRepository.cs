using BAWebLab2.LibCommon;
using Dapper;
 
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
 

public class UserRepository
{
    private readonly string _connectionString;

    public UserRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public  Object GetAllProducts(DynamicParameters param)
    {
        dynamic myObject = new ExpandoObject();
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                 
                var results = connection.QueryMultiple("BAWebUserGetSysUserInfo", param, commandType: CommandType.StoredProcedure);
                var count = results.Read<Object>().ToList();
                var list = results.Read<Object>().ToList();
                 
                myObject.count = count;
                myObject.list = list;
                 
            }
        }
        catch (Exception ex)
        {
            myObject.result = 99;
            myObject.exception = ex.Message;
            LibCommon.WriteLog(ex.ToString());
        }
        return myObject;

    }

    public Object Login(DynamicParameters param)
    {
        dynamic myObject = new ExpandoObject();
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                 
                var userInfo = connection.Query("BAWebUserLoginSysUserInfo", param, commandType: CommandType.StoredProcedure);
                var result = param.Get<Int64>("pret");
                 
                myObject.result = result;
                myObject.userInfo = userInfo;
                  
            }
        }
        catch (Exception ex)
        {
            myObject.result = 99;
            myObject.exception = ex.Message;
            LibCommon.WriteLog(ex.ToString());
        }
        return myObject;

    }

    public Object CheckLoginAndRole(DynamicParameters param)
    {
        dynamic myObject = new ExpandoObject();
        try
        {
            using (var connection = new SqlConnection(_connectionString))
        {
                 
            var tables = connection.QueryMultiple("BAWebUserCheckTokenLoginAndGetRoleSysUserInfo", param, commandType: CommandType.StoredProcedure);
            var isAdmin = tables.Read<Object>().ToList();
            var role = tables.Read<Object>().ToList();
            var result = param.Get<Int64>("pret");
              
            myObject.is_login = result;
            myObject.is_admin = isAdmin;
            myObject.role = role;
             
            }
        }
        catch (Exception ex)
        {
            myObject.result = 99;
            myObject.exception = ex.Message;
            LibCommon.WriteLog(ex.ToString());
        }
        return myObject;

    }

    public Object GetRole(DynamicParameters param)
    {
        dynamic myObject = new ExpandoObject();
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                 
                var result = connection.QueryMultiple("BAWebUserGetRoleSysUserInfo", param, commandType: CommandType.StoredProcedure);
                var isAdmin = result.Read<Object>().ToList();
                var role = result.Read<Object>().ToList();
                 
                myObject.is_admin = isAdmin;
                myObject.role = role;
                return myObject;

            }
        }
        catch (Exception ex)
        {
            myObject.result = 99;
            myObject.exception = ex.Message;
            LibCommon.WriteLog(ex.ToString());
        }
        return myObject;

    }
     
    public Object AddUser(DynamicParameters param)
    {
        dynamic myObject = new ExpandoObject();
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                 
                var customer = connection.Query("BAWebUserInsertSysUserInfo", param, commandType: CommandType.StoredProcedure);
                var result = param.Get<Int64>("pret");
                 
                myObject.result = result;
                 
            }
        } catch (Exception ex)
        {
            myObject.result = 99;
            
            myObject.exception = ex.Message;
            LibCommon.WriteLog(ex.ToString());
        }
        return myObject;

    }

    public Object EditUser(DynamicParameters param)
    {
        dynamic myObject = new ExpandoObject();
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                 
                var table = connection.Query("BAWebUserUpdateSysUserInfo", param, commandType: CommandType.StoredProcedure);
                var result = param.Get<Int64>("pret");
                 
                myObject.result = result;
               
            }
        }
        catch (Exception ex)
        {
            myObject.result = 99;
            myObject.exception = ex.Message;
            LibCommon.WriteLog(ex.ToString());
        }
        return myObject;

    }

    public Object ChangePass(DynamicParameters param)
    {
        dynamic myObject = new ExpandoObject();
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                 
                var table = connection.Query("BAWebUserUpdatePassSysUserInfo", param, commandType: CommandType.StoredProcedure);
                var result = param.Get<Int64>("pret");
                 
                myObject.result = result;
                return myObject;

            }
        }
        catch (Exception ex)
        {
            myObject.result = 99;
            myObject.exception = ex.Message;
            LibCommon.WriteLog(ex.ToString());
        }
        return myObject;

    }


    public Object DeleteUser(DynamicParameters param)
    {
        dynamic myObject = new ExpandoObject();
        try
        {
            using (var connection = new SqlConnection(_connectionString))
        {
                 
            var table = connection.Query("BAWebUserDeleteSysUserInfo", param, commandType: CommandType.StoredProcedure);
            var result = param.Get<Int64>("pret");
                 
            myObject.result = result;
            return myObject;

            }
        }
        catch (Exception ex)
        {
            myObject.result = 99;
            myObject.exception = ex.Message;
            LibCommon.WriteLog(ex.ToString());
        }
        return myObject;
         
    }

}
