﻿
using Dapper;
using System.Data;
using BAWebLab2.Entity;

using BAWebLab2.DTO;
using BAWebLab2.DAL.Repository;
using BAWebLab2.DAL.Repository.IRepository;
using BAWebLab2.DAL.DataContext;
using Microsoft.EntityFrameworkCore;

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
                string  user_id = input.user_id;
                 
                foreach (var item in input.users)
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("pid", (item.id));
                    parameters.Add("pusername", (item.username));

                    parameters.Add("puser_id", (user_id));

                    parameters.Add("pret", 0, DbType.Int64, ParameterDirection.Output);

                connection.Query("BAWebUserDeleteSysUserInfo", parameters, commandType: CommandType.StoredProcedure);
                var result = parameters.Get<Int64>("pret");
                myObject.list.Add((int)result);
 
            }
            
        }
          
        return myObject;

    }

}
