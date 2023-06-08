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
        using (var connection = new SqlConnection(_connectionString))
        {


            var customer = connection.Query("UserLogin", param, commandType: CommandType.StoredProcedure);
            var result = param.Get<Int64>("pret");

            dynamic myObject = new ExpandoObject();
            myObject.result = result;
            return myObject;

        }

    }


    //public async Task<Product> GetProductById(int id)
    //{
    //    using (var connection = new SqlConnection(_connectionString))
    //    {
    //        await connection.OpenAsync();
    //        return await connection.QuerySingleOrDefaultAsync<Product>("SELECT * FROM Products WHERE Id = @Id", new { Id = id });
    //    }
    //}

    // Thêm các phương thức CRUD khác tùy ý
}
