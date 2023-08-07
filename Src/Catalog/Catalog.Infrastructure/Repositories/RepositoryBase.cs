using System.Collections.Immutable;
using System.Linq.Expressions;
using Catalog.Application.Contracts.Persistence;
using Catalog.Domain.Common;
using Catalog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Repositories;

public class RepositoryBase<T> : IAsyncRepository<T> where T : EntityBase
{
    protected readonly IDbContextFactory<CatalogContext> CatalogContextFactory;

    protected RepositoryBase(IDbContextFactory<CatalogContext> catalogContextFactory)
    {
        CatalogContextFactory = catalogContextFactory;
    }


    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        await using var db = await CatalogContextFactory.CreateDbContextAsync();
        return await db.Set<T>().ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
    {
        await using var db = await CatalogContextFactory.CreateDbContextAsync();
        return await db.Set<T>().Where(predicate).ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeString = null,
        bool disableTracking = true)
    {
        await using var db = await CatalogContextFactory.CreateDbContextAsync();
        
        IQueryable<T> query = db.Set<T>();
        
        if (disableTracking)
        {
            query = query.AsNoTracking();
        }

        if (!string.IsNullOrWhiteSpace(includeString))
        {
            query = query.Include(includeString);
        }

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (orderBy != null)
        {
            return await orderBy(query).ToListAsync();
        }

        return await query.ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, List<Expression<Func<T, object>>>? includes = null, bool disableTracking = true)
    {
        await using var db = await CatalogContextFactory.CreateDbContextAsync();
        
        IQueryable<T> query = db.Set<T>();

        if (disableTracking)
        {
            query = query.AsNoTracking();
        }

        if (includes != null)
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }
        
        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (orderBy != null)
        {
            return await orderBy(query).ToListAsync();
        }

        return await query.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        await using var db = await CatalogContextFactory.CreateDbContextAsync();
        return await db.Set<T>().FindAsync(id);
    }

    public async Task<T> AddAsync(T entity)
    {
        await using var db = await CatalogContextFactory.CreateDbContextAsync();
        await db.Set<T>().AddAsync(entity);
        await db.SaveChangesAsync();

        return entity;
    }

    public async Task<IReadOnlyList<Guid>> AddBulkAsync(IEnumerable<T> entities)
    {
        var entityBases = entities.ToList();
        await using var db = await CatalogContextFactory.CreateDbContextAsync();
        await db.Set<T>().AddRangeAsync(entityBases);
        await db.SaveChangesAsync();

        return entityBases.Select(e => e.Id).ToImmutableList();
    }

    public Task UpdateAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(T entity)
    {
        throw new NotImplementedException();
    }
}