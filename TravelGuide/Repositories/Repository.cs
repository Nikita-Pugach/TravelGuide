using Microsoft.EntityFrameworkCore;
using TravelGuide.Data;
using TravelGuide.Services.Interfaces;

namespace TravelGuide.Repositories;

/// <summary>
/// Универсальный репозиторий для работы с БД
/// </summary>
public class Repository<T> : IRepository<T> where T : class
{
    protected readonly TravelGuideContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(TravelGuideContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}