using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class Repo<TEntity>(DataContext context) where TEntity : class
{
    private readonly DataContext _context = context;


    public async Task<TEntity> CreateOneAsync(TEntity entity)
    {
        try
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return null!;


    }

    public async Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var result = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
            if (result != null)
            {
                return result;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return null!;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var result = await _context.Set<TEntity>().Where(predicate).ToListAsync();
            if (result != null)
            {
                return result;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return null!;
    }



    public async Task<TEntity> UpdateOneAsync(TEntity entity)
    {
        try
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return null!;
    }


    public async Task<bool> AlreadyExistsAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var result = await _context.Set<TEntity>().AnyAsync(predicate);
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return false;
    }

    public async Task<bool> DeleteOneFindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return false;
    }

    public async Task<bool> DeleteOneAsync(TEntity entity)
    {
        try
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return false;
    }

    public async Task<bool> DeleteAllAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var entities = await _context.Set<TEntity>().Where(predicate).ToListAsync();
            if (entities != null)
            {
                _context.Set<TEntity>().RemoveRange(entities);
                await _context.SaveChangesAsync();
                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return false;
    }
}
