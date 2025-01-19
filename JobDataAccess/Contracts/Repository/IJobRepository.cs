using JobDataAccess.Contracts.Query;
using JobDataAccess.Entities;

namespace JobDataAccess.Contracts.Repository;

public interface IJobRepository : IRepository<Job, long>
{
    IQueryable<Job> SearchJobsQuery(SearchJobsQueryRequest request);

    Task<Job?> GetAsync(Guid objectId, CancellationToken cancellationToken);
}
