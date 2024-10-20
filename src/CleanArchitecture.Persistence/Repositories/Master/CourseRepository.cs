using CleanArchitecture.Master.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Infrastructure.Database;
using CleanArchitecture.Interfaces.Persistence.Master;
using CleanArchitecture.Shared.Domain.Enum;
using CleanArchitecture.Master.Dto.SearchFilter;
using ServiceStack;

namespace CleanArchitecture.Persistence.Repositories.Master;

public class CourseRepository : ICourseRepository
{
    private readonly ApplicationDatabaseContext _databaseContext;

    public CourseRepository(ApplicationDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext ?? throw new ArgumentNullException(nameof(databaseContext));
    }

    #region Queries

    public async Task<IEnumerable<Course>> GetAllAsync(object? obj = null)
    {
        IQueryable<Course>? query = null;

        if (obj is not null && obj is SearchCourseCriteriaDto criteria)
        {
            query = GenerateSearchQuery(criteria);
        }
        else
        {
            query = GenerateSearchQuery();
        }

        var courses = await query.ToListAsync();

        return courses;
    }

    public async Task<Course?> GetAsync(Guid id)
    {
        var course = await _databaseContext.Courses.AsNoTracking()
                                                   .SingleOrDefaultAsync(c => c.Id == id);

        return course;
    }

    public async Task<int> CountAsync()
    {
        var count = await _databaseContext.Courses.AsNoTracking()
                                                  .CountAsync();

        return count;
    }

    public async Task<IEnumerable<Course>> GetPaginationAsync(int pageNumber, int pageSize, object? obj = null)
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

        IQueryable<Course>? query = null;

        if (obj is not null && obj is SearchCourseCriteriaDto criteria)
        {
            query = GenerateSearchQuery(criteria);
        }
        else
        {
            query = GenerateSearchQuery();
        }

        var courses = await query.Skip(skip)
                                 .Take(pageSize)
                                 .ToListAsync();

        return courses;
    }

    #endregion
    #region Commands

    public async Task<bool> InsertAsync(Course entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity), "The course entity cannot be null.");
        }

        await _databaseContext.Courses.AddAsync(entity);

        var result = await _databaseContext.SaveChangesAsync();

        return result > 0;
    }

    public async Task<bool> UpdateAsync(Course entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity), "The course entity cannot be null.");
        }

        _databaseContext.Courses.Attach(entity);
        _databaseContext.Entry(entity).State = EntityState.Modified;

        var result = await _databaseContext.SaveChangesAsync();

        return result > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var course = await _databaseContext.Courses.AsNoTracking()
                                                   .SingleOrDefaultAsync(c => c.Id == id);

        if (course is null)
        {
            return false;
        }

        _databaseContext.Courses.Remove(course);

        var result = await _databaseContext.SaveChangesAsync();

        return result > 0;
    }

    #endregion

    private IQueryable<Course> GenerateSearchQuery(SearchCourseCriteriaDto? criteria = null)
    {
        var query = _databaseContext.Courses.AsNoTracking()
                                            .AsQueryable();

        if (criteria is not null)
        {
            if (!string.IsNullOrEmpty(criteria.Name))
            {
                query = query.Where(c => c.Name.Contains(criteria.Name));
            }

            if (criteria.IsActive.HasValue)
            {
                query = query.Where(c => c.IsActive == criteria.IsActive);
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
                    query = query.OrderBy(c => c.UpdatedAt);
                }
            }
        }

        query = query.OrderBy(c => c.UpdatedAt);

        return query;
    }
}