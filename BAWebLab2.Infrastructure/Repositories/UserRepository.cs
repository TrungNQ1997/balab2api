﻿
using Dapper;
using System.Data;
using BAWebLab2.Entities;
using BAWebLab2.Model;
using BAWebLab2.Repository;
using BAWebLab2.Infrastructure.Repository.IRepository;
using BAWebLab2.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;


/// <summary>class xử lí thao tác của phân hệ user với tầng Database</summary>
/// <Modified>
/// Name Date Comments
/// trungnq3 7/12/2023 created
/// </Modified>
public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly BADbContext _bADbContext;
    public UserRepository(BADbContext bADbContext)
        : base(bADbContext)
    {
        _bADbContext = bADbContext;
    }

   

    /// <summary>xóa danh sách user</summary>
    /// <param name="input">đối tượng chứa danh sách user cần xóa, id user thực hiện xóa</param>
    /// <returns>chuỗi kết quả xóa: 0- thành công, khác 0- thất bại</returns>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/12/2023 created
    /// </Modified>
    public StoreResult<int> DeleteUser(InputDelete input)
    {

        var myObject = new StoreResult<int>();
        using (var connection = _context.Database.GetDbConnection())
        {
            string userId = input.UserId;

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


    /// <summary>thêm mới đối tượng</summary>
    /// <param name="user">user muốn thêm</param>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/12/2023 created
    /// </Modified>
    public new void Add(User user)
    {
        user.DateCreated = DateTime.Now;
        user.DateEdited = DateTime.Now;
        base.Add(user);
    }

    /// <summary>sửa đối tượng</summary>
    /// <param name="user">user đã sửa</param>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/12/2023 created
    /// </Modified>
    public new void Update(User user)
    {

        user.DateEdited = DateTime.Now;
        base.Update(user);
    }

    /// <summary>lấy list user từ thủ tục</summary>
    /// <param name="procedure">tên thủ tục</param>
    /// <param name="input">danh sách tham số truyền vào</param>
    /// <returns>list user</returns>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/12/2023 created
    /// </Modified>
    public MultipleResult<LoginResult> GetListUserProcedure(string procedure, ref List<SqlParameter> input)
    {

        var u = new SqlParameter();
        string sql = "EXEC " + procedure;
        for (int i = 0; i < input.Count; i++)
        {
            if (i == 0)
            {
                sql += (" " + input[i].ParameterName + " ");
            }
            else
            {
                sql += (" , " + input[i].ParameterName + " ");
            }
            if (input[i].Direction == ParameterDirection.Output)
            {
                sql += (" output ");
            }
        }

        var t = _context.Set<LoginResult>().FromSqlRaw(sql,
          input.ToArray()).ToList();

        MultipleResult<LoginResult> resultDTO = new MultipleResult<LoginResult>();
        resultDTO.ListPrimary = t;

        return resultDTO;
    }

}
