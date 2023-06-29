using BAWebLab2.DAL.DataContext;
using BAWebLab2.DAL.Repository.IRepository;
using BAWebLab2.DTO.DTO;
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
using static Dapper.SqlMapper;

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
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
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
 
        public MultipleResultDTO<T1> CallStoredProcedure<T1>(string storedProcedureName, ref DynamicParameters param)
        {
            using var connection = _context.Database.GetDbConnection();
            connection.Open();

            var multi = connection.QueryMultiple(storedProcedureName, param , commandType: CommandType.StoredProcedure);
            
                var resultList = multi.Read<T1>().ToList();
                 connection.Close();
                return new MultipleResultDTO<T1> { ListPrimary = resultList  };
            
        }

    }
}
