using JobDataAccess.Entities;

namespace JobDataAccess.Contracts.Repository;

public interface IJobRepository : IRepository<Job, long>
{ 
}
