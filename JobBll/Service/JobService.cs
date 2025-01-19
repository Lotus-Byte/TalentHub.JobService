using JobBll.Contracts.Command;
using JobBll.Contracts.Dto;
using JobBll.Contracts.Interface;

using JobDataAccess.Contracts.Repository;
using JobDataAccess.Contracts.Query;
using JobDataAccess.Entities;

using Microsoft.EntityFrameworkCore;

namespace JobBll.Service;

internal sealed class JobService : IJob
{
    private readonly IJobRepository _jobRepository;

    public JobService(IJobRepository jobRepository) =>
        _jobRepository = jobRepository;

    public async Task<JobDataDto> GetJobDataAsync(int id, CancellationToken cancellationToken = default)
    {
        // TODO: Нужны только неудаленные вакансии
        var entity = await _jobRepository.GetAsync(id, cancellationToken);

        // TODO: если объект не найден, сервис не должен падать
        var ret = new JobDataDto
        {
            Id = entity.Id,
            ObjectId = entity.ObjectId,
            Name = entity.Name,
            MinSalary = entity.MinSalary,
            MaxSalary = entity.MaxSalary,
            Responsibilities = entity.Responsibilities,
            Requirements = entity.Requirements,
            Conditions = entity.Conditions,
            CreateStamp = entity.Created,
        };

        return ret;
    }

    public async Task<CreateJobDto> CreateJobAsync(CreateJobCommand command, CancellationToken cancellationToken = default)
    {
        var entity = new Job
        {
            Name = command.Name,
            MinSalary = command.MinSalary,
            MaxSalary = command.MaxSalary,
            Responsibilities = command.Responsibilities,
            Requirements = command.Requirements,
            Conditions = command.Conditions,
            CreateUserId = command.CreateUserId,
        };

        var createResult = await _jobRepository.AddAsync(entity, cancellationToken);
        await _jobRepository.SaveChangesAsync(cancellationToken);

        var ret = new CreateJobDto
        {
            Id = createResult.Id,
            ObjectId = createResult.ObjectId,
        };
        return ret;
    }

    public Task UpdateAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveJobAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _jobRepository.GetAsync(id, cancellationToken);
        var deleted = _jobRepository.Delete(entity);

        if (deleted)
        {
            await _jobRepository.SaveChangesAsync(cancellationToken);  
        }
    }

    public async Task<IReadOnlyCollection<JobDataDto>> SearchAsync(SearchCommand command, CancellationToken cancellationToken = default)
    {
        var request = new SearchJobsQueryRequest
        {
            SearchText = command.SearchText,
            City = command.City,
            MinSalary = command.MinSalary,
            MaxSalary = command.MaxSalary,
        };

        var jobsQuery = _jobRepository.SearchJobsQuery(request)
            .Select(j => new JobDataDto
            {
                Id = j.Id,
                ObjectId = j.ObjectId,
                Name = j.Name,
                MinSalary = j.MinSalary,
                MaxSalary = j.MaxSalary,
                Responsibilities = j.Responsibilities,
                Requirements = j.Requirements,
                Conditions = j.Conditions,
                CreateStamp = j.Created,
            });

        var ret = await jobsQuery.ToArrayAsync(cancellationToken);

        return ret;
    }
}
