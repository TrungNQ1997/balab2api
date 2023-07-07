
using Dapper;
using System.Data;
using BAWebLab2.Entity;

using BAWebLab2.DTO;
using BAWebLab2.Repository;
using BAWebLab2.Infrastructure.Repository.IRepository;
using BAWebLab2.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using BAWebLab2.Infrastructure.DTO;

public class UserRepository : GenericRepository<User>,IUserRepository
{
    private readonly BADbContext _bADbContext;
    public UserRepository(BADbContext bADbContext) 
        : base(bADbContext)
    {
        _bADbContext = bADbContext;
    }
  public StoreResultDTO<int> DeleteUser(InputDeleteDTO input)
    {
         
        var myObject = new StoreResultDTO<int>();
        using (var connection = _context.Database.GetDbConnection())
        {
                string  userId = input.UserId;
                 
                foreach (var item in input.Users)
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("id", (item.Id));
                    parameters.Add("username", (item.Username));

                    parameters.Add("userId", (userId));

                    parameters.Add("ret", 0, DbType.Int64, ParameterDirection.Output);

                connection.Query("BAWeb_User_DeleteUserInfo", parameters, commandType: CommandType.StoredProcedure);
                var result = parameters.Get<Int64>("ret");
                myObject.List.Add((int)result);
 
            }
            
        }
          
        return myObject;

    }

    public MultipleResultDTO<LoginResultDTO> GetListUserProcedure(string procedure, ref List<SqlParameter> input, ref List<SqlParameter> output)
    {

        var u = new SqlParameter();
        string sql = "EXEC " + procedure;
        for(int i = 0; i < input.Count; i++)
        {
            if (i == 0)
            {
                sql += (" " + input[i].ParameterName + " ");
            } else
            {
                sql += (" , " + input[i].ParameterName + " ");
            }
            if (input[i].Direction == ParameterDirection.Output)
            {
                sql += (" output ");
            }
        }
        //for (int i = 0; i < output.Count; i++)
        //{
        //    if (i == 0 && input.Count == 0)
        //    {
        //        sql += (" " + output[i].ParameterName + " ");
        //    }
        //    else
        //    {
        //        sql += (" , " + output[i].ParameterName + " OUTPUT ");
        //    }
        //}
        var t = _context.Set<LoginResultDTO>().FromSqlRaw(sql,
          input.ToArray()).ToList();

        MultipleResultDTO<LoginResultDTO> resultDTO = new MultipleResultDTO<LoginResultDTO>();
        resultDTO.ListPrimary = t;



         

        return resultDTO;
    }




    }
