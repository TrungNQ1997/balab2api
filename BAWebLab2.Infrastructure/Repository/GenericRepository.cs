using BAWebLab2.Infrastructure.DataContext;
using BAWebLab2.Infrastructure.Repository.IRepository;
using BAWebLab2.DTO;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;
using static Dapper.SqlMapper;
using Microsoft.Data.SqlClient;
using BAWebLab2.Entity;

namespace BAWebLab2.Repository
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
       // public MultipleResultDTO<T1> GetStoredProcedure<T1>(string storedProcedureName, ref List<SqlParameter> input, ref List<SqlParameter> output)
       // {
            
       //     var t = _context.Set<T1>().FromSqlRaw("EXEC GetUserInfo @userId, @username, @email OUTPUT",
       //input,output).ToList() ;


       //     var result = _context.Set<T1>().FromSqlInterpolated($"SELECT * FROM {storedProcedureName}").FirstOrDefault();


       //     return new MultipleResultDTO<T1> { ListPrimary = result };
       // }

        }
}
