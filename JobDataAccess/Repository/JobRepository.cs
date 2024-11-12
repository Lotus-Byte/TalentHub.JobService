using JobDataAccess.Context;
using JobDataAccess.Contracts.Repository;
using JobDataAccess.Entities;

namespace JobDataAccess.Repository;

internal sealed class JobRepository : Repository<Job, long>, IJobRepository
{
    public JobRepository(JobDbContext context) : base(context)
    {
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
}
