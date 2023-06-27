using BAWebLab2.DAL.DataContext;
using BAWebLab2.DAL.Repository.IRepository;
using BAWebLab2.Entity;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BAWebLab2.DAL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly BADbContext _context;
        public GenericRepository(BADbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public IEnumerable<T> ExcuteStore(string procedureName , ref DynamicParameters param)
        {

            //using var connection = _context.Database.GetDbConnection();
            using var connection = _context.Database.GetDbConnection();

            connection.Open();

            //string query = "SELECT * FROM Employees";

           var resultsExcute =  connection.QueryMultiple(procedureName, param, commandType: CommandType.StoredProcedure);

            var list = resultsExcute.Read<T>().ToList();

            return list;
        }

    }
}
