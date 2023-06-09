﻿using BAWebLab2.DTO;
using BAWebLab2.Entity;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAWebLab2.DAL.Repository.IRepository
{
    public interface   IUserRepository : IGenericRepository<User>
    {

        /// <summary>xóa danh sách user</summary>
        /// <param name="input">đối tượng chứa danh sách user cần xóa, id user thực hiện xóa</param>
        /// <returns>chuỗi kết quả xóa: 0- thành công, khác 0- thất bại</returns>
        public StoreResultDTO<int> DeleteUser(InputDeleteDTO input);
    }
}
