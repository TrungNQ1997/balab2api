﻿//using BAWebLab2.LibCommon;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using BAWebLab2.Entity;
//using BAWebLab2.DTO;
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


    public List<User> GetAllUsers1(DynamicParameters param)
    {
        //var result = new StoreResultDTO<User>();

        var list = ExcuteStore("BAWebUserGetSysUserInfo", ref param);

        //result.list = list.ToList();
        //var count = param.Get<Int64>("pCount");

        //result.count = (int)count;
        
        //result.is_success = true;
        //result.is_error = false;
        return list.ToList();

    }




    ///// <summary>lấy danh sách user</summary>
    ///// <param name="param">list param để chạy store</param>
    ///// <returns>số lượng user, list lấy theo offset</returns>
    //public StoreResultDTO<User> GetAllUsers(DynamicParameters param)
    //{
    //     var result = new StoreResultDTO<User>();
    //    try
    //    {
    //        using (var connection = new SqlConnection(_connectionString))
    //        {
                 
    //            var resultsExcute = connection.QueryMultiple("BAWebUserGetSysUserInfo", param, commandType: CommandType.StoredProcedure);
                
    //            var list = resultsExcute.Read<User>().ToList();

    //            var count = param.Get<Int64>("pCount");
                 
    //            result.count = (int)count;
    //            result.list = list;
    //            result.is_success = true;
    //            result.is_error = false;
                 
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        result.is_error = true;
    //        result.is_success = false;
    //        result.message = ex.Message;
    //        LibCommon.WriteLog(ex.ToString());
    //    }
    //    return result;

    //}

    ///// <summary>kiểm tra đăng nhập hợp lệ</summary>
    ///// <param name="param">list param để chạy store</param>
    ///// <returns>trạng thái đăng nhập, thông tin user login</returns>
    //public StoreResultDTO<User> Login(DynamicParameters param)
    //{
    //    var result = new StoreResultDTO<User>();
         
    //    try
    //    {
    //        using (var connection = new SqlConnection(_connectionString))
    //        {
                 
    //            var userInfo = connection.Query<User>("BAWebUserLoginSysUserInfo", param, commandType: CommandType.StoredProcedure).AsList();
                 
    //            result.list = userInfo;
    //            var ret = param.Get<Int64>("pret");
    //            result.count = (int)ret;
    //            result.is_error = false;
                
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        result.is_error = true;
    //        result.message = ex.Message;
    //        LibCommon.WriteLog(ex.ToString());
    //    }
    //    return result;

    //}

    ///// <summary>kiểm tra đăng nhập và lấy quyền</summary>
    ///// <param name="param">list param để chạy store</param>
    ///// <returns>list quyền, token hợp lệ không, user có là admin không</returns>
    //public StoreResultDTO<UserRole> CheckLoginAndRole(DynamicParameters param)
    //{
    //    var result = new StoreResultDTO<UserRole>();
    //     try
    //    {
    //        using (var connection = new SqlConnection(_connectionString))
    //    {
                 
    //        var tables = connection.QueryMultiple("BAWebUserCheckTokenLoginAndGetRoleSysUserInfo", param, commandType: CommandType.StoredProcedure);
            
    //        var role = tables.Read<UserRole>().ToList();
    //            var is_admin = param.Get<Boolean>("pis_admin");
    //            var ret = param.Get<Int64>("pret");
    //            result.list = role;
    //            result.is_success = ret == 0? false: true;
    //            result.is_admin = is_admin;
    //            result.is_error = false;
             
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        result.is_error = true;
    //         result.message = ex.Message;
    //        LibCommon.WriteLog(ex.ToString());
    //    }
    //    return result;

    //}

    ///// <summary>lấy list quyền</summary>
    ///// <param name="param">list param để chạy store</param>
    ///// <returns>list quyền, user có là admin không</returns>
    //public StoreResultDTO<UserRole> GetRole(DynamicParameters param)
    //{
    //    var result = new StoreResultDTO<UserRole>();
    //     try
    //    {
    //        using (var connection = new SqlConnection(_connectionString))
    //        {
                 
    //            var resultSQL = connection.QueryMultiple("BAWebUserGetRoleSysUserInfo", param, commandType: CommandType.StoredProcedure);
                
    //            var role = resultSQL.Read<UserRole>().ToList();
    //            var is_admin = param.Get<Boolean>("pis_admin");

    //            result.is_admin= is_admin;
    //            result.list= role;
    //            result.is_error = false;
 
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        result.is_error = true;
    //        result.message = ex.Message;
    //        LibCommon.WriteLog(ex.ToString());
    //    }
    //    return result;

    //}

    ///// <summary>thêm user</summary>
    ///// <param name="param">list param để chạy store</param>
    ///// <returns>trạng thái thêm user</returns>
    //public StoreResultDTO<int> AddUser(DynamicParameters param)
    //{
    //    var result = new StoreResultDTO<int>();
    //     try
    //    {
    //        using (var connection = new SqlConnection(_connectionString))
    //        {
                 
    //            var resultSQL = connection.Query("BAWebUserInsertSysUserInfo", param, commandType: CommandType.StoredProcedure);
    //            var isError = param.Get<Int64>("pret");
    //             if(isError == 0)
    //                result.is_success = true;
    //             else 
    //                result.is_success= false;
    //             result.is_error = false;   
                  
    //        }
    //    } catch (Exception ex)
    //    {
    //          result.is_error = true;
    //        result.is_success = false;
    //        result.message = ex.Message;
    //        LibCommon.WriteLog(ex.ToString());
    //    }
    //    return result;

    //}

    ///// <summary>sửa user</summary>
    ///// <param name="param">list param để chạy store</param>
    ///// <returns>trạng thái sửa user</returns>
    //public StoreResultDTO<int> EditUser(DynamicParameters param)
    //{

    //    var myObject = new StoreResultDTO<int>();
    //    try
    //    {
    //        using (var connection = new SqlConnection(_connectionString))
    //        {
                 
    //            var table = connection.Query("BAWebUserUpdateSysUserInfo", param, commandType: CommandType.StoredProcedure);
    //            var result = param.Get<Int64>("pret");
    //            if (result == 0)
    //                myObject.is_success = true;
    //            else
    //                myObject.is_success = false;
    //            myObject.is_error = false;
               
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        myObject.is_success = false;
    //        myObject.is_error = true;
    //        myObject.message = ex.Message;
    //        LibCommon.WriteLog(ex.ToString());
    //    }
    //    return myObject;

    //}

    ///// <summary>đổi pass user đang đăng nhập</summary>
    ///// <param name="param">list param để chạy store</param>
    ///// <returns>kết quả đổi pass</returns>
    //public StoreResultDTO<int> ChangePass(DynamicParameters param)
    //{
    //    var myObject = new StoreResultDTO<int>();
    //    try
    //    {
    //        using (var connection = new SqlConnection(_connectionString))
    //        {
                 
    //            var table = connection.Query("BAWebUserUpdatePassSysUserInfo", param, commandType: CommandType.StoredProcedure);
    //            var result = param.Get<Int64>("pret");
    //            if (result == 0)
    //                myObject.is_success = true;
    //            else
    //                myObject.is_success = false;
    //            myObject.is_error = false;


    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        myObject.is_success = false;
    //        myObject.is_error = true;
    //        myObject.message = ex.Message;
    //        LibCommon.WriteLog(ex.ToString());
    //    }
    //    return myObject;

    //}


    ///// <summary>xóa 1 user</summary>
    ///// <param name="param">list param để chạy store</param>
    ///// <returns>trạng thái xóa</returns>
    //public StoreResultDTO<int> DeleteUser(DynamicParameters param)
    //{
    //    var myObject = new StoreResultDTO<int>();
    //    try
    //    {
    //        using (var connection = new SqlConnection(_connectionString))
    //    {
                 
    //        var table = connection.Query("BAWebUserDeleteSysUserInfo", param, commandType: CommandType.StoredProcedure);
    //        var result = param.Get<Int64>("pret");
    //            myObject.list.Add((int)result);
    //            if (result == 0)
    //                myObject.is_success = true;
    //            else
    //                myObject.is_success = false;
    //            myObject.is_error = false;

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        myObject.is_error = true;
    //        myObject.message = ex.Message;
    //        LibCommon.WriteLog(ex.ToString());
    //    }
    //    return myObject;
         
    //}

}
