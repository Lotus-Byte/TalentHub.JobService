using JobDataAccess.Context;
using JobDataAccess.Contracts.Query;
using JobDataAccess.Contracts.Repository;
using JobDataAccess.Entities;

using Microsoft.EntityFrameworkCore;

namespace JobDataAccess.Repository;

internal sealed class JobRepository : Repository<Job, long>, IJobRepository
{
    public JobRepository(JobDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Получить сущность по Id.
    /// </summary>
    /// <param name="id"> Id сущности. </param>
    /// <param name="cancellationToken"> Токен отмены. </param>
    /// <returns> Cущность. </returns>
    public override async Task<Job?> GetAsync(long id, CancellationToken cancellationToken)
    {
        return await Context.Set<Job>()
            .Where(j => j.Id == id)
            .Where(j => j.Deleted == false)
            .FirstOrDefaultAsync(cancellationToken);
    }

    /// <summary>
    /// Получить сущность по ObjectId.
    /// </summary>
    /// <param name="objectId"> Бизнес-ключ сущности. </param>
    /// <param name="cancellationToken"> Токен отмены. </param>
    /// <returns> Cущность. </returns>
    public async Task<Job?> GetAsync(Guid objectId, CancellationToken cancellationToken)
    {
        return await Context.Set<Job>()
            .Where(j => j.ObjectId == objectId)
            .Where(j => j.Deleted == false)
            .FirstOrDefaultAsync(cancellationToken);
    }

    /// <summary>
    /// Пометить сущность удаленной.
    /// </summary>
    /// <param name="entity"> Сущность для удаления. </param>
    /// <returns> Была ли сущность удалена. </returns>
    public override bool Delete(Job? entity)
    {
        if (entity == null)
        {
            return false;
        }

        entity.Deleted = true;
        
        return true;
    }

    public IQueryable<Job> SearchJobsQuery(SearchJobsQueryRequest request)
    {
        var commonSearchQuery = Context.Set<Job>()
            .Where(j => j.Deleted == false);

        if (request.MinSalary != null)
        {
            commonSearchQuery = commonSearchQuery
                .Where(j => j.MinSalary == request.MinSalary);
        }

        if (request.MaxSalary != null)
        {
            commonSearchQuery = commonSearchQuery
                .Where(j => j.MaxSalary == request.MaxSalary);
        }

        var searchByName = commonSearchQuery
            .Where(j => request.SearchText.Contains(j.Name));

        var searchByResp = commonSearchQuery
            .Where(p => p.ResponsibilitiesVector.Matches(EF.Functions.ToTsQuery(request.SearchText)));

        var searchByCond = commonSearchQuery
            .Where(p => p.ConditionsVector.Matches(EF.Functions.ToTsQuery(request.SearchText)));

        commonSearchQuery = searchByName
            .Union(searchByResp)
            .Union(searchByCond);

        return commonSearchQuery;
    }
}
