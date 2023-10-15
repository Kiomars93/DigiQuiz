using DigiQuiz.Application.ApiServices.Responses;
using DigiQuiz.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DigiQuiz.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DigimonController : ControllerBase
{
    private readonly IGetDigimonsServiceHandler _getDigimonsServiceHandler;
    public DigimonController(IGetDigimonsServiceHandler getDigimonsServiceHandler)
    {
        _getDigimonsServiceHandler = getDigimonsServiceHandler;
    }

    [HttpGet("Questions")]
    public async Task<ActionResult<GetDigimonsServiceResponse>> GetDigimons()
    {
        var response = await _getDigimonsServiceHandler.GetDigimons();

        var digimonServiceReponse = new GetDigimonsServiceResponse
        {
            Name = response.Contents[0].Name,
            Image = response.Contents[0].Image
        };

        return new OkObjectResult(digimonServiceReponse);
    }
}