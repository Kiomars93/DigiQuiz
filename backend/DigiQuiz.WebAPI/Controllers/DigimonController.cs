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

        var getDigimonsServiceReponse = response.Contents
            .Select(c => new GetDigimonsServiceResponse { Id = c.Id, Name = c.Name, Image = c.Image });

        return new OkObjectResult(getDigimonsServiceReponse);
    }

    [HttpPost("Scoreboard")]
    public async Task<ActionResult<PostPlayerServiceResponse>> PostPlayers(PostPlayerServiceRequest serviceRequest)
    {
        var response = await _sender.Send(new PostPlayerServiceCommand(serviceRequest));

        var playerServiceReponse = new PostPlayerServiceResponse
        {
            Name = response.Name,
            Points = response.Points
        };

        return playerServiceReponse;
    }

    [HttpGet("Leaderboard")]
    public async Task<ActionResult<List<GetPlayersServiceResponse>>> GetPlayers()
    {
        var response = await _sender.Send(new GetPlayersServiceQuery());

        var playerServiceReponse = response.Select(x => new GetPlayersServiceResponse
        {
            Id = x.Id,
            Name = x.Name,
            Points = x.Points,
            GameDate = x.GameDate
        }).ToList();

        return playerServiceReponse;
    }
}