using CleanArchitecture.Master.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Infrastructure.Database;
using CleanArchitecture.Interfaces.Persistence.Master;
using CleanArchitecture.Shared.Domain.Enum;
using CleanArchitecture.Master.Dto.SearchFilter;
using ServiceStack;

namespace CleanArchitecture.Persistence.Repositories.Master;

public class LevelRepository : ILevelRepository
{
    private readonly ApplicationDatabaseContext _databaseContext;

    public LevelRepository(ApplicationDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext ?? throw new ArgumentNullException(nameof(databaseContext));
    }

    #region Queries

    public async Task<IEnumerable<Level>> GetAllAsync(object? obj = null)
    {
        IQueryable<Level>? query = null;

        if (obj is null && obj is SearchLevelCriteriaDto criteria)
        {
            query = GenerateSearchQuery(criteria);
        }
        else
        {
            query = GenerateSearchQuery();
        }

        var levels = await query.ToListAsync();

        return levels;
    }

    public async Task<Level?> GetAsync(Guid id)
    {
        var level = await _databaseContext.Levels.AsNoTracking()
                                                 .SingleOrDefaultAsync(l => l.Id == id);

        return level;
    }

    public async Task<int> CountAsync()
    {
        var count = await _databaseContext.Levels.AsNoTracking()
                                                 .CountAsync();

        return count;
    }

    public async Task<IEnumerable<Level>> GetPaginationAsync(int pageNumber, int pageSize, object? obj = null)
    {
        if (pageNumber <= 0)
        {
            throw new ArgumentException("Page number must be greater than 0.", nameof(pageNumber));
        }

        if (pageSize <= 0)
        {
            throw new ArgumentException("Page size must be greater than 0.", nameof(pageSize));
        }

        int skip = (pageNumber - 1) * pageSize;

        IQueryable<Level>? query = null;

        if (obj is not null && obj is SearchLevelCriteriaDto criteria)
        {
            query = GenerateSearchQuery(criteria);
        }
        else
        {
            query = GenerateSearchQuery();
        }

        var subjects = await query.Skip(skip)
                                  .Take(pageSize)
                                  .ToListAsync();

        return subjects;
    }

    #endregion
    #region Commands

    public async Task<bool> InsertAsync(Level entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity), "The level entity cannot be null.");
        }

        await _databaseContext.Levels.AddAsync(entity);

        var result = await _databaseContext.SaveChangesAsync();

        return result > 0;
    }

    public async Task<bool> UpdateAsync(Level entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity), "The level entity cannot be null.");
        }

        _databaseContext.Levels.Attach(entity);
        _databaseContext.Entry(entity).State = EntityState.Modified;

        var result = await _databaseContext.SaveChangesAsync();

        return result > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var level = await _databaseContext.Levels.AsNoTracking()
                                                 .SingleOrDefaultAsync(l => l.Id == id);

        if (level is null)
        {
            return false;
        }

        _databaseContext.Levels.Remove(level);

        var result = await _databaseContext.SaveChangesAsync();

        return result > 0;
    }

    #endregion

    private IQueryable<Level> GenerateSearchQuery(SearchLevelCriteriaDto? criteria = null)
    {
        var query = _databaseContext.Levels.AsNoTracking()
                                             .AsQueryable();

        if (criteria is not null)
        {
            if (criteria.CourseId.HasValue)
            {
                query = query.Where(s => s.CourseId == criteria.CourseId);
            }

            if (!string.IsNullOrEmpty(criteria.Name))
            {
                query = query.Where(s => s.IsActive == criteria.IsActive);
            }

            if (criteria.IsActive.HasValue)
            {
                query = query.Where(s => s.IsActive == criteria.IsActive);
            }

            if (!string.IsNullOrEmpty(criteria.SortBy))
            {
                string sorting = $"{criteria.SortBy} {(criteria.OrderBy == SortingOrder.DESC ? "descending" : "ascending")}";

                try
                {
                    query = query.OrderBy(sorting);
                }
                catch (Exception)
                {
                    query = query.OrderBy(s => s.UpdatedAt);
                }
            }
        }

        query = query.OrderBy(s => s.UpdatedAt);

        return query;
    }
}