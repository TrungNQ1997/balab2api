using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Reflection.PortableExecutable;
using System.Text.Json;
using System.Threading.Tasks;

public class UserRepository
{
    private readonly string _connectionString;

    public UserRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public  Object GetAllProducts(DynamicParameters param)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
             
           
            var customer = connection.QueryMultiple("getListUserPagination", param, commandType: CommandType.StoredProcedure) ;
            var ProductListOne = customer.Read<Object>().ToList();
            var ProductListTwo = customer.Read<Object>().ToList();

            dynamic myObject = new ExpandoObject();
            myObject.count = ProductListOne;
            myObject.list = ProductListTwo;
              
            return myObject;
                 
        }
         
    }

    public Object Login(DynamicParameters param)
    {
        dynamic myObject = new ExpandoObject();
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {


                var customer = connection.Query("UserLogin", param, commandType: CommandType.StoredProcedure);
                var result = param.Get<Int64>("pret");

                
                myObject.result = result;
                myObject.userInfo = customer;
                return myObject;

            }
        }
        catch (Exception ex)
        {
            myObject.result = 99;
            myObject.exception = ex.ToString();
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


            var customer = connection.QueryMultiple("CheckTokenLoginAndGetRole", param, commandType: CommandType.StoredProcedure);
            var ProductListOne = customer.Read<Object>().ToList();
            var ProductListTwo = customer.Read<Object>().ToList();
            var result = param.Get<Int64>("pret");
             
            
            myObject.is_login = result;
            myObject.is_admin = ProductListOne;
            myObject.role = ProductListTwo;
            return myObject;

            }
        }
        catch (Exception ex)
        {
            myObject.result = 99;
            myObject.exception = ex.ToString();
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


                var customer = connection.QueryMultiple("GetRoleSysUserInfo", param, commandType: CommandType.StoredProcedure);
                var ProductListOne = customer.Read<Object>().ToList();
                var ProductListTwo = customer.Read<Object>().ToList();
                


                
                myObject.is_admin = ProductListOne;
                myObject.role = ProductListTwo;
                return myObject;

            }
        }
        catch (Exception ex)
        {
            myObject.result = 99;
            myObject.exception = ex.ToString();
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


                var customer = connection.Query("sysUserInfoAdd", param, commandType: CommandType.StoredProcedure);
                var result = param.Get<Int64>("pret");

                
                myObject.result = result;
                

            }
        } catch (Exception ex)
        {
            myObject.result = 99;
            
            myObject.exception = ex.ToString();
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


                var customer = connection.Query("sysUserInfoUpd", param, commandType: CommandType.StoredProcedure);
                var result = param.Get<Int64>("pret");

                
                myObject.result = result;
                return myObject;

            }
        }
        catch (Exception ex)
        {
            myObject.result = 99;
            myObject.exception = ex.ToString();
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


            var customer = connection.Query("sysUserInfoDel", param, commandType: CommandType.StoredProcedure);
            var result = param.Get<Int64>("pret");

            
            myObject.result = result;
            return myObject;

            }
        }
        catch (Exception ex)
        {
            myObject.result = 99;
            myObject.exception = ex.ToString();
        }
        return myObject;


    }

}
