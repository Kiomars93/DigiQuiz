using DigiQuiz.Application.ApiServices.Requests;
using DigiQuiz.Application.ApiServices.Responses;
using DigiQuiz.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DigiQuiz.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DigimonController : ControllerBase
{
    private readonly IGetDigimonsServiceHandler _getDigimonsServiceHandler;
    private readonly IPostDigimonServiceHandler _postDigimonServiceHandler;
    public DigimonController(IGetDigimonsServiceHandler getDigimonsServiceHandler, IPostDigimonServiceHandler postDigimonServiceHandler)
    {
        _getDigimonsServiceHandler = getDigimonsServiceHandler;
        _postDigimonServiceHandler = postDigimonServiceHandler;
    }

    [HttpGet("Questions")]
    public async Task<ActionResult<GetDigimonsServiceResponse>> GetDigimons()
    {
        var response = await _getDigimonsServiceHandler.GetDigimonsHandler();

        var digimonsServiceReponse = response.Contents
            .Select(c => new GetDigimonsServiceResponse { Name = c.Name, Image = c.Image });

        return new OkObjectResult(digimonsServiceReponse);
    }

    [HttpPost("Answers")]
    public async Task<ActionResult<PostDigimonServiceResponse>> PostDigimon(PostDigimonServiceRequest serviceRequest)
    {
        var response = await _postDigimonServiceHandler.PostDigimonHandler(serviceRequest);
        var digimonsServiceReponse = new PostDigimonServiceResponse
        {
            Name = response.Name,
            Points = response.Points
        };

        return digimonsServiceReponse;
    }
}