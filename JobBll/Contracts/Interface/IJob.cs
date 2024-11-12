using JobBll.Contracts.Command;
using JobBll.Contracts.Dto;

namespace JobBll.Contracts.Interface;

public interface IJob
{
    Task<JobDataDto> GetJobDataAsync(int id, CancellationToken cancellationToken = default);

    Task<CreateJobDto> CreateJobAsync(CreateJobCommand command, CancellationToken cancellationToken = default);

    Task UpdateAsync(CancellationToken cancellationToken = default);

    Task RemoveJobAsync(int id, CancellationToken cancellationToken = default);
}
