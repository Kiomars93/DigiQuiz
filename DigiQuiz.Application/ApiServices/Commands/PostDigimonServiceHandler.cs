using DigiQuiz.Application.ApiServices.Requests;
using DigiQuiz.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiQuiz.Application.ApiServices.Commands
{
    public class PostDigimonServiceHandler : IPostDigimonServiceHandler
    {
        public async Task<string> PostDigimonHandler(PostDigimonServiceRequest serviceRequest)
        {
            // Todo: 
            // Om det är rätt svar så ska vi skicka ner poäng till respektive spelare annars inget
            // Test scenario där poäng:en är rätt
            if (serviceRequest.Name == "Agumon")
            {
                // skickar ner poäng till via EF Core till DB:n
                await Task.Delay(2000);
                return "";
            }
            else
            {
                return null;
            }
        }
    }
}
