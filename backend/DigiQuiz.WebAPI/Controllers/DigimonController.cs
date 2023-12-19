using DigiQuiz.Application.Commands;
using DigiQuiz.Application.Queries;
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
    public async Task<ActionResult<GetDigimonsServiceResponse>> GetDigimons()
    {
        var response = await _sender.Send(new GetDigimonsServiceQuery());
        return new OkObjectResult(response);
    }

    [HttpPost("Scoreboard")]
    public async Task<ActionResult<PostPlayerServiceResponse>> PostPlayers(PostPlayerServiceRequest serviceRequest)
    {
        var response = await _sender.Send(new PostPlayerServiceCommand(serviceRequest));

        return response;
    }

    [HttpGet("Leaderboard")]
    public async Task<ActionResult<List<GetPlayersServiceResponse>>> GetPlayers()
    {
        var response = await _sender.Send(new GetPlayersServiceQuery());

        return response;
    }
}