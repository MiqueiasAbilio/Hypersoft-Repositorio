using Hypesoft.Application.DTOs;
using Hypesoft.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Hypesoft.API.Controllers;


/// Controller para dados agregados do dashboard.

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DashboardController : ControllerBase
{
    private readonly IMediator _mediator;


    /// Inicializa uma nova instância de <see cref="DashboardController"/>.

    public DashboardController(IMediator mediator) => _mediator = mediator;

    /// Retorna os dados consolidados do dashboard.

    [HttpGet]
    [SwaggerOperation(Summary = "Retorna os dados consolidados do dashboard")]
    public async Task<ActionResult<DashboardResponse>> GetSummary()
    {
        var result = await _mediator.Send(new GetDashboardSummaryQuery());
        return Ok(result);
    }
}