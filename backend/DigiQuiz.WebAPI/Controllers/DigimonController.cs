﻿using DigiQuiz.Application.Commands;
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

    [HttpPost("Answers")]
    public async Task<ActionResult<PostDigimonServiceResponse>> PostDigimons(PostDigimonServiceRequest serviceRequest)
    {
        var response = await _sender.Send(new PostDigimonServiceCommand(serviceRequest));

        var playerServiceReponse = new PostDigimonServiceResponse
        {
            Name = response.Name,
            Points = response.Points
        };

        return playerServiceReponse;
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
}