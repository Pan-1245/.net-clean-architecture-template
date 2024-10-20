using CleanArchitecture.Master.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Infrastructure.Database;
using CleanArchitecture.Interfaces.Persistence.Master;
using CleanArchitecture.Shared.Domain.Enum;
using CleanArchitecture.Master.Dto.SearchFilter;
using ServiceStack;

namespace CleanArchitecture.Persistence.Repositories.Master;

public class SubjectRepository : ISubjectRepository
{
    private readonly ApplicationDatabaseContext _databaseContext;

    public SubjectRepository(ApplicationDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext ?? throw new ArgumentNullException(nameof(databaseContext));
    }

    #region Queries

    public async Task<IEnumerable<Subject>> GetAllAsync(object? obj = null)
    {
        IQueryable<Subject>? query = null;

        if (obj is not null && obj is SearchSubjectCriteriaDto criteria)
        {
            query = GenerateSearchQuery(criteria);
        }
        else
        {
            query = GenerateSearchQuery();
        }

        var subjects = await query.ToListAsync();

        return subjects;
    }

    public async Task<Subject?> GetAsync(Guid id)
    {
        var subject = await _databaseContext.Subjects.AsNoTracking()
                                                     .SingleOrDefaultAsync(s => s.Id == id);

        return subject;
    }

    public async Task<int> CountAsync()
    {
        var count = await _databaseContext.Subjects.AsNoTracking()
                                                  .CountAsync();

        return count;
    }

    public async Task<IEnumerable<Subject>> GetPaginationAsync(int pageNumber, int pageSize, object? obj = null)
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

        IQueryable<Subject>? query = null;

        if (obj is not null && obj is SearchSubjectCriteriaDto criteria)
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

    public async Task<bool> InsertAsync(Subject entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity), "The subject entity cannot be null.");
        }

        await _databaseContext.Subjects.AddAsync(entity);

        var result = await _databaseContext.SaveChangesAsync();

        return result > 0;
    }

    public async Task<bool> UpdateAsync(Subject entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity), "The subject entity cannot be null.");
        }

        _databaseContext.Subjects.Attach(entity);
        _databaseContext.Entry(entity).State = EntityState.Modified;

        var result = await _databaseContext.SaveChangesAsync();

        return result > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var subject = await _databaseContext.Subjects.AsNoTracking()
                                                     .SingleOrDefaultAsync(s => s.Id == id);

        if (subject is null)
        {
            return false;
        }

        _databaseContext.Subjects.Remove(subject);

        var result = await _databaseContext.SaveChangesAsync();

        return result > 0;
    }

    #endregion

    private IQueryable<Subject> GenerateSearchQuery(SearchSubjectCriteriaDto? criteria = null)
    {
        var query = _databaseContext.Subjects.AsNoTracking()
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