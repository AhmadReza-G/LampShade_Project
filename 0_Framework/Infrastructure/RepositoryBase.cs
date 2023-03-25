using _0_Framework.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _0_Framework.Infrastructure;
public class RepositoryBase<TKey, T> : IRepository<TKey, T> where T : class
{
    private readonly DbContext _context;

    public RepositoryBase(DbContext context)
    {
        _context = context;
    }

    public void Create(T entity)
    {
        _context.Add(entity);
    }

    public List<T> GetAll()
    {
        return _context.Set<T>().ToList();
    }

    public T? GetBy(TKey id)
    {
        return _context.Find<T>(id);
    }

    public bool IsExists(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().Any(expression);
    }

    public void SaveChanges()  
    {
        _context.SaveChanges();
    }
}
