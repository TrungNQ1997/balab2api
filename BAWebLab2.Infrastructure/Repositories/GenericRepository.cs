﻿using BAWebLab2.Infrastructure.DataContext;
using BAWebLab2.Infrastructure.Repository.IRepository;
using BAWebLab2.Model;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;
using static Dapper.SqlMapper;

namespace BAWebLab2.Repository
{
    /// <summary>class implement của IGenericRepository</summary>
    /// <typeparam name="T">Kiểu đối tượng truyền vào</typeparam>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/12/2023 created
    /// </Modified>
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly BADbContext _context;
        public GenericRepository(BADbContext context)
        {
            _context = context;
        }

        /// <summary>thêm mới đối tượng</summary>
        /// <param name="entity">đối tượng muốn thêm</param>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        /// <summary>sửa đối tượng</summary>
        /// <param name="entity">đối tượng muốn sửa</param>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }

        /// <summary>thêm nhiều đối tượng</summary>
        /// <param name="entities">danh sách đối tượng</param>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        /// <summary>tìm theo điều kiện</summary>
        /// <param name="expression">điều kiện</param>
        /// <returns>list thỏa mãn điều kiện</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }

        /// <summary>lấy tất cả đối tượng</summary>
        /// <returns>danh sách đối tượng</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        /// <summary>Get đối tượng bằng id</summary>
        /// <param name="id">id đối tượng</param>
        /// <returns>đối tượng muốn lấy</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        /// <summary>xóa đối tượng</summary>
        /// <param name="entity">đối tượng cần xóa</param>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        /// <summary>xóa nhiều đối tượng</summary>
        /// <param name="entities">danh sách đối tượng</param>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        /// <summary>gọi thủ tục</summary>
        /// <typeparam name="T1">kiểu đối tượng trả về</typeparam>
        /// <param name="storedProcedureName">tên store</param>
        /// <param name="param">list tham số store</param>
        /// <returns>đối tượng chứa danh sách trả về</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public MultipleResult<T1> CallStoredProcedure<T1>(string storedProcedureName, ref DynamicParameters param)
        {
            using var connection = _context.Database.GetDbConnection();
            connection.Open();

            var multi = connection.QueryMultiple(storedProcedureName, param , commandType: CommandType.StoredProcedure);
            
                var resultList = multi.Read<T1>().ToList();
                 connection.Close();
                return new MultipleResult<T1> { ListPrimary = resultList  };
            
        }
      

        }
}