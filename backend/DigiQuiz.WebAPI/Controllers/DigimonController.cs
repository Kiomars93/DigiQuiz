using DigiQuiz.Application.CQRS.Commands;
using DigiQuiz.Application.CQRS.Queries;
using DigiQuiz.Application.Requests;
using DigiQuiz.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DigiQuiz.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DigimonController : ControllerBase
{
    private readonly ISender _sender;
    public DigimonController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("Questions")]
    public async Task<ActionResult<List<GetDigimonsServiceResponse>>> GetDigimons()
    {
        var response = await _sender.Send(new GetDigimonsServiceQuery());

        if (response == null || response.Count == 0)
            return NotFound(response);

        return Ok(response);
    }

    [HttpPost("Scoreboard")]
    public async Task<ActionResult<PostPlayerServiceResponse>> PostPlayer(PostPlayerServiceRequest serviceRequest)
    {
        var response = await _sender.Send(new PostPlayerServiceCommand(serviceRequest));

        return Ok(response);
    }

    [HttpGet("Leaderboard")]
    public async Task<ActionResult<List<GetPlayersServiceResponse>>> GetPlayers()
    {
        var response = await _sender.Send(new GetPlayersServiceQuery());

        if (response == null || response.Count == 0)
            return NotFound(response);

        return Ok(response);
    }
}