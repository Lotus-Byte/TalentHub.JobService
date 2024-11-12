using JobBll.Contracts.Command;
using JobBll.Contracts.Interface;
using JobApi.Contracts;
using Microsoft.AspNetCore.Mvc;
using JobBll.Contracts.Dto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobApi.Controllers;

/// <summary>
/// Сервис для работы с вакансиями
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class JobsController : ControllerBase
{
    private readonly IJob _jobService;

    public JobsController(IJob jobService) =>
        _jobService = jobService;

    /// <summary>
    /// Поиск вакансий 
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    /// <example>POST: api/<JobsController></example>
    [HttpPost("search")]
    public Task<IReadOnlyCollection<JobDataDto>> Search(SearchCommand command, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Информация о вакансии по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    /// <example>GET api/<JobsController>/5</example>
    [HttpGet("{id}")]
    public async Task<JobDataDto> Get(int id, CancellationToken cancellationToken = default)
    {
        return await _jobService.GetJobDataAsync(id, cancellationToken);
    }

    /// <summary>
    /// Создание вакансии
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <example>POST api/<JobsController></example>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateJobCommand command, CancellationToken cancellationToken = default)
    {
        var result = await _jobService.CreateJobAsync(command, cancellationToken);

        var ret = new CreatedResult();
        ret.Value = result;

        return ret;
    }

    /// <summary>
    /// Редактирование вакансии
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <example>PUT api/<JobsController>/5</example>
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] EditJobCommand command, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Удаление вакансии
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <example>DELETE api/<JobsController>/5</example>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
    {
        await _jobService.RemoveJobAsync(id, cancellationToken);
        
        return new NoContentResult();
    }
}
