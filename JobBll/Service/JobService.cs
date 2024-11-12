using JobBll.Contracts.Command;
using JobBll.Contracts.Dto;
using JobBll.Contracts.Interface;
using JobDataAccess.Contracts.Repository;
using JobDataAccess.Entities;

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
            Created = entity.Created,
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

        // TODO: подумать, как возвращать результат
    }
}
