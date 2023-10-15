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


        var digimonsServiceReponse = new List<GetDigimonsServiceResponse>();
        foreach (var content in response.Contents)
        {
            var digimonServiceResponse = new GetDigimonsServiceResponse
            {
                Name = content.Name,
                Image = content.Image
            };

            digimonsServiceReponse.Add(digimonServiceResponse);
        }

        return new OkObjectResult(digimonsServiceReponse);
    }
}